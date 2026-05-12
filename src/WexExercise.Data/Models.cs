namespace WexExercise.Data
{
    public class Models
    {
        public record Transaction
        {
            //[Required]
            //[DisplayName("Unique Identifier")]
            public required long Id { get; init; }

            //[Required]
            //[DisplayName("Description")]
            //[StringLength(maximumLength: 50, MinimumLength = 0, ErrorMessage = "{0} must be between {2} and {1} characters.")]
            public required string Description 
            { 
                get; 
                init
                {
                    field = (value != null && value.Length <= 50)
                        ? value
                        : throw new ArgumentOutOfRangeException("Description", "Description must be between 0 and 50 characters.");
                }
            } //must not exceed 50 characters

            //[Required]
            //[DisplayName("Transaction Date")]
            public required DateOnly TransactionDate { get; init; } //must be a valid date format

            //[Required]
            //[DisplayName("Purchase Amount")]
            //[DisplayFormat(DataFormatString = "{0:C}")]
            //[RegularExpression(@"\d{1,3}(,?\d{3})*(\.\d{2})?$", ErrorMessage = "Invalid USD currency.")]
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
