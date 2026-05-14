using WexExercise.Data;
using WexExercise.ExchangeService;
using WexExercise.TreasuryService;

using static WexExercise.ExchangeService.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<Repository>();
builder.Services.AddScoped<TreasuryExchangeRates>();
builder.Services.AddKeyedScoped<ICurrencyExchange, CurrencyExchange>("exch");

var app = builder.Build();

app.MapPost("/purchase", ([FromKeyedServices("exch")] ICurrencyExchange exch, Purchase purchase) =>
{
    return exch.AddPurchase(purchase);
});


//app.MapGet("/", () => "Hello World!");

app.Run();
