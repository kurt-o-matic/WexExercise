using static WexExercise.ExchangeService.Models;

namespace WexExercise.WebApi.Tests
{
    [TestClass]
    public sealed class PurchaseValidation
    {
        [TestMethod]
        public void CreatePurchase()
        {
            var purchase = new Purchase()
            {
                Description = "description",
                TransactionDate = DateOnly.FromDateTime(DateTime.Today),
                PurchaseAmount = new decimal(1.5)
            };

            Assert.IsNotNull(purchase);
        }

        [TestMethod]
        public void DescriptionLength()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var purchase = new Purchase()
                {
                    Description = "",
                    TransactionDate = DateOnly.FromDateTime(DateTime.Today),
                    PurchaseAmount = new decimal(1.5)
                };
            });

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var purchase = new Purchase()
                {
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
                var purchase = new Purchase()
                {
                    Description = "description",
                    TransactionDate = DateOnly.Parse("2001-01-01 8:30"),
                    PurchaseAmount = new decimal(1.5)
                };
            });

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var purchase = new Purchase()
                {
                    Description = "description",
                    TransactionDate = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
                    PurchaseAmount = new decimal(1.5)
                };
            });
        }

        [TestMethod]
        public void AmountUsd()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var purchase = new Purchase()
                {
                    Description = "description",
                    TransactionDate = DateOnly.FromDateTime(DateTime.Today),
                    PurchaseAmount = new decimal(-2.5)
                };
            });

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var purchase = new Purchase()
                {
                    Description = "description",
                    TransactionDate = DateOnly.FromDateTime(DateTime.Today),
                    PurchaseAmount = new decimal(1.021)
                };
            });
        }

    }
}
