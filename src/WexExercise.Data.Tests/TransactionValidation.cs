using static WexExercise.Data.Models;

namespace WexExercise.Data.Tests
{
    [TestClass]
    public sealed class TransactionValidation
    {
        private readonly Sequence IdSeq = new Sequence();

        [TestMethod]
        public void CreateTransaction()
        {
            var transaction = new Transaction()
            {
                Id = IdSeq.Next(),
                Description = "description",
                TransactionDate = DateOnly.FromDateTime(DateTime.Today),
                PurchaseAmount = new decimal(1.5)
            };

            Assert.IsNotNull(transaction);
        }

        [TestMethod]
        public void DescriptionLength()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var transaction = new Transaction()
                {
                    Id = IdSeq.Next(),
                    Description = "012345678901234567890123456789012345678901234567890",
                    TransactionDate = DateOnly.FromDateTime(DateTime.Today),
                    PurchaseAmount = new decimal(1.5)
                };
            });
        }

        [TestMethod]
        public void DateFormat()
        {
            Assert.Throws<FormatException>(() =>
            {
                var transaction = new Transaction()
                {
                    Id = IdSeq.Next(),
                    Description = "description",
                    TransactionDate = DateOnly.Parse("2001-01-01 8:30"),
                    PurchaseAmount = new decimal(1.5)
                };
            });
        }

        [TestMethod]
        public void AmountUsd()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var transaction = new Transaction()
                {
                    Id = IdSeq.Next(),
                    Description = "description",
                    TransactionDate = DateOnly.FromDateTime(DateTime.Today),
                    PurchaseAmount = new decimal(-2.5)
                };
            });

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var transaction = new Transaction()
                {
                    Id = IdSeq.Next(),
                    Description = "description",
                    TransactionDate = DateOnly.FromDateTime(DateTime.Today),
                    PurchaseAmount = new decimal(1.021)
                };
            });
        }
    }
}
