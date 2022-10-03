namespace Game;

/// <summary>
/// Class of simple game.
/// </summary>
public class Game
{
    /// <summary>
    /// Width of the game map.
    /// </summary>
    private int width;
    public int Width => width;

    /// <summary>
    /// Height of the game map.
    /// </summary>
    private int height;
    public int Height => height;

    /// <summary>
    /// Boolean representation of the game map.
    /// </summary>
    private bool[,] boolMap;
    public bool[,] BoolMap => boolMap;
    
    /// <summary>
    /// String representation of the game map.
    /// </summary>
    private string[] map;
    public string[] Map => map;

    /// <summary>
    /// Player position on the game map.
    /// </summary>
    private (int x, int y) playerPosition;
    public (int x, int y) PlayerPosition => playerPosition;

    /// <summary>
    /// Function of drawing game objects.
    /// </summary>
    private readonly Action<string> write;

    /// <summary>
    /// Cursor setting function.
    /// </summary>
    private readonly Action<int, int> setCursor;

    /// <summary>
    /// Creates a new game by the game map file, the specified writing function and the specified cursor setting function.
    /// </summary>
    /// <param name="filePath">Path to the game map file.</param>
    /// <param name="writeFunction">Specified writing function.</param>
    /// <param name="setCursorFunction">Specified cursor setting function.</param>
    public Game(string filePath, Action<string> writeFunction, Action<int, int> setCursorFunction)
    {
        write = writeFunction;
        setCursor = setCursorFunction;
        Initialize(filePath);
    }

    private static int FindMaximumLength(string[] lines)
    {
        return lines.Any() ? lines.MaxBy(x => x.Length).Length : 0;
    }

    /// <summary>
    /// Draws game map.
    /// </summary>
    public void DrawMap()
    {
        for (var i = 0; i < map.Length; ++i)
        {
            setCursor(0, i);
            write(map[i]);
        }
    }

    /// <summary>
    /// Initialize game by file
    /// </summary>
    private void Initialize(string path)
    {
        if (!File.Exists(path))
        {
            throw new ArgumentException("File does not exists");
        }

        map = File.ReadAllLines(path);
        width = FindMaximumLength(map);
        height = map.Length;

        if (width == 0 || height == 0)
        {
            throw new ArgumentException("Invalid map size");
        }

        boolMap = new bool[height, width];
        var isPlayerOnMap = false;

        for (var i = 0; i < height; ++i)
        {
            for (var j = 0; j < map[i].Length; ++j)
            {
                if (map[i][j] == '@')
                {
                    if (isPlayerOnMap)
                    {
                        throw new ArgumentException("More than one player on map");
                    }

                    isPlayerOnMap = true;

                    playerPosition = (j, i);
                    continue;
                }

                if (map[i][j] == '#')
                {
                    boolMap[i, j] = true;
                }
            }
        }

        if (!isPlayerOnMap)
        {
            throw new ArgumentException("Player is not on map");
        }
    }

    /// <summary>
    /// Returns true if cell with given coordinates is free.
    /// </summary>
    private bool IsFree(int x, int y)
        => !boolMap[y, x];

    /// <summary>
    /// Moves the player in the given coordinate.
    /// </summary>
    private void RedrawPlayer(int newX, int newY)
    {
        setCursor(playerPosition.x, playerPosition.y);
        write(" ");
        setCursor(newX, newY);
        write("@");
    }

    /// <summary>
    /// Moves the player by the given coordinates.
    /// </summary>
    private void MovePlayer(int deltaX, int deltaY)
    {
        var newX = (playerPosition.x + deltaX + width) % width;
        var newY = (playerPosition.y + deltaY + height) % height;

        if (!IsFree(newX, newY))
        {
            return;
        }
        
        RedrawPlayer(newX, newY);
        playerPosition = (newX, newY);
    }

    /// <summary>
    /// Moves the player to the left if possible.
    /// </summary>
    public void OnLeft(object sender, EventArgs args)
        => MovePlayer(-1, 0);

    /// <summary>
    /// Moves the player to the right if possible.
    /// </summary>
    public void OnRight(object sender, EventArgs args)
        => MovePlayer(1, 0);

    /// <summary>
    /// Moves the player up if possible.
    /// </summary>
    public void OnUp(object sender, EventArgs args)
        => MovePlayer(0, -1);

    /// <summary>
    /// Moves the player down if possible.
    /// </summary>
    public void OnDown(object sender, EventArgs args)
        => MovePlayer(0, 1);
}
