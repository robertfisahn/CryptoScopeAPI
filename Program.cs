using Microsoft.EntityFrameworkCore;
using MediatR;
using CryptoScopeAPI.Services;
using CryptoScopeAPI.Features.GetCoins;
using System.Reflection;
using CryptoScopeAPI.Mappings;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient<ICoinGeckoClient, CoinGeckoClient>(client =>
{
    client.DefaultRequestHeaders.Add("User-Agent", "CryptoScopeApp");
});
builder.Services.AddAutoMapper(typeof(CoinMappingProfile));
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=coins.db"));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.MapGet("/api/coins", async (IMediator mediator) =>
{
    var result = await mediator.Send(new GetCoinsQuery());
    return Results.Ok(result);
});

app.Run();