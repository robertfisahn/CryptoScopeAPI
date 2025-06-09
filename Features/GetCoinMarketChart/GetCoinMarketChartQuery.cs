using CryptoScopeAPI.Dtos;
using MediatR;

namespace CryptoScopeAPI.Features.GetCoinMarketChart
{
    public record GetCoinMarketChartQuery(string Id, string Days) : IRequest<CoinMarketChartDto>
    {
    }
}
