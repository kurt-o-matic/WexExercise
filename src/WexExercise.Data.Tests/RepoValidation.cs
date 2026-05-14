
using static WexExercise.Data.Entities;

namespace WexExercise.Data.Tests;

[TestClass]
public class RepoValidation
{
    private static readonly Repository repo = new Repository();

    [TestMethod]
    public void RoundTrip()
    {
        string testDesc = "TEST DATA: live data test";

        Guid id = repo
            .AddTransaction(testDesc, new DateOnly(2020, 12, 31), 150.25m)
            .Id;

        Transaction? txn = repo.GetTransaction(id);

        Assert.IsNotNull(txn);
        Assert.AreEqual(testDesc, txn.Description);
    }
}
