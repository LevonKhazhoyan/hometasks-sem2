namespace SparseVector;

public class SparseVector
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

    public SparseVector(int numOfRows)
    {
        this.numOfRows = numOfRows;
        elements = new Dictionary<int, int>();
    }
    
    public SparseVector(int numOfRows, Dictionary<int, int> elements)
    {
        this.numOfRows = numOfRows;
        this.elements = elements;
    }

    public void Add(int row, int value)
    {
        if (elements.ContainsKey(row))
        {
            elements[row] = value;
        }
        else
        {
            elements.Add(row, value);
        }
    }
    
    public static SparseVector Sum(SparseVector first, SparseVector second)
    {
        if (first.numOfRows != second.numOfRows)
        {
            throw new NotSupportedException();
        }

        var vector = new SparseVector(first.numOfRows);

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

    public static SparseVector Subtract(SparseVector first, SparseVector second)
    {
        if (first.numOfRows != second.numOfRows)
        {
            throw new NotSupportedException();
        }

        var vector = new SparseVector(first.numOfRows);

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

    public static SparseVector MultiplyByScalar(SparseVector vector, int scalar)
    {
        foreach (var key in vector.elements.Keys)
        {
            vector.elements[key] *= scalar;
        }

        return vector;
    }

    public static bool checkOnNullVector(SparseVector vector)
        => vector.elements.Count == 0;

}