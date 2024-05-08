using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Derby.Domain.Models.Entities
{
	public class Instrument
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
		public string InstrumentName { get; set; }
	}
}

