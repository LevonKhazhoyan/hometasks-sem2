namespace BWT;

using NUnit.Framework;

public class BwtUnitTest
{
    [Test]
    public void TransformAndInverse()
        => Assert.That(new string(Bwt.InverseTransform(Bwt.BwTransformation(new string("banana")))), Is.EqualTo("banana"));
    
    [Test]
    public void SimpleBwtTest() 
        => Assert.That(Bwt.BwTransformation("banana^"), Is.EqualTo(("annb^aa".ToCharArray(), 4)));
}