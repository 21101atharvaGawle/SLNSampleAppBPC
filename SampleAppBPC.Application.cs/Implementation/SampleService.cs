using Microsoft.Extensions.Configuration;
using SampleAppBPC.Application.cs.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleAppBPC.Application.cs.Implementation
{
    
    public class SampleService : ISampleService
    {
        private readonly IConfiguration _configuration;
        public SampleService(IConfiguration configuration)
        {
         _configuration = configuration;
        }
        public string GetData()
        {
            return _configuration["data"];
        }
    }
}
