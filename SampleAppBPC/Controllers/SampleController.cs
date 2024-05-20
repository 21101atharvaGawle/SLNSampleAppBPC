using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleAppBPC.Repository;
using SampleAppBPC.Responses;

namespace SampleAppBPC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly ISampleService _sampleService;
        public SampleController(ISampleService sampleService)
        {
           _sampleService = sampleService;
        }
        [HttpGet("Read")]
        public async Task<ActionResult> GetDataFromAppSetting()
        { 
            string value=_sampleService.GetData();
            return Ok(new Response<string>(value));
        }
    }
}
