namespace SampleAppBPC.Repository
{
    public class SampleService : ISampleService
    {
        private readonly IConfiguration _configuration;
        public SampleService(IConfiguration configuration)
        {
            //configuration = _configuration;
            _configuration = configuration;
        }
        public string GetData()
        {
            return _configuration["data"];
        }
    }
}
