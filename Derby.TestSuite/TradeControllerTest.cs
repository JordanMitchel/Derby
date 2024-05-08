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
	public class TradeControllerTest
    {
		private readonly TradeController _tradeController;
		private readonly Mock<ITradeService> _tradeService = new Mock<ITradeService>();

		public TradeControllerTest()
		{
            _tradeController = new TradeController(_tradeService.Object);
		}

		[Fact]
		public async Task GetInstruments_ShouldReturnAListOfInstruments_WhenPresent()
		{
			var trades = A.ListOf<Trade>(5);
			_tradeService.Setup(x => x.GetAllAsyc()).ReturnsAsync(trades);

			var tradeResult = await _tradeController.GetTrades();
			var okResult = tradeResult as OkObjectResult;

			Assert.IsAssignableFrom<IActionResult>(tradeResult);
			Assert.Equal(200, okResult.StatusCode);
		}

		[Fact]
        public async Task GetInstruments_ShouldReturnBadRequest_WhenNoInstrumentsPresent()
        {
           var instrumentResult = await _tradeController.GetTrades();
           var badResult = instrumentResult as BadRequestObjectResult;

           Assert.Equal(400, badResult.StatusCode);
			Assert.Equal("No trades found", badResult.Value);
        }

		[Fact]
		public async Task GetTradesByInstrumentName_ShouldReturnTrades_WhenInstrumentNameCorrect()
		{
			var trades = A.ListOf<Trade>(2);

            _tradeService.Setup(x => x.GetLatestTradesByInstrumentName("testName",2)).ReturnsAsync(trades);

            var tradeResult = await _tradeController.GetTradesByInstrumentName("testName",2);
			var okResult = tradeResult as OkObjectResult;
			var resultData = okResult.Value as List<Trade>;

            Assert.Equal(200, okResult.StatusCode);
			Assert.Equal(2, resultData.Count());
		}

        [Fact]
        public async Task GetTradesByInstrumentName_ShouldNotReturnTrades_WhenInstrumentNameIncorrect()
        {
            var trades = A.ListOf<Trade>(5);
			var tradesSlim = trades.Take(2).ToList();
            _tradeService.Setup(x => x.GetLatestTradesByInstrumentName("testName",2)).ReturnsAsync(tradesSlim);

            var tradeResult = await _tradeController.GetTradesByInstrumentName("wrong test name",2);
            var notFoundResult = tradeResult as NotFoundObjectResult;

            Assert.Equal(404, notFoundResult.StatusCode);
			Assert.Equal("Trades not found for instrument Name: wrong test name", notFoundResult.Value);
        }


    }
}

