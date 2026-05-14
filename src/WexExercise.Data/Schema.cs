using StereoDB;
using StereoDB.CSharp;

using static WexExercise.Data.Models;

namespace WexExercise.Data
{
    public class TransactionSchema
    {
        public required ITable<Guid, Transaction> Table { get; init; }
    }

    public class Schema : IDbSchema
    {
        public TransactionSchema Transactions { get; }

        public Schema()
        {
            Transactions = new TransactionSchema
            {
                Table = StereoDb.CreateTable<Guid, Transaction>("transactions")
            };
        }

        public IEnumerable<ITable> AllTables => [Transactions.Table];
    }
}
