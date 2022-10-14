using NuGet.Frameworks;
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
        Assert.Multiple(() =>
        {
            Assert.That(_trie.HowManyStartsWithPrefix("l"), Is.EqualTo(1));
            Assert.That(_trie.HowManyStartsWithPrefix("li"), Is.EqualTo(0));
        });
    }
    
    [Test]
    public void ContainsTest()
    {
        Assert.Multiple(() =>
        {
            Assert.That(_trie.Contains("test"));
            Assert.That(_trie.Contains("false test"), Is.EqualTo(false));
        });
    }
    
    [Test]
    public void AddTest()
    {
        Assert.That(_trie.Contains("add test"), Is.EqualTo(false));
        _trie.Add("add test");
        Assert.That(_trie.Contains("add test"));
    }
    
    [Test]
    public void RemoveTest()
    {
        Assert.That(_trie.Contains("test"), Is.EqualTo(true));
        _trie.Remove("test");
        Assert.Multiple(() =>
        {
            Assert.That(_trie.Contains("test"), Is.EqualTo(false));
            Assert.That(_trie.Contains("t"), Is.EqualTo(false));
        });
    }
}