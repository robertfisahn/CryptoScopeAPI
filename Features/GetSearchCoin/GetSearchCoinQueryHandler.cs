using AutoMapper;
using CryptoScopeAPI.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoScopeAPI.Features.GetSearchCoin
{
    public class GetSearchCoinsQueryHandler(AppDbContext _db, IMapper _mapper) : IRequestHandler<GetSearchCoinQuery, List<SearchCoinDto>>
    {
        public async Task<List<SearchCoinDto>> Handle(GetSearchCoinQuery request, CancellationToken cancellationToken)
        {
            var existing = await _db.SearchCoins
                .OrderBy(c => c.Name)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<SearchCoinDto>>(existing);
        }
    }
}
