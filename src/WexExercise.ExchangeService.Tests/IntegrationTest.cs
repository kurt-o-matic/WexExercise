using WexExercise.Data;
using WexExercise.TreasuryService;
using static WexExercise.ExchangeService.Models;

namespace WexExercise.ExchangeService.Tests
{
    [TestClass]
    public sealed class IntegrationTest
    {
        private static CurrencyExchange service = new CurrencyExchange(
            repo: new Repository(), 
            rates: new TreasuryExchangeRates());

        [TestMethod]
        public void NormalCase()
        {
            var purchase = new Purchase()
            {
                Description = "normal test case",
                TransactionDate = new DateOnly(2020, 01, 01),
                PurchaseAmount = 150.25m
            };

            var NormalId = service.AddPurchase(purchase);
            var converted = service.ConvertTransaction(NormalId, "Canada", "Dollar");

            Assert.IsNotNull(converted);
            Assert.AreNotEqual(0.0m, converted.ConvertedAmount);
        }

        [TestMethod]
        public void MissingCase()
        {
            Assert.Throws<KeyNotFoundException>(() =>
            {
                service.ConvertTransaction(Guid.NewGuid(), "Canada", "Dollar");
            });
        }

        [TestMethod]
        public void ExpiredCase()
        {
            var purchase = new Purchase()
            {
                Description = "expired test case",
                TransactionDate = new DateOnly(1970, 01, 01),
                PurchaseAmount = 15.00m
            };

            var ExpiredId = service.AddPurchase(purchase);

            Assert.ThrowsAsync<KeyNotFoundException>(async () =>
            {
                service.ConvertTransaction(ExpiredId, "Canada", "Dollar");
            });
        }
    }
}
