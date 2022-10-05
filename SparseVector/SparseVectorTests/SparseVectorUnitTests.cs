using SparseVector;

namespace SparseVectorTests;

public class Tests
{
    private static readonly Dictionary<int,int> expectedSumResult = new ()
    {
        { 0, 1},
        { 3, 7},
        { 2, 4}
    };
    
    private static readonly Dictionary<int,int> expectedSubtractResult = new ()
    {
        { 0, 1},
        { 3, -1},
        { 2, -4}
    };
    
    private static readonly Dictionary<int,int> expectedMultiplyByScalarResult = new ()
    {
        { 0, 5},
        { 3, 15},
    };

    [Test]
    public void SumTest()
    {
        var first = new Vector(4);
        var second = new Vector(4);
        first.Add(0, 1);
        first.Add(3, 3);
        second.Add(3, 4);
        second.Add(2, 4);
        var result = Vector.Sum(first, second);
        Assert.AreEqual(expectedSumResult, result.Elements);
    }
    
    [Test]
    public void SubtractTest()
    {
        var first = new Vector(4);
        var second = new Vector(4);
        first.Add(0, 1);
        first.Add(3, 3);
        second.Add(3, 4);
        second.Add(2, 4);
        var result = Vector.Subtract(first, second);
        Assert.AreEqual(expectedSubtractResult, result.Elements);
    }

    [Test] 
    public void MultiplyByScalarTest()
    {
        var vector = new Vector(4);
        vector.Add(0, 1);
        vector.Add(3, 3);
        var result = Vector.MultiplyByScalar(vector, 5);
        Assert.AreEqual(expectedMultiplyByScalarResult, result.Elements);
    }
    
    [Test] 
    public void CannotAddZero()
    {
        var vector = new Vector(4);
        Assert.Throws<ArgumentException>(() => vector.Add(3, 0));
    }
    
    [Test] 
    public void CannotNegativeRowVector()
    {
        var vector = new Vector(4);
        Assert.Throws<ArgumentException>(() => vector.Add(-5, 3));
    }
}