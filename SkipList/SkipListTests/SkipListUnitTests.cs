namespace SkipListTests;

using SkipList;
using NUnit.Framework;

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
        Does.Not.Contain(skipList.Contains(100));
        skipList.Add(100);
        Does.Contain(skipList.Contains(100));
    }
    
    [Test]
    public void RemoveAtTest()
    {
        Does.Contain(skipList.Contains(40));
        skipList.RemoveAt(3);
        Does.Not.Contain(skipList.Contains(40));
    }
    
    [Test]
    public void RemoveAndAddCountTest()
    {
        Assert.That(skipList, Has.Count.EqualTo(6));
        skipList.Add(70);
        Assert.That(skipList, Has.Count.EqualTo(7));
        skipList.Remove(50);
        Assert.That(skipList, Has.Count.EqualTo(6));
    }
    
    [Test]
    public void CopyToTest()
    {
        var array = new int[6];
        skipList.CopyTo(array, 0);
        Assert.That(array[2], Is.EqualTo(30));
    }
    
    [Test]
    public void ClearTest()
    {
        skipList.Clear();
        Assert.That(skipList, Has.Count.EqualTo(0));
        Does.Not.Contain(skipList.Contains(40));
    }
}