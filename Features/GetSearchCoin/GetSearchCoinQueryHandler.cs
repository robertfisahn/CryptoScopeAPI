using CryptoScopeAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoScopeAPI.Features.GetSearchCoin
{
    public class GetSearchCoinsQueryHandler(AppDbContext _db) : IRequestHandler<GetSearchCoinQuery, List<SearchCoin>>
    {
        public async Task<List<SearchCoin>> Handle(GetSearchCoinQuery request, CancellationToken cancellationToken)
        {
            return await _db.SearchCoins
                .OrderBy(c => c.Name)
                .ToListAsync(cancellationToken);
        }
    }
}
