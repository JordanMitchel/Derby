using System;

namespace Derby.Domain.Models.DataModels
{
    public class Instruments : InstrumentData
    {
        public List<InstrumentRequest> Result { get; set; }
    }
}

