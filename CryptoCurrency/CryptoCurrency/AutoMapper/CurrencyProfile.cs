using AutoMapper;
using CryptoCurrency.Models;
using BLLBriefCurrency = BLL.Models.BriefCurrency;
using BLLDetailedCurrency = BLL.Models.DetailedCurrency;
using BLLMarket = BLL.Models.Market;

namespace CryptoCurrency.AutoMapper
{
    public class CurrencyProfile : Profile
    {
        public CurrencyProfile()
        {
            CreateMap<BLLBriefCurrency, BriefCurrency>()
                .ForMember(briefCurrency => briefCurrency.Changes, config => config.MapFrom(src => src.ChangePercent24Hr));

            CreateMap<BLLDetailedCurrency, DetailedCurrency>()
                .ForMember(detailedCurrency => detailedCurrency.Volume, config => config.MapFrom(src => src.VolumUsd24Hr))
                .ForMember(detailedCurrency => detailedCurrency.Price, config => config.MapFrom(src => src.PriceUsd))
                .ForMember(detailedCurrency => detailedCurrency.Changes, config => config.MapFrom(src => src.ChangePercent24Hr));

            CreateMap<BLLMarket, Market>()
                .ForMember(market => market.Id, config => config.MapFrom(src => src.ExchangeId))
                .ForMember(market => market.Price, config => config.MapFrom(src => src.PriceUsd));
        }
    }
}
