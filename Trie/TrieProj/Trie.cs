namespace TrieProj;

/// <summary>
///  Trie, also called prefix tree, data structure used for store associative array,
///  with keys that usually are strings, by individual chars.
/// </summary>
public class Trie
{
    private readonly TrieNode root = new();

    /// <summary>
    /// Adds a word by character
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
                current.Children.Add(character, new TrieNode());
                current = current.Children[character]; 
            }
            current.PrefixCount += 1;
        }

        current.IsWord = true;
    }

    /// <summary>
    /// Checks if contains word
    /// </summary>
    public bool Contains(string word)
    {
        var current = root;

        foreach (var character in word.ToCharArray())
        {
            if (!current.Children.ContainsKey(character))
            {
                return false;
            }
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
        Remove(root, word, 0);
    }

    /// <summary>
    /// Recursive method for removing word
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
        
        current.PrefixCount--;
        
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
    /// Counts how many words starts with prefix
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
        public readonly Dictionary<char, TrieNode> Children;
        public bool IsWord { get; set; }
        public int PrefixCount { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public TrieNode()
        {
            Children = new Dictionary<char, TrieNode>();
            IsWord = false;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public TrieNode(Dictionary<char, TrieNode> children, bool isWord)
        {
            Children = children;
            IsWord = isWord;
        }
    }
}
