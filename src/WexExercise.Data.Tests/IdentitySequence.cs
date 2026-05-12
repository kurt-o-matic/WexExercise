namespace WexExercise.Data.Tests;

[TestClass]
public class IdentitySequence
{
    [TestMethod]
    public void ValidateSequence()
    {
        var seq = new Sequence();
        Assert.AreEqual(0, seq.Last());
        
        long id = seq.Next();
        Assert.AreEqual(1, id);
    }
}
