namespace WexExercise.Data
{
    public class Entities
    {
        public record Transaction
        {
            public required Guid Id { get; init; }

            public required string Description 
            { 
                get; 
                init
                {
                    field = (value != null && value.Length >= 5 && value.Length <= 50)
                        ? value
                        : throw new ArgumentOutOfRangeException("Description", "Description must be between 5 and 50 characters.");
                }
            } //must not exceed 50 characters

            public required DateOnly TransactionDate { get; init
                {
                    if (value > DateOnly.FromDateTime(DateTime.Today))
                        throw new ArgumentOutOfRangeException("TransactionDate", "TransactionDate must be present or past date.");

                    field = value;
                }
            } //must be a valid date format

            public required decimal PurchaseAmount
            {
                get;
                init
                {
                    if (value < 0.0m)
                        throw new ArgumentOutOfRangeException("PurchaseAmount", "PurchaseAmount must have positive value.");
                    else if (value.Scale > 2)
                        throw new ArgumentOutOfRangeException("PurchaseAmount", "PurchaseAmount is limited to 2 decimal places.");

                    field = value;
                }
            } //must be a valid positive amount rounded to the nearest cent
        }
    }
}
