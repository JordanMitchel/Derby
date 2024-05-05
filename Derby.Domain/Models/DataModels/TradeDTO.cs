using System;
using Newtonsoft.Json;

namespace Derby.Domain.Models.DataModels
{
    public class TradeDTO
    {
        [JsonProperty("trade_id")]
        public string TradeId { get; set; }

        public decimal Contracts { get; set; }

        [JsonProperty("instrument_name")]
        public string InstrumentName { get; set; }

        [JsonProperty("tick_direction")]
        public string TickDirection { get; set; }

        [JsonProperty("trade_seq")]
        public string TradeSeq { get; set; }

        public decimal Amount { get; set; }

        [JsonProperty("mark_price")]
        public decimal MarkPrice { get; set; }

        [JsonProperty("index_price")]
        public decimal IndexPrice { get; set; }

        public string Direction { get; set; }

        public decimal Price { get; set; }

        [JsonProperty("timestamp")]
        public string TimeStamp { get; set; }
    }
}

