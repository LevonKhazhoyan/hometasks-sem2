namespace Lzw;

/// <summary>
/// Utils class for compress and decompress files by Lzw algorithm
/// </summary>
public static class TrieForLzwUtils
{
    /// <summary>
    /// Saves node in file
    /// </summary>
    public static void SaveTrieElement(TrieForLzw trie, byte value, FileStream file)
    {
        var writeValue = trie.Pointer.HasParent() ? trie.Pointer.Value : 0;
        var bytes = BitConverter.GetBytes(writeValue);
        Array.Resize(ref bytes, (int) Math.Ceiling(Math.Log2(trie.PrefixCount) / 8));
        file.Write(bytes);
        file.WriteByte(value);
    }

    /// <summary>
    /// Saves node count
    /// </summary>
    public static void SaveTrieCount(TrieForLzw trie, FileStream file)
        => file.Write(BitConverter.GetBytes(trie.PrefixCount));

}