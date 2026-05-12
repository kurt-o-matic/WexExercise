using System.Text.Json;
using System.Text.Json.Serialization;
using static WexExercise.TreasuryService.Models;

namespace WexExercise.TreasuryService
{
    public class TreasuryExchangeRates
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string baseUrl = "https://api.fiscaldata.treasury.gov/services/api/fiscal_service/";
        private static readonly string exchRatesUrl = "v1/accounting/od/rates_of_exchange";

        public CurrencyExchangeRate GetRate(string country, string currency, DateOnly transDate)
        {
            return GetRate($"{country}-{currency}", transDate);
        }

        public CurrencyExchangeRate GetRate(string countryCurrency, DateOnly transDate)
        {
            string fields = "fields=country_currency_desc,exchange_rate,record_date";
            string filter = $"filter=country_currency_desc:eq:{countryCurrency},record_date:gte:{transDate.AddMonths(-6).ToString("yyyy-MM-dd")},record_date:lte:{transDate.ToString("yyyy-MM-dd")}";
            string sort = "sort=-record_date";

            string url = baseUrl
                + exchRatesUrl
                + "?" + fields
                + "&" + filter
                + "&" + sort;

            string response = client
                .GetStringAsync(url)
                .GetAwaiter()
                .GetResult();

            var rates = new List<CurrencyExchangeRate>();

            using JsonDocument doc = JsonDocument.Parse(response);
            JsonElement root = doc.RootElement;
            JsonElement data = root.GetProperty("data");

            var options = new JsonSerializerOptions
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };

            foreach (JsonElement rate in data.EnumerateArray())
            {
                rates.Add(JsonSerializer.Deserialize<CurrencyExchangeRate>(rate, options)!);
            }

            return rates.FirstOrDefault() ?? new CurrencyExchangeRate();
        }
    }
}
