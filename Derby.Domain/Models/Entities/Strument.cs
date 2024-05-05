using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Derby.Domain.Models.Entities
{
	public class Strument
	{
		public string id { get; set; }
		public string loveName { get; set; }
		public string word { get; set; }
	}
}

