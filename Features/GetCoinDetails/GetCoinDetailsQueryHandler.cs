using CryptoScopeAPI.Services;
using CryptoScopeAPI.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CryptoScopeAPI.Models;

namespace CryptoScopeAPI.Features.GetCoinDetails;

public class GetCoinDetailsQueryHandler(ICoinGeckoClient _client, AppDbContext _db, IMapper _mapper)
    : IRequestHandler<GetCoinDetailsQuery, CoinDetailsDto>
{
    public async Task<CoinDetailsDto> Handle(GetCoinDetailsQuery request, CancellationToken cancellationToken)
    {
        var existing = await _db.CoinDetails.FirstOrDefaultAsync(c => c.CoinId == request.Id, cancellationToken);

        var isStale = existing == null || existing.LastUpdated < DateTime.UtcNow.AddMinutes(-5);

        if (isStale)
        {
            var apiData = await _client.GetCoinDetailsAsync(request.Id, cancellationToken);

            if (apiData == null)
                throw new Exception("Coin not found");

            var entity = _mapper.Map<CoinDetails>(apiData);
            entity.LastUpdated = DateTime.UtcNow;

            if (existing == null)
            {
                await _db.CoinDetails.AddAsync(entity, cancellationToken);
            }
            else
            {
                _mapper.Map(entity, existing);
            }

            await _db.SaveChangesAsync(cancellationToken);
            existing = entity;
        }

        return _mapper.Map<CoinDetailsDto>(existing);
    }
}

