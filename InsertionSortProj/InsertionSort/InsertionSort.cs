namespace InsertionSort;

/// <summary>
/// Insertion sort - sorting algorithm in which the elements of the input sequence are examined one at a time,
/// and each new incoming element is placed in a suitable place among the previously sorted elements.
/// </summary>
public static class InsertionSortProj
{
    /// <summary>
    /// Insertion sort method
    /// </summary>
    public static int[] InsertionSort(int[] arr)
    {
        for (var sortedLen = 0; sortedLen < arr.Length; sortedLen++)
        {
            for (var k = 0; k < sortedLen; k++)
            {
                var i = sortedLen - k;
                if (arr[i] < arr[i - 1])
                {
                    (arr[i], arr[i - 1]) = (arr[i - 1], arr[i]);
                } else
                {
                    break;
                }
            }
        }
        return arr;
    }
}
