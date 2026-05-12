
using static WexExercise.Data.Models;

namespace WexExercise.Data.Tests;

[TestClass]
public class RepoValidation
{
    private static readonly Repository repo = Repository.FromInMemoryDb();

    [TestMethod]
    public void RoundTrip()
    {
        string testDesc = "this is a unit test";

        long id = repo
            .AddTrans(testDesc, new DateOnly(2020, 12, 31), 150.25m)
            .Id;

        Transaction? trans = repo.GetTrans(id);

        Assert.IsNotNull(trans);
        Assert.AreEqual(testDesc, trans.Description);
    }
}
