namespace BWT;
using NUnit.Framework;

public class BwtUnitTest
{
    [Test]
    public void TransformAndInverse()
    {
        Assert.That(new string(BwtProj.InverseTransform(BwtProj.BwTransformation(new string("banana$")))), Is.EqualTo("banana$"));
    }
    
    [Test]
    public void SimpleBwtTest()
    {
        Assert.That(BwtProj.BwTransformation("banana$"), Is.EqualTo(new Tuple<char[], int>("annb$aa".ToCharArray(), 4)));
    }
}