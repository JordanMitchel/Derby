using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Derby.Domain.Models.Entities
{
	public class Trade
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string TradeId { get; set; }
        public string InstrumentName { get; set; }
        public string TickDirection { get; set; }
        public string TradeSeq { get; set; }
        public decimal Amount { get; set; }
        public decimal MarkPrice { get; set; }
        public decimal IndexPrice { get; set; }
        public string Direction { get; set; }
        public decimal Price { get; set; }
        public string TimeStamp { get; set; }
    }
}

