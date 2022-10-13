using TrieProj;
using NUnit.Framework;

namespace UnitTrieTests;

public class UnitTests
{
    private Trie _trie;
    
    [SetUp]
    public void Setup()
    {
        _trie = new Trie();
        _trie.Add("test");
    }

    [Test]
    public void HowManyStartsWithPrefixTest()
    {
        Assert.That(_trie.HowManyStartsWithPrefix("l"), Is.EqualTo(0));
        _trie.Add("led");
        Assert.That(_trie.HowManyStartsWithPrefix("l"), Is.EqualTo(1));
        _trie.Add("lid");
        _trie.Remove("lid");
        Assert.That(_trie.HowManyStartsWithPrefix("l"), Is.EqualTo(2));
        Assert.That(_trie.HowManyStartsWithPrefix("li"), Is.EqualTo(1));
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
        Assert.False(_trie.Contains("t"));
    }
}