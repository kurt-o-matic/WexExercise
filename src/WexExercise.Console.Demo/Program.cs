using Serilog;
using System.Text.Json;
using WexExercise.Data;
using WexExercise.ExchangeService;
using WexExercise.TreasuryService;

using var log = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var options = new JsonSerializerOptions { WriteIndented = true };
var repo = Repository.FromInMemoryDb();
var exch = new TreasuryExchangeRates();

var demo = repo.AddTrans("demo record", new DateOnly(2020, 12, 31), 150.25m);

log.Information(JsonSerializer.Serialize(demo, options));

var svc = new CurrencyExchange(repo, exch);
var conv = svc.ConvertTransaction(demo.Id, "Canada", "Dollar");

log.Information(JsonSerializer.Serialize(conv, options));
