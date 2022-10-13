namespace TrieProj;

/// <summary>
///  Trie, also called prefix tree, data structure used for store associative array,
///  with keys that usually are strings, by individual chars.
/// </summary>
public class Trie
{
    private readonly TrieNode root;

    /// <summary>
    /// Constructor of <see cref="Trie"/> class instance 
    /// </summary>
    public Trie()
    {
        root = new TrieNode();
    }

    /// <summary>
    /// Adds a word in instance of <see cref="Trie"/> class by character
    /// </summary>
    public void Add(string word)
    {
        var current = root;

        foreach (var character in word.ToCharArray())
        {
            if (current.Children.ContainsKey(character))
            {
                current = current.Children[character];
            }
            else
            {
                current.Children.TryAdd(character, new TrieNode());
                current = current.Children[character]; 
            }
            current.PrefixCount += 1;
        }

        current.IsWord = true;
    }

    /// <summary>
    /// Checks if instance of <see cref="Trie"/> class contains word
    /// </summary>
    public bool Contains(string word)
    {
        var current = root;

        foreach (var character in word.ToCharArray())
        {
            if (!current.Children.ContainsKey(character))
                return false;
            var node = current.Children[character];
            current = node;
        }

        return current.IsWord;
    }

    /// <summary>
    /// Auxiliary for <see cref="Remove(TrieNode, string, int)"/> method
    /// </summary>
    public void Remove(string word)
    {
        if (!Remove(root, word, 0))
        {
            return;
        }

        var current = root;
        foreach (var character in word.ToCharArray())
        {
            current.PrefixCount--;
            if (!current.Children.ContainsKey(character))
            {
                return;
            }
            var node = current.Children[character];
            current = node;
        }
    }

    /// <summary>
    /// Recursive method for removing word from <see cref="Trie"/> class instance
    /// </summary>
    private static bool Remove(TrieNode current, string word, int index)
    {
        if (index == word.Length)
        {
            if (!current.IsWord) 
            {
                return false;
            }
            current.IsWord = false;
            return current.Children.Count == 0;
        }
        
        var character = word[index];
        
        if (!current.Children.ContainsKey(character))
        {
            return false;
        }
        
        var node = current.Children[character];
        var shouldDeleteCurrentNode = Remove(node, word, index + 1) && !node.IsWord;
        if (!shouldDeleteCurrentNode) 
        {
            return false;
        }
        
        current.Children.Remove(character);
        return current.Children.Count == 0;
    }

    /// <summary>
    /// Counts how many words starts with prefix in <see cref="Trie"/> class instance
    /// </summary>
    public int HowManyStartsWithPrefix(string prefix)
    {
        var current = root;
        
        foreach (var character in prefix.ToCharArray())
        {
            if (!current.Children.ContainsKey(character))
            {
                return 0;
            }
            var node = current.Children[character];
            current = node;
        }
        
        return current.PrefixCount;
    }

    /// <summary>
    /// Node just like in tree class
    /// </summary>
    private class TrieNode
    {
        private Dictionary<char, TrieNode> children;
        public Dictionary<char, TrieNode> Children
        {
            get => children;
            set => children = value ?? throw new ArgumentNullException(nameof(value));
        }
        
        public bool IsWord { get; set; }
        
        private int prefixCount;
        public int PrefixCount { get; set; }

        /// <summary>
        /// Constructor of <see cref="TrieNode"/> class instance 
        /// </summary>
        public TrieNode()
        {
            IsWord = false;
        }

        /// <summary>
        /// Constructor of <see cref="TrieNode"/> class instance 
        /// </summary>
        public TrieNode(Dictionary<char, TrieNode> children, bool isWord)
        {
            Children = children;
            IsWord = isWord;
        }
    }
}
