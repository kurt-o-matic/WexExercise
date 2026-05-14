using WexExercise.Data;
using WexExercise.TreasuryService;

namespace WexExercise.ExchangeService.Tests
{
    [TestClass]
    public sealed class IntegrationTest
    {
        private static Repository repo = new Repository();
        private static TreasuryExchangeRates exch = new TreasuryExchangeRates();

        [TestMethod]
        public void NormalCase()
        {
            var NormalId = repo
                .AddTransaction("normal case", new DateOnly(2020, 12, 31), 150.25m)
                .Id;

            var svc = new CurrencyExchange(repo, exch);
            var conv = svc.ConvertTransaction(NormalId, "Canada", "Dollar");

            Assert.IsNotNull(conv);
            Assert.AreNotEqual(0.0m, conv.ConvertedAmount);
        }

        [TestMethod]
        public void MissingCase()
        {
            var svc = new CurrencyExchange(repo, exch);

            Assert.Throws<KeyNotFoundException>(() =>
            {
                svc.ConvertTransaction(Guid.NewGuid(), "Canada", "Dollar");
            });
        }

        [TestMethod]
        public void ExpiredCase()
        {
            var ExpiredId = repo
                .AddTransaction("expired case", new DateOnly(1970, 12, 31), 15.00m)
                .Id;

            var svc = new CurrencyExchange(repo, exch);

            Assert.Throws<KeyNotFoundException>(() =>
            {
                svc.ConvertTransaction(ExpiredId, "Canada", "Dollar");
            });
        }
    }
}
