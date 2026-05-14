using LiteDB;

using static WexExercise.Data.Entities;

namespace WexExercise.Data
{
    public class Repository
    {
        // the database path is being fixed here in code to be managed by
        // source code change control; configuration is only needed when
        // called for by business requirements or change control policy
        private LiteDatabase CreateDatabase() => new LiteDatabase($"Filename={AppContext.BaseDirectory}wexex.db;Connection=shared");

        public Transaction AddTransaction(string description, DateOnly transDate, decimal purchaceAmount)
        {
            var transaction = new Transaction()
            {
                Id = Guid.CreateVersion7(), //pk-friendly time ordered guid
                Description = description,
                TransactionDate = transDate,
                PurchaseAmount = purchaceAmount
            };

            using var db = CreateDatabase();

            var col = db.GetCollection<Transaction>("transactions");
            col.Insert(transaction);
            col.EnsureIndex(t => t.Id);

            return transaction;
        }

        public Transaction? GetTransaction(Guid id)
        {
            using var db = CreateDatabase();

            var col = db.GetCollection<Transaction>("transactions");
            col.EnsureIndex(t => t.Id);

            var result = col.Query()
                .Where(t => t.Id == id)
                .FirstOrDefault();

            return result;
        }
    }
}
