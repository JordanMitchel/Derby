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
				//.ForMember(dest =>
				//dest.InstrumentName,
				//opt => opt.MapFrom(src => src.InstrumentName))
				.ForMember(dest =>
				dest.Id,
				opt => opt.MapFrom(src => ""));

			CreateMap<StrumentRequest, Strument>()
				.ForMember(dest =>
				dest.loveName,
				opt => opt.MapFrom(src => src.hakiName))
				.ForMember(dest => dest.id,
				opt => opt.MapFrom(src => "3"));

		}
	}
}

