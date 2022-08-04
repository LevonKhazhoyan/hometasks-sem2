using TrieProj;
using NUnit.Framework;

namespace UnitTrieTests;

public class UnitTests
{
    private readonly Trie _trie = new Trie();
    
    [SetUp]
    public void Setup()
    {
        _trie.Add("test");
    }

    
    [Test]
    public void ContainsTest()
    {
        Assert.True(_trie.Contains("test"));
        Assert.False(_trie.Contains("false test"));
    }
    
    [Test]
    public void AddTest()
    {
        Assert.False(_trie.Contains("add test"));
        _trie.Add("add test");
        Assert.True(_trie.Contains("add test"));
    }
    
    [Test]
    public void RemoveTest()
    {
        Assert.True(_trie.Contains("test"));
        _trie.Remove("test");
        Assert.False(_trie.Contains("test"));
    }
    
    [Test]
    public void HowManyStartsWithPrefixTest()
    {
        Assert.That(_trie.HowManyStartsWithPrefix("t"), Is.EqualTo(1));
        _trie.Add("test2");
        Assert.That(_trie.HowManyStartsWithPrefix("t"), Is.EqualTo(2));
        _trie.Add("dest2");
        Assert.That(_trie.HowManyStartsWithPrefix("t"), Is.EqualTo(2));
    }
}