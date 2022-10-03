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
    public static int[] InsertionSort(int[] inputArray)
    {
        for (var sortedLength = 0; sortedLength < inputArray.Length; sortedLength++)
        {
            for (var k = 0; k < sortedLength; k++)
            {
                var i = sortedLength - k;
                if (inputArray[i] < inputArray[i - 1])
                {
                    (inputArray[i], inputArray[i - 1]) = (inputArray[i - 1], inputArray[i]);
                } 
                else
                {
                    break;
                }
            }
        }
        return inputArray;
    }
}
