using System;
using Newtonsoft.Json;

namespace Derby.API
{
	public class DerebitLastTradeInstrument : DerebitInstrumentData
	{
		public DerebitBody result { get; set; }
	}
}

