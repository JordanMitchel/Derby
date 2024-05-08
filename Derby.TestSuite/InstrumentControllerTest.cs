using System;
using Derby.API.Controllers;
using Derby.API.Services;
using Derby.Domain.Models.DataModels;
using Derby.Domain.Models.Entities;
using GenFu;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Derby.TestSuite
{
	public class InstrumentControllerTest
	{
		private readonly InsturmentController _instrumentController;
		private readonly Mock<IInstrumentService> _instrumentService = new Mock<IInstrumentService>();

		public InstrumentControllerTest()
		{
			_instrumentController = new InsturmentController(_instrumentService.Object);
		}

		[Fact]
		public async Task GetInstruments_ShouldReturnAListOfInstruments_WhenPresent()
		{
			var instruments = A.ListOf<Instrument>(5);
			_instrumentService.Setup(x => x.GetInstruments()).ReturnsAsync(instruments);

			var instrumentResult = await _instrumentController.GetInstruments();
			var okResult = instrumentResult as OkObjectResult;

			Assert.IsAssignableFrom<IActionResult>(instrumentResult);
			Assert.Equal(200, okResult.StatusCode);
		}

		[Fact]
        public async Task GetInstruments_ShouldReturnBadRequest_WhenNoInstrumentsPresent()
        {
           var instrumentResult = await _instrumentController.GetInstruments();
           var badResult = instrumentResult as BadRequestObjectResult;

           Assert.Equal(400, badResult.StatusCode);
			Assert.Equal("No instruments found", badResult.Value);
        }

		[Fact]
		public async Task GetInstrumentsByName_ShouldReturnInstruments_WhenInstrumentNameCorrect()
		{
			var instrument = A.New<Instrument>();
			instrument.InstrumentName = "testName";
            _instrumentService.Setup(x => x.GetByName("testName")).ReturnsAsync(instrument);

            var instrumentResult = await _instrumentController.GetInstrumentByName(instrument.InstrumentName);
			var okResult = instrumentResult as OkObjectResult;
			var resultData = okResult.Value as Instrument;

            Assert.Equal(200, okResult.StatusCode);
			Assert.Equal("testName", resultData.InstrumentName);
		}

        [Fact]
        public async Task GetInstrumentsByName_ShouldNotReturnInstruments_WhenInstrumentNameWrong()
        {
            var instrument = A.New<Instrument>();
            instrument.InstrumentName = "wrongTestName";
            _instrumentService.Setup(x => x.GetByName("testName")).ReturnsAsync(instrument);

            var instrumentResult = await _instrumentController.GetInstrumentByName(instrument.InstrumentName);
            var notFoundResult = instrumentResult as NotFoundObjectResult;

            Assert.Equal(404, notFoundResult.StatusCode);
			Assert.Equal("Instrument not found with Name: wrongTestName", notFoundResult.Value);
        }


    }
}

