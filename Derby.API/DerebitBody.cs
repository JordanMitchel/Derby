using System;
using Newtonsoft.Json;

namespace Derby.API
{
	public class DerebitBody
	{
        [JsonProperty("has_more")]
        public bool hasMore { get; set; }

		public List<Trade> trades { get; set; }
	}
}

