using AutoMapper;
using Derby.API.Services;
using Derby.Domain.Models.DataModels;
using Derby.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;


namespace Derby.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InsturmentController : ControllerBase
    {
        private readonly ILogger<InsturmentController> _logger;
        private readonly IDerebitService _derebitService;
        private readonly ITradeService _tradeService;
        private readonly IInstrumentService _instrumentService;

        // GET: /<controller>/
        public InsturmentController(ILogger<InsturmentController> logger,
            IDerebitService derebitService,
            IInstrumentService instrumentService,
            ITradeService tradeService)
        {
            _logger = logger;
            _derebitService = derebitService;
            _instrumentService = instrumentService;
            _tradeService = tradeService;
        }

        [HttpPost("PostDummyInstrument")]
        public async Task<IActionResult> PostDummyInstrument(Instrument instrument)
        {
            await _instrumentService.CreateAsync(instrument);
            return Ok("successfully made");
        }

        [HttpGet("GetAllInstruments")]
        public async Task<IActionResult> GetInstruments()
        {
            var instruments = await _instrumentService.GetInstruments();
            return Ok(instruments);
        }

        [HttpGet("GetInstrumentByName")]
        public async Task<IActionResult> GetInstrumentById(string name)
        {
            var instrument = await _instrumentService.GetByName(name);
            if(instrument == null)
            {
                return NotFound("Instrument not found");
            }
            return Ok(instrument);
        }

        [HttpGet("GetAllTrades")]
        public async Task<IActionResult> GetTrades()
        {
            var instruments = await _instrumentService.GetInstruments();
            return Ok(instruments);
        }

        [HttpGet("GetLatestTradeByInstrumentName")]
        public async Task<IActionResult> GetLatestTradeByInstrumentName(string name)
        {
            var instrument = await _tradeService.GetTradeByInstrumentName(name);
            if (instrument == null)
            {
                return NotFound("Instrument not found");
            }
            return Ok(instrument);
        }

        [HttpGet("GetTradesByInstrumentName")]
        public async Task<IActionResult> GetTradesByInstrumentName(string name, int count)
        {
            var instrument = await _tradeService.GetLatestTradesByInstrumentName(name, count);
            if (instrument == null)
            {
                return NotFound("Instrument not found");
            }
            return Ok(instrument);
        }

        [HttpGet("GetNum")]
        public int GetNum()
        {
            return 2;
        }
    }
}

