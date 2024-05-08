using System;
using Newtonsoft.Json;

namespace Derby.Domain.Models.DataModels
{
    public class TradeBody
    {
        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        public List<Trade> trades { get; set; }
    }
}

