using CryptoScopeAPI.Dtos;
using MediatR;

namespace CryptoScopeAPI.Features.GetCoinDetails
{
    public record GetCoinDetailsQuery(string Id) : IRequest<CoinDetailsDto>;

}
