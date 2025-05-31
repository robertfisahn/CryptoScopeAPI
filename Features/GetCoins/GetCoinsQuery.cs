using MediatR;
using CryptoScopeAPI.Dtos;

namespace CryptoScopeAPI.Features.GetCoins
{
    public record GetCoinsQuery() : IRequest<List<CoinListDto>>;
}