namespace BubbleSort;

/// <summary>
/// Static Bubble sort class
/// </summary>
public static class BubbleSort
{
    /// <summary>
    /// Static Bubble sort method 
    /// </summary>
    /// <param name="list"> Generic list with non-nullable parameters</param>>
    /// <param name="comparer"> Comparer for list elements</param>>
    public static void Sort<T>(List<T> list, Comparer<T> comparer)
    {
        for (var i = 0; i < list.Count; i++)
        {
            if (list[i] == null)
            {
                throw new NullReferenceException($"Null element in list with {i} index");
            }
        }

        for (var i = 0; i < list.Count; i++)
        {
            for (var j = 0; j < list.Count - i - 1; j++)
            {
                if (comparer.Compare(list[j], list[j + 1]) > 0)
                {
                    (list[j], list[j+1]) = (list[j + 1], list[j]);
                }
            }
        }
    }
}
