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
        private readonly IInstrumentService _instrumentService;
        private readonly IMapper _mapper;

        // GET: /<controller>/
        public InsturmentController(ILogger<InsturmentController> logger,
            IDerebitService derebitService,
            IInstrumentService instrumentService,
            IMapper mapper)
        {
            _logger = logger;
            _derebitService = derebitService;
            _instrumentService = instrumentService;
            _mapper = mapper;
        }


        [HttpPost("FetchInstrumentDataFromDerebit")]
        public async Task<LastTrade> FetchInstrumentData(string instrumentName)
        {
            var lastTradeData = _derebitService.GetLastTradeDataFromDerebitAsync(instrumentName);
            _logger.Log(LogLevel.Information, "Data recieved from Derebit API");
            return await lastTradeData;
        }

        [HttpPost("FetchInstrumentNamesFromDerebit")]
        public async Task<IActionResult> FetchInstruments()
        {
            var instruments = await _derebitService.GetInstrumentsFromDerebitAsync();
            var instrumentNames = _derebitService.GetInstruments(instruments);
            _logger.Log(LogLevel.Information, "Data recieved from Derebit API");

            await _instrumentService.CreateManyAsync(instrumentNames.ToList());
            return Ok("Data successfully imported from Derebit");
        }

        [HttpPost("PostDummyInstrument")]
        public async Task<IActionResult> PostDummyInstrument(Domain.Models.Entities.Instrument instrument)
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

        [HttpGet("GetNum")]
        public int GetNum()
        {
            return 2;
        }


        [HttpGet("DTOTest")]
        public ActionResult<Strument> GetTest()
        {
            var hakiUser = new StrumentRequest();
            hakiUser.hakiName = "Luffy";
            hakiUser.word = "Kiya";

            return Ok(_mapper.Map<Strument>(hakiUser));

        }
    }
}

