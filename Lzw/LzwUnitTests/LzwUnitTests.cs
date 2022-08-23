using Lzw;

namespace LzwUnitTests;
using NUnit.Framework;

public class LzwTests
{   
    private const string pathTxt1 = "..\\..\\..\\test1.txt";
    private const string pathTxt2 = "..\\..\\..\\expectedTest1.txt"; 
    private const string pathExe1 = "..\\..\\..\\MyGamesLoader.exe";
    private const string pathExe2 = "..\\..\\..\\test.exe";
    
    private static bool FilesAreEqual(string path1, string path2)
    {
        using var file1 = File.OpenRead(path1);
        using var file2 = File.OpenRead(path2);
        while (true)
        {
            var firstFileByte = file1.ReadByte();
            var secondFileByte = file2.ReadByte();

            if (firstFileByte == -1 && secondFileByte == -1)
            {
                break;
            }
            if (firstFileByte != secondFileByte)
            {
                return false;
            }
        }
        return true;
    }

    [SetUp]
    public void SetUp()
    {
        File.Delete(pathTxt2);
        File.Delete(pathTxt2 + ".zipped");
        File.Delete(pathExe2); 
        File.Delete(pathExe2 + ".zipped");
    }
    
    [Test]
    public void TestCorrectCompressDecompressTxt()
    {
        File.Copy(pathTxt1, pathTxt2);
        LzwMethods.Compress(pathTxt2);
        File.Delete(pathTxt2);
        LzwMethods.Decompress(pathTxt2 + ".zipped");
        Assert.IsTrue(FilesAreEqual(pathTxt1, pathTxt2));
    }
    
    [Test]
    public void TestCorrectCompressDecompressExe()
    {
        File.Copy(pathExe1, pathExe2);
        LzwMethods.Compress(pathExe2);
        File.Delete(pathExe2);
        LzwMethods.Decompress(pathExe2 + ".zipped"); 
        Assert.IsTrue(FilesAreEqual(pathExe1, pathExe2));
    }
}