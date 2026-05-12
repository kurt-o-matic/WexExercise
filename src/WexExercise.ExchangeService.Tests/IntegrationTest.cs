using WexExercise.Data;
using WexExercise.TreasuryService;

namespace WexExercise.ExchangeService.Tests
{
    [TestClass]
    public sealed class IntegrationTest
    {
        private static Repository repo = Repository.FromInMemoryDb();
        private static TreasuryExchangeRates exch = new TreasuryExchangeRates();

        private long NormId { get; init; }
        private long ExpId { get; init; }

        public IntegrationTest()
        {
            NormId = repo
                .AddTrans("normal case", new DateOnly(2020, 12, 31), 150.25m)
                .Id;

            ExpId = repo
                .AddTrans("expired case", new DateOnly(1970, 12, 31), 15.00m)
                .Id;
        }

        [TestMethod]
        public void NormalCase()
        {
            var svc = new CurrencyExchange(repo, exch);
            var conv = svc.ConvertTransaction(NormId, "Canada", "Dollar");

            Assert.IsNotNull(conv);
            Assert.AreNotEqual(0.0m, conv.ConvertedAmount);
        }

        [TestMethod]
        public void ExpiredCase()
        {
            var svc = new CurrencyExchange(repo, exch);

            Assert.Throws<KeyNotFoundException>(() =>
            {
                svc.ConvertTransaction(ExpId, "Canada", "Dollar");
            });
        }
    }
}
