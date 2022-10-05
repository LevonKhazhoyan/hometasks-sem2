namespace SparseVector;

/// <summary>
/// Sparse vector represented by dictionary with row, value key-pair
/// </summary>
public class Vector
{
    private int numOfRows;
    public int NumOfRows
    {
        get => numOfRows;
        set => numOfRows = value;
    }
    private Dictionary<int, int> elements;
    public Dictionary<int, int> Elements
    {
        get => elements;
        set => elements = value ?? throw new ArgumentNullException(nameof(value));
    }

    public Vector(int numOfRows)
    {
        this.numOfRows = numOfRows;
        elements = new Dictionary<int, int>();
    }
    
    public Vector(int numOfRows, Dictionary<int, int> elements)
    {
        this.numOfRows = numOfRows;
        this.elements = elements;
    }

    /// <summary>
    /// Add element to vector, or, if presented: replace it
    /// </summary>
    /// <param name="row"></param>
    /// <param name="value"></param>
    public void Add(int row, int value)
    {
        if (row >= numOfRows || row < 0 || value == 0)
        {
            throw new ArgumentException();
        }
        
        if (elements.ContainsKey(row))
        {
            elements[row] = value;
        }
        
        else
        {
            elements.Add(row, value);
        }
    }
    
    /// <summary>
    /// Sum two vectors by coordinates
    /// </summary>
    /// <param name="first">first vector</param>
    /// <param name="second">second vector</param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public static Vector Sum(Vector first, Vector second)
    {
        if (first.numOfRows != second.numOfRows)
        {
            throw new NotSupportedException();
        }

        var vector = new Vector(first.numOfRows);

        foreach (var element in first.elements)
        {
            vector.elements.Add(element.Key, element.Value);
        }

        foreach (var element in second.elements)
        {
            if (vector.elements.ContainsKey(element.Key))
            {
                vector.elements[element.Key] += element.Value;
            }
            else
            {
                vector.elements.Add(element.Key, element.Value);
            }
        }

        return vector;
    }

    /// <summary>
    /// Subtracts two vectors by coordinates
    /// </summary>
    /// <param name="first">first vector</param>
    /// <param name="second">second vector</param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public static Vector Subtract(Vector first, Vector second)
    {
        if (first.numOfRows != second.numOfRows)
        {
            throw new NotSupportedException();
        }

        var vector = new Vector(first.numOfRows);

        foreach (var element in first.elements)
        {
            vector.elements.Add(element.Key, element.Value);
        }

        foreach (var element in second.elements)
        {
            if (vector.elements.ContainsKey(element.Key))
            {
                vector.elements[element.Key] -= element.Value;
            }
            else
            {
                vector.elements.Add(element.Key, -element.Value);
            }
        }

        return vector;
    }

    /// <summary>
    /// Multiply vector by scalar by coordinates
    /// </summary>
    /// <param name="vector">given vector</param>
    /// <param name="scalar">given scalar</param>
    /// <returns></returns>
    public static Vector MultiplyByScalar(Vector vector, int scalar)
    {
        foreach (var key in vector.elements.Keys)
        {
            vector.elements[key] *= scalar;
        }

        return vector;
    }
    
    /// <summary>
    /// Checks if vector is null
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    public static bool isNull(Vector vector)
        => vector.elements.Count == 0;

}