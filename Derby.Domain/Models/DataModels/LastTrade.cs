using System;
using Newtonsoft.Json;

namespace Derby.Domain.Models.DataModels
{
    public class LastTrade : InstrumentData
    {
        public TradeBody result { get; set; }
    }
}

