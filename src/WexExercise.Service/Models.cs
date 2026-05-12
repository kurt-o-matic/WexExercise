namespace WexExercise.ExchangeService
{
    public class Models
    {
        public record Conversion
        {
            public required long Id { get; init; }
            public required string Description { get; init; }
            public required DateOnly TransactionDate { get; init; }
            public required decimal PurchaseAmount { get; init; }
            public required string CountryCurrency { get; init; }
            public required decimal ExchangeRate { get; init; }
            public required decimal ConvertedAmount { get; init; }
        }
    }
}
