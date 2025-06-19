using CryptoScopeAPI.Dtos;
using MediatR;

namespace CryptoScopeAPI.Features.GetSearchCoin
{
    public class GetSearchCoinQuery : IRequest<List<SearchCoinDto>>
    {
    }
}
