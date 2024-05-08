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
        private readonly IInstrumentService _instrumentService;

        // GET: /<controller>/
        public InsturmentController(
            IInstrumentService instrumentService)
        {
            _instrumentService = instrumentService;
        }

        [HttpGet("GetAllInstruments")]
        public async Task<IActionResult> GetInstruments()
        {
            var instruments = await _instrumentService.GetInstruments();
            if (instruments != null&& instruments.ToList().Count()>0)
            {
                return Ok(instruments);
            }
            else
            {
                return BadRequest("No instruments found");
            }
        }

        [HttpGet("GetInstrumentByName")]
        public async Task<IActionResult> GetInstrumentByName(string name)
        {
            var instrument = await _instrumentService.GetByName(name);
            if(instrument == null)
            {
                return NotFound($"Instrument not found with Name: {name}");
            }
            return Ok(instrument);
        }

        [HttpGet("HealthCheck")]
        public string GetHealthCheck()
        {
            return "Healthy Response";
        }
    }
}

