using System;
using AutoMapper;
using Derby.Domain.Models.DataModels;
using Derby.Domain.Models.Entities;

namespace Derby.Domain.Profiles
{
	public class InstrumentProfile : Profile
	{
		public InstrumentProfile()
		{
			CreateMap<InstrumentRequest, Instrument>()
				.ForMember(dest =>
				dest.Id,
				opt => opt.MapFrom(src => ""));

			CreateMap<TradeRequest, Trade>();
		}
	}
}

