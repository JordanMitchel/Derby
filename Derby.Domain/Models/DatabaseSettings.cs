using System;
namespace Derby.Domain.Models
{
	public class DatabaseSettings
	{
		public string? ConnectionString { get; set; }
        public string? DatabaseName { get; set; }
        public string? InstrumentsCollectionName { get; set; }
        public string? TradesCollectionName { get; set; }
    }
}

