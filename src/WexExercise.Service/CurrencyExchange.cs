using WexExercise.Data;
using WexExercise.TreasuryService;

using static WexExercise.ExchangeService.Models;

namespace WexExercise.ExchangeService
{
    public class CurrencyExchange(
        Repository repo,
        TreasuryExchangeRates exch)
    {
        public Conversion ConvertTransaction(Guid id, string country, string currency)
        {
            return ConvertTransaction(id, $"{country}-{currency}");
        }

        public Conversion ConvertTransaction(Guid id, string countryCurrency)
        {
            var txn = repo.GetTransaction(id);

            if (txn is not null)
            {
                var rate = exch.GetRate(countryCurrency, txn.TransactionDate);

                if (rate is not null)
                {
                    return new Conversion()
                    {
                        Id = id,
                        Description = txn.Description,
                        TransactionDate = txn.TransactionDate,
                        PurchaseAmount = txn.PurchaseAmount,
                        CountryCurrency = rate.CountryCurrency,
                        ExchangeRate = rate.ExchangeRate,
                        ConvertedAmount = Math.Round(txn.PurchaseAmount * rate.ExchangeRate, 2)
                    };
                }
                else
                {
                    throw new KeyNotFoundException($"This purchase cannot be converted to {countryCurrency}.");
                }
            }
            else
            {
                throw new KeyNotFoundException($"No transaction record found for ID:{id}.");
            }
        }
    }
}
