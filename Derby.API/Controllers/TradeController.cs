using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Derby.API.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Derby.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TradeController : ControllerBase
    {
        private ITradeService _tradeService;

        public TradeController(ITradeService tradeService)
        {
            _tradeService = tradeService;
        }

        [HttpGet("GetAllTrades")]
        public async Task<IActionResult> GetTrades()
        {
            var trades = await _tradeService.GetAllAsyc();
            if(trades!= null && trades.ToList().Count > 0)
            {
                return Ok(trades);

            }
            return BadRequest("No Trades found");
        }

        [HttpGet("GetATradeByInstrumentName")]
        public async Task<IActionResult> GetATradeByInstrumentName(string name)
        {
            var instrument = await _tradeService.GetTradeByInstrumentName(name);
            if (instrument == null)
            {
                return NotFound("Trade not found");
            }
            return Ok(instrument);
        }

        [HttpGet("GetLatestTradesByInstrumentName")]
        public async Task<IActionResult> GetTradesByInstrumentName(string name, int count)
        {
            var instrument = await _tradeService.GetLatestTradesByInstrumentName(name, count);
            if (instrument == null)
            {
                return NotFound($"Trades not found for instrument Name: {name}");
            }
            return Ok(instrument);
        }
    }
}

