using AutoMapper;
using CryptoScopeAPI.Dtos;
using CryptoScopeAPI.Models;
using CryptoScopeAPI.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoScopeAPI.Features.GetCoinMarketChart
{
    public class GetCoinMarketChartQueryHandler(AppDbContext _db, IMapper _mapper, ICoinGeckoClient _client) : IRequestHandler<GetCoinMarketChartQuery, CoinMarketChartDto>
    {
        public async Task<CoinMarketChartDto> Handle(GetCoinMarketChartQuery request, CancellationToken cancellationToken)
        {
            var existing = await _db.CoinMarketCharts
                .FirstOrDefaultAsync(c => c.CoinId == request.Id && c.TimeRange == request.Days, cancellationToken);

            if (existing == null || existing.LastUpdated < DateTime.UtcNow.AddMinutes(-5))
            {
                var apiData = await _client.GetCoinMarketChartAsync(request.Id, request.Days, cancellationToken)
                    ?? throw new Exception("Coin market chart data not found");

                if (existing == null)
                {
                    var newEntity = _mapper.Map<CoinMarketChart>(apiData);
                    newEntity.CoinId = request.Id;
                    newEntity.TimeRange = request.Days;
                    newEntity.LastUpdated = DateTime.UtcNow;

                    await _db.CoinMarketCharts.AddAsync(newEntity, cancellationToken);
                    existing = newEntity;
                }
                else
                {
                    _mapper.Map(apiData, existing);
                    existing.LastUpdated = DateTime.UtcNow;
                }
                await _db.SaveChangesAsync(cancellationToken);
            }

            return _mapper.Map<CoinMarketChartDto>(existing);

        }
    }
}
