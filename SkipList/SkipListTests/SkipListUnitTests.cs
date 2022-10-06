using SkipList;
using NUnit.Framework;

namespace SkipListTests;

public class Tests
{
    private SkipList<int> skipList;
    [SetUp]
    public void Setup()
    {
        skipList = new(5);
        skipList.Add(10);
        skipList.Add(20);
        skipList.Add(30);
        skipList.Add(40);
        skipList.Add(50);
        skipList.Add(60);
    }

    [Test]
    public void AddTest()
    {
        Assert.IsFalse(skipList.Contains(100));
        skipList.Add(100);
        Assert.IsTrue(skipList.Contains(100));
    }
    
    [Test]
    public void RemoveAtTest()
    {
        Assert.IsTrue(skipList.Contains(40));
        skipList.RemoveAt(3);
        Assert.IsFalse(skipList.Contains(40));
    }
    
    [Test]
    public void RemoveAndAddCountTest()
    {
        Assert.AreEqual(6, skipList.Count);
        skipList.Add(70);
        Assert.AreEqual(7, skipList.Count);
        skipList.Remove(50);
        Assert.AreEqual(6, skipList.Count);
    }
    
    [Test]
    public void CopyToTest()
    {
        var array = new int[6];
        skipList.CopyTo(array, 0);
        Assert.AreEqual(30, array[2]);
    }
    
    
    [Test]
    public void ClearTest()
    {
        skipList.Clear();
        Assert.AreEqual(0, skipList.Count);
        Assert.IsFalse(skipList.Contains(40));
    }
    
    
}