namespace WexExercise.TreasuryService.Tests
{
    [TestClass]
    public sealed class ExchangeRateResponse
    {
        TreasuryExchangeRates exchangeRates = new TreasuryExchangeRates();

        [TestMethod]
        public void ServiceResponse()
        {
            var rate = exchangeRates.GetRate("Canada", "Dollar", new DateOnly(2020, 12, 31));

            Assert.IsNotNull(rate);
        }

        [TestMethod]
        public void NoApplicableRate()
        {
            var rate = exchangeRates.GetRate("Canada", "Dollar", new DateOnly(1970, 12, 31));

            Assert.IsNull(rate);
        }
    }
}
