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
    [TestCase(new [] { 6, 2, 9, 20, 130 }, TestName="PositiveNumbersSort" )]
    [TestCase(new [] { -2, 5, 0, -16, 100 }, TestName="NegativeAndPositiveNumbersSort" )]
    [TestCase(new [] { -2, -5, -3, -16, -100 }, TestName="JustNegativeNumbersSort" )]
    [TestCase(new int[] {}, TestName="EmptyArraySort" )]
    public void Sort(int[] bunchOfNumbers)
    {
        CompareInsertionSortWithSystemSort(bunchOfNumbers);
    }

    private static void CompareInsertionSortWithSystemSort(int[] inputArray)
    {
        var expectedArray = inputArray.ToArray();
        Array.Sort(expectedArray);
        CollectionAssert.AreEqual(expectedArray, InsertionSortProj.InsertionSort(inputArray));
    }

    private int[] CreateIntArr()
    {
        var numbers = Enumerable.Range(1, _rnd.Next(1, 400)).OrderBy(_ => _rnd.Next()).ToArray();
        return numbers;
    }
}