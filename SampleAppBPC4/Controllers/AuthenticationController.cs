using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SampleAppBPC4.Models;
using SampleAppBPC4.Repository;
using SampleAppBPC4.DAL;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SampleAppBPC4.DTO;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Cryptography;
using Azure;
using Microsoft.EntityFrameworkCore;

namespace SampleAppBPC4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IGenericRepository<Student> _newStudent;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        //private IdentityDbContext _context;
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;

        public AuthenticationController(ILogger<AuthenticationController> logger, IGenericRepository<Student> newStudent, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, AppDbContext appDbContext, IConfiguration configuration)
        {
            _logger = logger;
            _newStudent = newStudent;
            _signInManager = signInManager;
            _userManager = userManager;
            _appDbContext = appDbContext;
            _configuration = configuration;
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            var result = await _signInManager.PasswordSignInAsync(model.Username!, model.Password!, (bool)model.RememberMe, false);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),//token-id
                    new Claim("Id", user.Id)//user-id
                };

                var roleSign = "";
                foreach (var userRole in userRoles)
                {

                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));

                    if (userRole == "Admin")
                    {
                        roleSign = "admin";
                    }
                    if (userRole == "Student")
                    {
                        roleSign = "student";
                    }

                   
                }

                var token = CreateToken(authClaims);
                var refreshToken = GenerateRefreshToken();

                _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

                await _userManager.UpdateAsync(user);

                return Ok(new
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken,
                    Expiration = token.ValidTo,
                    roleSign = roleSign
                });
            }
            return Unauthorized();



           
        }

        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }


        [HttpPost]
        [Route("AdminRegister")]
        public async Task<IActionResult> AdminRegister(RegisterDto model)
        {
            if (ModelState.IsValid)
            {
                var userExists = await _userManager.FindByNameAsync(model.Email);
                if (userExists != null)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Models.Response { Status = "Error", Message = "User already exists!" });

                _userManager.CreateAsync(new AppUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    
                    SecurityStamp = Guid.NewGuid().ToString(),

                    //Password = "Admin@123"
                }, model.Password).GetAwaiter().GetResult();


                var Appuser = _appDbContext.Users.FirstOrDefault(x => x.Email == model.Email);
                //Get the default Admin User, which is created above.

                if (Appuser != null)
                {
                    _userManager.AddToRoleAsync(Appuser, "Admin").GetAwaiter().GetResult();
                }
                return Ok(new { message = "Admin Created Successfully!" });
            }
            return StatusCode(401, "Admin not Created");
        }

        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { message = "Logged Out Successfully!" });
        }

    }
}
