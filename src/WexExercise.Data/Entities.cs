namespace WexExercise.Data
{
    public class Entities
    {
        public record Transaction
        {
            public required Guid Id { get; init; }
            public required string Description { get; init; }
            public required DateOnly TransactionDate { get; init; }
            public required decimal PurchaseAmount { get; init; }
        }
    }
}
