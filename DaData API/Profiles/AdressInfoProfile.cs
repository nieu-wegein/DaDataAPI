using AutoMapper;
using DaData_API.DTO;

namespace DaData_API.Profiles
{
	public class AdressInfoProfile : Profile
	{
		public AdressInfoProfile()
		{
			CreateMap<AdressInfo, AdressInfoResponse>()
				.ForMember(dest => dest.PostalCode, src => src.MapFrom(i => i.Postal_Code))
				.ForMember(dest => dest.HouseNumber, src => src.MapFrom(i => i.House))
				.ForMember(dest => dest.FloorNumber, src => src.MapFrom(i => i.Floor))
				.ForMember(dest => dest.FlatNumber, src => src.MapFrom(i => i.Flat))
				.ForMember(dest => dest.FlatArea, src => src.MapFrom(i => i.Flat_Area));
		}
	}
}
