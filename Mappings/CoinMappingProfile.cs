using AutoMapper;
using CryptoScopeAPI.Models;
using CryptoScopeAPI.Dtos;
using CryptoScopeAPI.Dtos.CoinGecko;
using System.Diagnostics;
using System.Text.Json;

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

            CreateMap<CoinDetails, CoinDetailsDto>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CoinId))
              .ForMember(dest => dest.Image, opt => opt.MapFrom(src => new CoinDetailsDto.CoinImage
              {
                  Thumb = src.ImageThumb,
                  Small = src.ImageSmall,
                  Large = src.ImageLarge
              }))
              .ForMember(dest => dest.MarketData, opt => opt.MapFrom(src => new CoinDetailsDto.CoinMarketData
              {
                  CurrentPrice = new CoinDetailsDto.UsdValue { Usd = src.CurrentPriceUsd },
                  MarketCap = new CoinDetailsDto.UsdValue { Usd = src.MarketCapUsd },
                  PriceChangePercentage24h = src.PriceChangePercentage24h
              }));
            CreateMap<CoinDetailsGeckoResponse, CoinDetailsDto>();
            CreateMap<CoinDetailsGeckoResponse.CoinImage, CoinDetailsDto.CoinImage>();
            CreateMap<CoinDetailsGeckoResponse.CoinMarketData, CoinDetailsDto.CoinMarketData>();
            CreateMap<CoinDetailsGeckoResponse.UsdValue, CoinDetailsDto.UsdValue>();

            CreateMap<CoinDetailsDto, CoinDetails>()
              .ForMember(dest => dest.Id, opt => opt.Ignore())
              .ForMember(dest => dest.CoinId, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.Symbol))
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
              .ForMember(dest => dest.ImageThumb, opt => opt.MapFrom(src => src.Image.Thumb))
              .ForMember(dest => dest.ImageSmall, opt => opt.MapFrom(src => src.Image.Small))
              .ForMember(dest => dest.ImageLarge, opt => opt.MapFrom(src => src.Image.Large))
              .ForMember(dest => dest.CurrentPriceUsd, opt => opt.MapFrom(src => src.MarketData!.CurrentPrice!.Usd ?? 0))
              .ForMember(dest => dest.MarketCapUsd, opt => opt.MapFrom(src => src.MarketData!.MarketCap!.Usd ?? 0))
              .ForMember(dest => dest.PriceChangePercentage24h, opt => opt.MapFrom(src => src.MarketData!.PriceChangePercentage24h ?? 0))
              .ForMember(dest => dest.LastUpdated, opt => opt.Ignore());

            CreateMap<CoinMarketChart, CoinMarketChartDto>()
                .ForMember(dest => dest.Prices, opt => opt.MapFrom(src => JsonSerializer.Deserialize<List<List<decimal>>>(src.PricesJson, (JsonSerializerOptions?)null)!))
                .ForMember(dest => dest.MarketCaps, opt => opt.MapFrom(src => JsonSerializer.Deserialize<List<List<decimal>>>(src.MarketCapsJson, (JsonSerializerOptions?)null)!))
                .ForMember(dest => dest.TotalVolumes, opt => opt.MapFrom(src => JsonSerializer.Deserialize<List<List<decimal>>>(src.TotalVolumesJson, (JsonSerializerOptions?)null)!));

             CreateMap<CoinMarketChartGeckoResponse, CoinMarketChart>()
                .ForMember(dest => dest.PricesJson, opt => opt.MapFrom(src => JsonSerializer.Serialize(src.Prices, (JsonSerializerOptions?)null)!))
                .ForMember(dest => dest.MarketCapsJson, opt => opt.MapFrom(src => JsonSerializer.Serialize(src.MarketCaps, (JsonSerializerOptions?)null)!))
                .ForMember(dest => dest.TotalVolumesJson, opt => opt.MapFrom(src => JsonSerializer.Serialize(src.TotalVolumes, (JsonSerializerOptions?)null)!));
        }
    }

}
