using LiteDB;

using static WexExercise.Data.Entities;

namespace WexExercise.Data
{
    public sealed class Repository : IDisposable
    {
        // the database path is being fixed here in code to be managed by
        // source code change control; configuration is only needed when
        // called for by business requirements or change control policy
        private static readonly Lazy<LiteDatabase> _lazy =
            new Lazy<LiteDatabase>(() => 
                new LiteDatabase($"Filename={AppContext.BaseDirectory}wexex.db"));

        private static LiteDatabase Instance => _lazy.Value;

        public Transaction AddTransaction(string description, DateOnly transDate, decimal purchaceAmount)
        {
            var transaction = new Transaction()
            {
                Id = Guid.CreateVersion7(), //pk-friendly time ordered guid
                Description = description,
                TransactionDate = transDate,
                PurchaseAmount = purchaceAmount
            };

            var db = Instance;
            var col = db.GetCollection<Transaction>("transactions");
            col.Insert(transaction);
            col.EnsureIndex(t => t.Id);

            return transaction;
        }

        public Transaction? GetTransaction(Guid id)
        {
            var db = Instance;
            var col = db.GetCollection<Transaction>("transactions");
            col.EnsureIndex(t => t.Id);

            var result = col.Query()
                .Where(t => t.Id == id)
                .FirstOrDefault();

            return result;
        }

        public void Dispose() => Instance.Dispose();
    }
}
