using System.Text.Json.Serialization;

namespace WexExercise.TreasuryService
{
    public class Models
    {
        public record CurrencyExchangeRate
        {
            [JsonPropertyName("country_currency_desc")]
            public string CountryCurrency { get; init; } = string.Empty;

            [JsonPropertyName("exchange_rate")]
            public decimal ExchangeRate { get; init; }

            [JsonPropertyName("record_date")]
            public DateOnly RecordDate { get; init; }
        }
    }
}
