using WexExercise.Data;
using WexExercise.TreasuryService;

using static WexExercise.ExchangeService.Models;

namespace WexExercise.ExchangeService
{
    public class CurrencyExchange(
        Repository repo,
        TreasuryExchangeRates exch)
    {
        public Conversion? ConvertTransaction(long id, string country, string currency)
        {
            return ConvertTransaction(id, $"{country}-{currency}");
        }

        public Conversion? ConvertTransaction(long id, string countryCurrency)
        {
            var trans = repo.GetTrans(id);

            if (trans is not null)
            {
                var rate = exch.GetRate(countryCurrency, trans.TransactionDate);

                if (rate.CountryCurrency == countryCurrency) //not empty
                {
                    return new Conversion()
                    {
                        Id = id,
                        Description = trans.Description,
                        TransactionDate = trans.TransactionDate,
                        PurchaseAmount = trans.PurchaseAmount,
                        CountryCurrency = rate.CountryCurrency,
                        ExchangeRate = rate.ExchangeRate,
                        ConvertedAmount = Math.Round(trans.PurchaseAmount * rate.ExchangeRate, 2)
                    };
                }
                else
                {
                    throw new KeyNotFoundException($"No valid exchange rate record found for {countryCurrency}.");
                }
            }
            else
            {
                throw new KeyNotFoundException($"No transaction record found for ID:{id}.");
            }
        }
    }
}
