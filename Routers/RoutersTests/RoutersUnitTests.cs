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
        Assert.That(FileComparator.FilesAreEqual("../../../ExpectedNetwork.txt", "../../../RoutersNetworkResult.txt"));
    }
    
    [Test]
    public void DisconnectedGraphTest()
    {
        Assert.Throws<ArgumentException>(() => RoutersNetwork.MakeOptimalNetwork("../../../DisconnectedGraph.txt", "../../..FailedResult"));
    }
}