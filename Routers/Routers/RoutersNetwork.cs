namespace Routers;

/// <summary>
/// Static class for working with network graph of routers connections.
/// </summary>
public static class RoutersNetwork
{
    /// <summary>
    /// Creates the most optimal network structure for a given network.
    /// </summary>
    /// <param name="inputFilePath">Path to the input file (given network).</param>
    /// <param name="outputFilePath">Path to the output file (optimal network).</param>
    public static void MakeOptimalNetwork(string inputFilePath, string outputFilePath)
        => GraphUtils.WriteToFile(new Graph(inputFilePath).MakeMaximumSpanningTree(), outputFilePath);
}