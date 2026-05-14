using Microsoft.AspNetCore.Diagnostics;
using WexExercise.Data;
using WexExercise.ExchangeService;
using WexExercise.TreasuryService;

using static WexExercise.ExchangeService.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<Repository>();
builder.Services.AddScoped<TreasuryExchangeRates>();
builder.Services.AddKeyedScoped<ICurrencyExchange, CurrencyExchange>("exch");

var app = builder.Build();

app.UseExceptionHandler(appError => {
    appError.Run(async context => {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

        if (contextFeature is not null)
        {
            Console.WriteLine($"Error : {contextFeature.Error}");
            await context.Response.WriteAsJsonAsync(new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error",
                MoreInfo = contextFeature.Error.Message
            });
        }
    });
});

app.MapPost("/purchase", ([FromKeyedServices("exch")] ICurrencyExchange exch, Purchase purchase) =>
{
    var id = exch.AddPurchase(purchase);

    return Results.Ok(new { Id = id });
});

app.MapGet("/convert", ([FromKeyedServices("exch")] ICurrencyExchange exch, Guid id, string country, string currency) =>
{
    var converted = exch.ConvertTransaction(id, country, currency);
    
    return Results.Ok(converted);
});

app.Run();
