using System;
using Newtonsoft.Json;

namespace Derby.API
{
	public class Trade
	{
        [JsonProperty("trade_id")]
        public string tradeId { get; set; }

		public decimal contracts { get; set; }

        [JsonProperty("instrument_name")]
        public string instrumentName { get; set; }

        [JsonProperty("tick_direction")]
        public string tickDirection {get;set;}

        [JsonProperty("trade_seq")]
        public string tradeSeq { get; set; }

		public decimal amount { get; set; }

        [JsonProperty("mark_price")]
        public decimal markPrice { get; set; }

        [JsonProperty("index_price")]
        public decimal indexPrice { get; set; }

		public string direction { get; set; }

		public decimal price { get; set; }

        [JsonProperty("timestamp")]
		public string timeStap { get; set; }
	}
}

