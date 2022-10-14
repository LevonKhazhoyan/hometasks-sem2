namespace Routers;

/// <summary>
/// Undirected graph class.
/// </summary>
public class Graph
{
    /// <summary>
    /// An adjacency matrix containing the lengths between the vertices.
    /// </summary>
    public int[,] MatrixOfLengths { get; }

    /// <summary>
    /// An adjacency matrix containing boolean values that indicate whether the vertices are connected.
    /// </summary>
    public bool[,] MatrixOfConnections { get; }

    /// <summary>
    /// Creates a new graph by adjacency matrices.
    /// </summary>
    /// <param name="matrixOfLengths">Matrix of lengths.</param>
    /// <param name="matrixOfConnections">Matrix of connections.</param>
    private Graph(int[,] matrixOfLengths, bool[,] matrixOfConnections)
    {
        if (matrixOfConnections == null || matrixOfLengths == null ||
            matrixOfConnections.Length != matrixOfLengths.Length ||
            matrixOfConnections.GetLength(0) != matrixOfConnections.GetLength(1) ||
            matrixOfLengths.GetLength(0) != matrixOfLengths.GetLength(1))
        {
            throw new ArgumentException("Graph matrix is incorrect");
        }

        MatrixOfLengths = matrixOfLengths;
        MatrixOfConnections = matrixOfConnections;
    }

    /// <summary>
    /// Creates a new graph by the file containing adjacency lists.
    /// </summary>
    /// <param name="path">Path to the file.</param>
    public Graph(string path)
    {
        var (matrixOfLengths, matrixOfConnections) = GraphUtils.ReadFromFile(path);

        if (matrixOfConnections == null || matrixOfLengths == null)
        {
            throw new ArgumentException("Graph matrix is empty");
        }

        MatrixOfLengths = matrixOfLengths;
        MatrixOfConnections = matrixOfConnections;
    }

    private void DFS(int numberOfVertex, bool[] visited)
    {
        for (var i = 0; i < MatrixOfLengths.GetLength(0); ++i)
        {
            if (MatrixOfConnections[numberOfVertex, i] && !visited[i])
            {
                visited[i] = true;
                DFS(i, visited);
            }
        }
    }

    /// <summary>
    /// Checks if the graph is connected.
    /// </summary>
    public bool IsConnected()
    {
        var visited = new bool[MatrixOfLengths.GetLength(0)];
        DFS(0, visited);

        return visited.All(x => x);
    }

    /// <summary>
    /// Graph edge class, that helps in the algorithm of making a maximum spanning tree.
    /// </summary>
    private class Edge
    {
        public int FirstVertex { get; }

        public int SecondVertex { get; }

        public int Weight { get; }

        public Edge(int weight, int firstVertex, int secondVertex)
        {
            Weight = weight;
            FirstVertex = firstVertex;
            SecondVertex = secondVertex;
        }
    }

    /// <summary>
    /// Makes maximum spanning tree.
    /// </summary>
    public Graph MakeMaximumSpanningTree()
    {
        var countOfVertices = MatrixOfLengths.GetLength(0);
        var newLengths = new int[countOfVertices, countOfVertices];
        var newConnections = new bool[countOfVertices, countOfVertices];

        var edges = new PriorityQueue<Edge>();
        var visited = new bool[countOfVertices];

        for (var i = 0; i < countOfVertices; ++i)
        {
            newConnections[i, i] = true;
        }

        var currentVertex = 0;
        for (var i = 0; i < countOfVertices - 1; ++i)
        {
            visited[currentVertex] = true;

            for (var j = 0; j < countOfVertices; ++j)
            {
                if (!visited[j] && MatrixOfConnections[currentVertex, j])
                {
                    var newEdge = new Edge(MatrixOfLengths[currentVertex, j], Math.Min(currentVertex, j), Math.Max(currentVertex, j));
                    edges.Enqueue(newEdge, newEdge.Weight);
                }
            }

            Edge? maxEdge = null;
            
            while (maxEdge == null && !edges.IsEmpty)
            {
                var firstEdgeInQueue = edges.Dequeue();
                if (!visited[firstEdgeInQueue.FirstVertex] || !visited[firstEdgeInQueue.SecondVertex])
                {
                    maxEdge = firstEdgeInQueue;
                }
            }

            if (maxEdge == null)
            {
                continue;
            }
            
            newLengths[maxEdge.FirstVertex, maxEdge.SecondVertex] = maxEdge.Weight;
            newLengths[maxEdge.SecondVertex, maxEdge.FirstVertex] = maxEdge.Weight;

            newConnections[maxEdge.FirstVertex, maxEdge.SecondVertex] = true;
            newConnections[maxEdge.SecondVertex, maxEdge.FirstVertex] = true;

            currentVertex = visited[maxEdge.FirstVertex] ? maxEdge.SecondVertex : maxEdge.FirstVertex;
        }
        
        return new Graph(newLengths, newConnections);
    }
}