using System;
using Derby.API.Services;
using Derby.Domain.Models;
using Derby.Domain.Models.Entities;
using GenFu;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;

namespace Derby.TestSuite
{
	public class TradeTest
    {
    // {
    //     private readonly IMongoCollection<Trade> _tradeCollection;
    //     private readonly IOptions<DatabaseSettings> _dbSettings;
	// 	IOptions<DatabaseSettings> dbSettings;
    //     public TradeTest(ITradeService tradeService)
	// 	{
		
	// 		_tradeService = tradeService;
	// 	}

        //private void InitializeMongoDb()
        //{

        //    _mockMongoCollection.Setup(x => x.GetDatabase(It.IsAny<string>(),
        //        default)).Returns(this.mongodb.Object);
        //}

  //      [Fact]
		//public void testGetAllAsync()
		//{
  //          //Arrange
  //          var trades = A.ListOf<Trade>(5);
		//	var asyncCursor = new Mock<IAsyncCursor<Trade>>();

		//	_mockMongoCollection.Setup(_collection => _collection.Find(Builders<Trade>.Filter.Empty,It.IsAny<FindOptions<Trade>>(),default)).Returns(trades);

  //          var mock = new Mock<IMongoCollection<Trade>>();
		//	mock.Setup(x => x.Find(_ => true));
		//	//Act
		//	_ = _tradeService.GetAllAsyc();
		//	//Assert
		//}

	// 	[Fact]
	// 	public void testMock()
	// 	{
	// 		TradeService service = new TradeService();
    //         var tradeModel = new Trade() { Id = "123" };
    //         var returningTrade = A.ListOf<Trade>(4);
    //         var mockCollection = new Mock<IMongoCollection<Trade>>();
    //         Predicate<Trade> filter = _ => true;
    //         mockCollection.Setup(c => c.Find(_ => true,null))
    // .Returns((IFindFluent<Trade, Trade>)returningTrade.ToList());

    //         var result = _tradeService.GetAllAsyc();

    //         Assert.Equal("1", (2-1).ToString());

    //     }

		//[Fact]
		//public void exactResponse()
		//{
  //          var tradeModel = new Trade() { Id = "123" };

  //          var mockCursor = new Mock<IAsyncCursor<Trade>>();
  //          mockCursor
  //            .Setup(c => c.FirstOrDefault())
  //            .Returns(tradeModel);

  //          var mockCollection = new Mock<IMongoCollection<Trade>>();
  //          Predicate<Trade> filter = u => u.Id == tradeModel.Id;
  //          mockCollection
  //            .Setup(c => c.Find(_=> true))
  //            .ReturnsAsync(tradeModel);

  //      }
    }
}

