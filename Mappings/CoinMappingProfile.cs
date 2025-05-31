using AutoMapper;
using CryptoScopeAPI.Models;
using CryptoScopeAPI.Dtos;

namespace CryptoScopeAPI.Mappings
{
    public class CoinMappingProfile : Profile
    {
        public CoinMappingProfile()
        {
            CreateMap<Coin, CoinListDto>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CoinId))
              .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.ImageUrl))
              .ForMember(dest => dest.CurrentPrice, opt => opt.MapFrom(src => src.CurrentPriceUsd));
        }
    }

}
