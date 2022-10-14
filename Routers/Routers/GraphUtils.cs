namespace Routers;

using System.Text.RegularExpressions;

/// <summary>
/// Graph Utils class.
/// </summary>
public static class GraphUtils
{
    /// <summary>
    /// Splits lines of file by non-number values.
    /// </summary>
    /// <param name="path">Path to the file.</param>
    private static int[][] GetGraphInfo(string path)
    {
        if (!File.Exists(path))
        {
            throw new ArgumentException("File does not exists");
        }

        return File.ReadAllLines(path).Select(x => Regex.Split(x, @"\D+").Where(val => val != "").Select(int.Parse).ToArray()).ToArray();
    }

    /// <summary>
    /// Returns number of vertices.
    /// </summary>
    /// <param name="graphInfo">Path to the file.</param>
    private static int GetCountOfVertices(int[][] graphInfo)
    {
        var maximumNumber = 0;
        for (var i = 0; i < graphInfo.GetLength(0); ++i)
        {
            if (graphInfo[i][0] > maximumNumber)
            {
                maximumNumber = graphInfo[i][0];
            }
            for (var j = 1; j < graphInfo[i].Length; j += 2)
            {
                if (graphInfo[i][j] > maximumNumber)
                {
                    maximumNumber = graphInfo[i][j];
                }
            }
        }

        if (maximumNumber == 0)
        {
            throw new ArgumentException("Incorrect vertex number");
        }

        return maximumNumber;
    }

    /// <summary>
    /// Returns adjacency matrices for distances and connections in the graph.
    /// </summary>
    /// <param name="path">Path to the file.</param>
    public static (int[,], bool[,]) ReadFromFile(string path)
    {
        var graphInfo = GetGraphInfo(path);
        var countOfVertices = GetCountOfVertices(graphInfo);
        var matrixOfLengths = new int[countOfVertices, countOfVertices];
        var matrixOfConnections = new bool[countOfVertices, countOfVertices];

        for (var i = 0; i < countOfVertices; ++i)
        {
            matrixOfConnections[i, i] = true;
        }

        for (var i = 0; i < graphInfo.GetLength(0); ++i)
        {
            for (var j = 1; j < graphInfo[i].Length - 1; j += 2)
            {
                if (graphInfo[i][0] <= 0 || graphInfo[i][0] > countOfVertices ||
                    graphInfo[i][j] <= 0 || graphInfo[i][j] > countOfVertices)
                {
                    throw new ArgumentException("Incorrect vertex number");
                }

                matrixOfConnections[graphInfo[i][0] - 1, graphInfo[i][j] - 1] = true;
                matrixOfConnections[graphInfo[i][j] - 1, graphInfo[i][0] - 1] = true;
                matrixOfLengths[graphInfo[i][0] - 1, graphInfo[i][j] - 1] = graphInfo[i][j + 1];
                matrixOfLengths[graphInfo[i][j] - 1, graphInfo[i][0] - 1] = graphInfo[i][j + 1];
            }
        }

        return (matrixOfLengths, matrixOfConnections);
    }

    /// <summary>
    /// Writes the graph to a file as adjacency lists.
    /// </summary>
    /// <param name="graph">Specified graph.</param>
    /// <param name="path">Path to the file.</param>
    public static void WriteToFile(Graph graph, string path)
    {
        var countOfVertices = graph.MatrixOfLengths.GetLength(0);
        using var file = new StreamWriter(path);

        for (var i = 0; i < countOfVertices; ++i)
        {
            var line = "";

            for (var j = i + 1; j < countOfVertices; ++j)
            {
                if (graph.MatrixOfConnections[i, j])
                {
                    line += $" {j + 1} ({graph.MatrixOfLengths[i, j]}),";
                }
            }

            if (line == "")
            {
                continue;
            }
            
            line = line.Remove(line.Length - 1);
            line = $"{i + 1}:" + line;
            file.WriteLine(line);
        }
    }
}