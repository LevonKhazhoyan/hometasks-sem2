using NUnit.Framework;
using Routers;

namespace RoutersTests;

[TestFixture]
public class RoutersUnitTests
{
    [Test]
    public void ConnectedGraphTest()
    {
        RoutersNetwork.MakeOptimalNetwork("../../../RoutersNetwork.txt", "../../../RoutersNetworkResult.txt");
        Assert.IsTrue(FileComparator.FilesAreEqual("../../../ExpectedNetwork.txt", "../../../RoutersNetworkResult.txt"));
    }
}