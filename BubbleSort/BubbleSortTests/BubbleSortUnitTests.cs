namespace BubbleSortTests;

using NUnit.Framework;
using BubbleSort;

public class Tests
{
    
    [TestCaseSource(nameof(ListSortCasesThatShouldNotFail))]
    public void BubbleSortTestThatShouldNotFail<T>(Comparer<T> comparer, List<T> list, List<T> expectedList)
    {
        BubbleSort.Sort(list, comparer);
        Assert.That(expectedList, Is.EqualTo(list));
    }

    [TestCaseSource(nameof(ListSortCasesThatShouldThrowNullReferenceException))]
    public void BubbleSortTestThatShouldFailByNullElement<T>(Comparer<T> comparer, List<T> list)
    {
        Assert.That(() => BubbleSort.Sort(list, comparer), Throws.TypeOf<NullReferenceException>());
    }

    private static IEnumerable<object[]> ListSortCasesThatShouldNotFail()
    {
        yield return new object[] { Comparer<int>.Default, new List<int> { 3, 1, 1, 5, 8, 10, 12 }, new List<int> { 1, 1, 3, 5, 8, 10, 12 } };
        yield return new object[] { Comparer<int>.Default, new List<int> { 0, 0, 0, 0, 0, 0 }, new List<int> { 0, 0, 0, 0, 0, 0 } };
        yield return new object[] { Comparer<int>.Default, new List<int> { 12, 10, 8, 5, 0, 0, -6, -13 }, new List<int> { -13, -6, 0, 0, 5, 8, 10, 12 } };
        yield return new object[] { Comparer<int>.Default, new List<int> { 5, 8, 10, 12, 0, -6, -13, 0 }, new List<int> { -13, -6, 0, 0, 5, 8, 10, 12 } };
        yield return new object[] { Comparer<int>.Default, new List<int> { 10, 10, 10, 10, 10 }, new List<int> { 10, 10, 10, 10, 10 } };
        yield return new object[] { Comparer<int>.Default, new List<int> { -10, -10, -10, -10, -10 }, new List<int> { -10, -10, -10, -10, -10 } };
        yield return new object[] { Comparer<int>.Default, new List<int> { 3, 1, 1, 5, 8, 10, 12 }, new List<int> { 1, 1, 3, 5, 8, 10, 12 } };
        yield return new object[] { Comparer<string>.Default, new List<string> { "abc", "a", "aa", "ab", "", "asdl", "b" }, new List<string> { "", "a", "aa", "ab", "abc", "asdl", "b" } };
        yield return new object[] { Comparer<string>.Default, new List<string> { "", "", "", "", "" }, new List<string> { "", "", "", "", "" } };
        yield return new object[] { Comparer<int>.Default, new List<int>(), new List<int>() };
    }
    
    private static IEnumerable<object[]> ListSortCasesThatShouldThrowNullReferenceException()
    {
        yield return new object[] { Comparer<object>.Default, new List<object> { null, null, null } };
        yield return new object[] { Comparer<object>.Default, new List<object> { null } };
    }
}