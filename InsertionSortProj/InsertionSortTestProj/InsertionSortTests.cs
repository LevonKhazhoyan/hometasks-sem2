using InsertionSort;
using NUnit.Framework;

namespace InsertionSortTestProj;

[TestFixture]
public class InsertionSortTests
{
    private readonly Random _rnd = new();
    
    [Test]
    public void RandomArraySort()
    {
        CompareInsertionSortWithSystemSort(CreateIntArr());
    }

    [Test]
    public void PositiveNumbersSort()
    {
        CompareInsertionSortWithSystemSort(new [] { 6, 2, 9, 20, 130 });
    }
    
    [Test]
    public void NegativeAndPositiveNumbersSort()
    {
        CompareInsertionSortWithSystemSort(new [] { -2, 5, 0, -16, 100 });
    }

    [Test]
    public void JustNegativeNumbersSort()
    {
        CompareInsertionSortWithSystemSort(new [] { -2, -5, -3, -16, -100 });
    }

    [Test]
    public void EmptyListSort()
    {
        CompareInsertionSortWithSystemSort(Array.Empty<int>());
    }

    private void CompareInsertionSortWithSystemSort(int[] arr)
    {
        var expectedArr = arr.ToArray();
        Array.Sort(expectedArr);
        CollectionAssert.AreEqual(expectedArr, InsertionSortProj.InsertionSort(arr));
    }

    private int[] CreateIntArr()
    {
        var numbers = Enumerable.Range(1, _rnd.Next(1,400)).OrderBy(_ => _rnd.Next()).ToArray();
        return numbers;
    }
}