namespace Lzw;

/// <summary>
/// Class for Lzw methods
/// </summary>
public static class LzwMethods
{
    /// <summary>
    /// Compresses file using Lzw algorithm
    /// </summary>
    /// <param name="path">Path to file</param>
    public static void Compress(string path) 
    {
        var compressedPath = path + ".zipped";
        using var fileRead = File.OpenRead(path);
        using var fileWrite = File.OpenWrite(compressedPath);
        var trie = new TrieForLzw();
        for (var charIndex = 0; charIndex < fileRead.Length; charIndex++)
        {
            var valueByte = (byte) fileRead.ReadByte();
            if (charIndex == fileRead.Length - 1)
            {
                TrieForLzwUtils.SaveTrieElement(trie, valueByte, fileWrite);
            }
            else if (trie.TryAdd(valueByte))
            {
                TrieForLzwUtils.SaveTrieElement(trie, valueByte, fileWrite);
                trie.ResetCursor();
            }
        }
        TrieForLzwUtils.SaveTrieCount(trie, fileWrite);      
    }

    /// <summary>
    /// Decompresses .zipped file
    /// </summary>
    /// <param name="path">Path to .zipped file</param>
    public static void Decompress(string path)
    {
        using var fileRead = File.OpenRead(path);
        using var fileWrite = File.OpenWrite(path.Remove(path.Length - 7));
        fileRead.Seek(-4, SeekOrigin.End);
        var sizeByte = new []{
            (byte)fileRead.ReadByte(),
            (byte)fileRead.ReadByte(),
            (byte)fileRead.ReadByte(),
            (byte)fileRead.ReadByte()};
        fileRead.Seek(0, SeekOrigin.Begin);
        var dictionary = new byte[BitConverter.ToInt32(sizeByte)][];

        for (var i = 0; i < dictionary.GetLength(0); i++)
        {
            var bytesLength = (int)Math.Ceiling(Math.Log2(i + 2) / 8);
            var byteValues = new byte[4];
            for (var j = 0; j < 4; j++)
            {
                byteValues[j] = j < bytesLength ? (byte)fileRead.ReadByte() : (byte)0;
            }
            var value = BitConverter.ToInt32(byteValues);
            var character = (byte)fileRead.ReadByte();
            if (value == 0)
            {
                dictionary[i] = new []{character};
                fileWrite.WriteByte(character);
            }
            else
            {
                dictionary[i] = new byte[dictionary[value - 1].Length + 1];
                for (var j = 0; j < dictionary[i].Length; j++)
                {
                    dictionary[i][j] = j != dictionary[i].Length - 1 ? dictionary[value - 1][j] : character;
                    fileWrite.WriteByte(dictionary[i][j]);
                }
            }
        }
    }
}
