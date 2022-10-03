namespace ListFunctionsTests;

using ListFunctions;
using NUnit.Framework;
using System;
using System.Collections.Generic;

public class ListFunctionsUnitTests
{
    [TestCaseSource(nameof(MapDataCases))]
    public void MapTest<TValue, TResult>(List<TValue> list, Func<TValue, TResult> function, List<TResult> expectedList)
    {
        Assert.That(expectedList, Is.EqualTo(ListFunctions.Map(list, function)));
    }

    [TestCaseSource(nameof(FilterDataCases))]
    public void FilterTest<TValue>(List<TValue> list, Func<TValue, bool> function, List<TValue> expectedList)
    {
        Assert.That(expectedList, Is.EqualTo(ListFunctions.Filter(list, function)));
    }

    [TestCaseSource(nameof(FoldDataCases))]
    public void FoldTest<TValue, TResult>(List<TValue> list, Func<TResult, TValue, TResult> function, TResult accumulator, TResult expectedResult)
    {
        Assert.That(expectedResult, Is.EqualTo(ListFunctions.Fold(list, accumulator, function)));
    }

    private static IEnumerable<object[]> MapDataCases()
    {
        yield return new object[] { new List<int> { 0, 1, 2, 3, 4, 5 }, new Func<int, int>(x => x * 2), new List<int> { 0, 2, 4, 6, 8, 10 } };
        yield return new object[] { new List<int> { 0, 1, 2, 3, 4, 5 }, new Func<int, int>(x => x % 2), new List<int> { 0, 1, 0, 1, 0, 1 } };
        yield return new object[] { new List<string> { "0", "1", "2", "3", "4", "5" }, new Func<string, int>(x => int.Parse(x)), new List<int> { 0, 1, 2, 3, 4, 5 } };
        yield return new object[] { new List<string> { "0", "1", "2", "3", "4", "5" }, new Func<string, int>(x => int.Parse(x) * 2), new List<int> { 0, 2, 4, 6, 8, 10 } };
    }

    private static IEnumerable<object[]> FilterDataCases()
    {
        yield return new object[] { new List<int> { -3, 0, 2, -2, -1, 1 }, new Func<int, bool>(x => x < 0), new List<int> { -3, -2, -1 } };
        yield return new object[] { new List<int> { 0, 1, 2, 3, 4, 5 }, new Func<int, bool>(x => x % 2 == 0), new List<int> { 0, 2, 4} };
        yield return new object[] { new List<string> { "000", "11", "22", "33", "4", "5555" }, new Func<string, bool>(x => x.Length == 2), new List<string> { "11", "22", "33" } };
    }

    private static IEnumerable<object[]> FoldDataCases()
    {
        yield return new object[] { new List<int> { 1, 2, 3, 4, 5, 6 }, new Func<int, int, int>((x, y) => x * y), 1, 720 };
        yield return new object[] { new List<int> { 1, 2, 3, 4, 5, 6 }, new Func<int, int, int>((x, y) => x + y), 0, 21 };
        yield return new object[] { new List<int> { 1, 2, 31, 42 }, new Func<string, int, string>((x, y) => x + $"{y}"), "a", "a123142" };
    }
}
