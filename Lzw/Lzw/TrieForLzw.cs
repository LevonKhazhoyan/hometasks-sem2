namespace Lzw;

public class TrieForLzw
{
    private int prefixCount;
    private readonly TrieElement head;
    private TrieElement pointer;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="TrieForLzw"/> class
    /// </summary>
    public TrieForLzw()
    {
        prefixCount = 1;
        head = new TrieElement();
        pointer = head;
    }
    
    /// <summary>
    /// Saves node in file
    /// </summary>
    public void SaveTrieElement(byte value, FileStream file)
    {
        var writeValue = pointer.HasParent() ? pointer.Value : 0;
        var bytes = BitConverter.GetBytes(writeValue);
        Array.Resize(ref bytes, (int) Math.Ceiling(Math.Log2(prefixCount) / 8));
        file.Write(bytes);
        file.WriteByte(value);
    }

    /// <summary>
    /// Saves node count
    /// </summary>
    public void SaveTrieCount(FileStream file)
        => file.Write(BitConverter.GetBytes(prefixCount));

    /// <summary>
    /// Sets the pointer back to the head
    /// </summary>
    public void ResetCursor()
        => pointer = head;
    
    /// <summary>
    /// Adds child if current <see cref="TrieElement"/> doesn't have one
    /// </summary>
    public bool TryAdd(byte value)
    {
        if (pointer.HasChild(value))
        {
            pointer = pointer.GetChild(value);
            return false;
        }
        pointer.AddChild(prefixCount, value);
        prefixCount++;
        return true;
    }
    
    /// <summary>
    /// Element of trie containing code and dictionary of children
    /// </summary>
    private class TrieElement
    {
        public int Value { get; set; }
        private TrieElement? parent;
        private readonly Dictionary<byte, TrieElement> children;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrieElement"/> class
        /// </summary>
        public TrieElement()
            => children = new Dictionary<byte, TrieElement>();

        /// <summary>
        /// Adds child
        /// </summary>
        public void AddChild(int value, byte keyByte)
        {
            var newChild = new TrieElement
            {
                parent = this,
                Value = value
            };
            children.Add(keyByte, newChild);
        }

        /// <summary>
        /// Returns true if current <see cref="TrieElement"/> instance has a child
        /// </summary>
        public bool HasChild(byte keyByte)
            => children.ContainsKey(keyByte);

        /// <summary>
        /// Returns child of current <see cref="TrieElement"/> instance
        /// </summary>
        public TrieElement GetChild(byte keyByte)
            => children[keyByte];

        /// <summary>
        /// Returns true if current <see cref="TrieElement"/> instance has a parent
        /// </summary>
        public bool HasParent()
            => parent != null;
    }
}
