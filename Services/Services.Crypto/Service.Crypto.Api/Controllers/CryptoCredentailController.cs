using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Crypto.Application;
namespace Service.Crypto.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CryptoCredentailController : ControllerBase
    {
        private readonly ILogger<CryptoCredentailController> _logger;
        private readonly ICryptoService _service;

        public CryptoCredentailController(ILogger<CryptoCredentailController> logger, ICryptoService service)
        {
            _service = service;
            _logger = logger;
        }


        [HttpGet("{mobile_no}/{pin}/{label}")]
        public string Get(string mobile_no, int pin, string label)
        {
            var query = new CredentialQuery 
            {
                mobile_phone = mobile_no,
                pin = pin,
                label = label
            };
            return _service.Get(query);

        }

        [HttpPost]
        public Tuple<string,string> Add(AddNewCredentialCmd cmd)
        {
            return _service.AddNew(cmd);
        }
    }
}