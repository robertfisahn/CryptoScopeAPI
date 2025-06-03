using CryptoScopeAPI.Models;
using MediatR;

namespace CryptoScopeAPI.Features.GetSearchCoin
{
    public class GetSearchCoinQuery : IRequest<List<SearchCoin>>
    {
    }
}
