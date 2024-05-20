using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.Models;
using SampleAppBPC3.Commands;
using Serilog;

namespace SampleAppBPC3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var query = new GetAllStudentsQuery();
            var result = await _mediator.Send(query);
            Log.Information("Students Info:{@result}", result);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var query = new GetStudentByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] StudentDTO student)
        {
            await _mediator.Send(new CreateStudentCommand { Student = student });
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] StudentDTO student)
        {
            student.Id = id;
            await _mediator.Send(new UpdateStudentCommand { Student = student });
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _mediator.Send(new DeleteStudentCommand { Id = id });
            return Ok();
        }
    }
}
