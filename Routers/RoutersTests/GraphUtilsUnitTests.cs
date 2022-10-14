using NUnit.Framework;
using Routers;

namespace RoutersTests;

[TestFixture]
public class GraphInFileTests
{
    private readonly Graph graph = new("../../../Graph.txt");
    
    private readonly int[,] expectedGraphLengths =
    {
        { 0, 5, 7, 0, 0 },
        { 5, 0, 0, 8, 0 },
        { 7, 0, 0, 9, 10 },
        { 0, 8, 9, 0, 0 },
        { 0, 0, 10, 0, 0 }
    };

    private readonly bool[,] expectedGraphConnections =
    {
        { true, true, true, false, false },
        { true, true, false, true, false },
        { true, false, true, true, true },
        { false, true, true, true, false },
        { false, false, true, false, true }
    };
    
    [Test]
    public void ReadFromFileShouldWorkCorrectly()
    {
        var (matrixOfLengths, matrixOfConnections) = GraphUtils.ReadFromFile("../../../Graph.txt");
        Assert.Multiple(() =>
        {
            Assert.That(expectedGraphConnections, Is.EqualTo(matrixOfConnections));
            Assert.That(expectedGraphLengths, Is.EqualTo(matrixOfLengths));
        });
    }

    [Test]
    public void WriteGraphShouldWorkCorrectly()
    {
        GraphUtils.WriteToFile(graph, "../../../WriteGraphResult.txt");
        Assert.IsTrue(FileComparator.FilesAreEqual("../../../Graph.txt", "../../../WriteGraphResult.txt"));
    }
}