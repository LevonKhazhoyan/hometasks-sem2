using InsertionSort;
using NUnit.Framework;

namespace InsertionSortTestProj;

[TestFixture]
public class InsertionSortTests
{
    [TestCaseSource(nameof(SortTestCases))]
    public void Sort(int[] bunchOfNumbers)
    {
        var expectedArray = bunchOfNumbers.ToArray();
        Array.Sort(expectedArray);
        CollectionAssert.AreEqual(expectedArray, InsertionSortProj.InsertionSort(bunchOfNumbers));
    }

    private static int[] CreateIntArray()
    {
        var random = new Random();
        var numbers = Enumerable.Range(1, random.Next(1, 400)).OrderBy(_ => random.Next()).ToArray();
        return numbers;
    }
    
    private static IEnumerable<int[]> SortTestCases()
    {
        yield return new [] { 6, 2, 9, 20, 130 };
        yield return new [] { -2, 5, 0, -16, 100 };
        yield return new [] { -2, -5, -3, -16, -100 };
        yield return Array.Empty<int>();
        yield return CreateIntArray();
    }
}