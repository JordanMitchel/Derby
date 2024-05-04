using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Derby.API.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using JsonSerializer = System.Text.Json.JsonSerializer;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Derby.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InsturmentController : ControllerBase
    {
        private readonly ILogger<InsturmentController> _logger;
        private readonly IDerebitService _derebitService;

        // GET: /<controller>/
        public InsturmentController(ILogger<InsturmentController> logger, IDerebitService derebitService)
        {
            _logger = logger;
            _derebitService = derebitService;
        }


        [HttpGet("GetLastTradeInstrumentData")]
        public async Task<DerebitLastTradeInstrument> GetInstrumentData(string instrumentName)
        {
            var lastTradeData = _derebitService.GetLastTradeDataFromDerebitAsync(instrumentName);
            _logger.Log(LogLevel.Information, "Data recieved from Derebit API");
            return await lastTradeData;
        }

        [HttpGet("GetInstruments")]
        public async Task<List<string>> GetInstruments()
        {
            var instruments = await _derebitService.GetInstrumentsFromDerebitAsync();
            var instrumentNames = _derebitService.GetInstrumentNames(instruments);
            _logger.Log(LogLevel.Information, "Data recieved from Derebit API");
            return instrumentNames;
        }

        [HttpGet("GetNum")]

        public int GetNum()
        {
            return 2;
        }
    }
}

