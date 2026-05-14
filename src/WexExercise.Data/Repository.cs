using StereoDB;
using StereoDB.CSharp;

using static WexExercise.Data.Models;

namespace WexExercise.Data
{
    public class Repository
    {
        private static readonly Sequence idSeq = new Sequence();

        public required IStereoDb<Schema> Database { get; init; }

        private Repository() { }

        public static Repository FromInMemoryDb()
        {
            return new Repository()
            {
                Database = StereoDb.Create(new Schema(), StereoDbSettings.Default)
            };
        }

        public Transaction AddTrans(string desciption, DateOnly transDate, decimal purchaceAmount)
        {
            var transaction = new Transaction()
            {
                Id = Guid.CreateVersion7(), //primary key-friendly time ordered guid
                Description = desciption,
                TransactionDate = transDate,
                PurchaseAmount = purchaceAmount
            };

            Database.WriteTransaction(ctx =>
            {
                var transactions = ctx.UseTable(ctx.Schema.Transactions.Table);

                transactions.Set(transaction.Id, transaction);
            });

            return transaction;
        }

        public Transaction? GetTrans(Guid id)
        {
            return Database.WriteTransaction(ctx =>
            {
                var transactions = ctx.UseTable(ctx.Schema.Transactions.Table);

                if (transactions.TryGet(id, out var transaction))
                {
                    return transaction;
                }

                return null;
            });
        }
    }
}
