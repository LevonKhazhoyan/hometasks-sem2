namespace BWT;

/// <summary>
/// Suffix Array class
/// </summary>
public class SuffixArray
{
    public int[] ArrayOfSuffixes { get; }
    
    /// <summary>
    /// Constructor of <see cref="SuffixArray"/>
    /// </summary>
    public SuffixArray(string word)
    {
        var wordLength = word.Length;
        var suffix = new Suffix[wordLength];
        
        for (var i = 0; i < wordLength; i++)
        {
            suffix[i] = new Suffix(i, word[i] - word[wordLength - 1], 0);
        }

        for (var i = 0; i < wordLength; i++)
            suffix[i].Next = i + 1 < wordLength ? suffix[i + 1].Rank : -1;

        Array.Sort(suffix);
        var index = new int[wordLength];

        for (var length = 4; length < 2 * wordLength; length <<= 1)
        {
            var rank = 0;
            var previous = suffix[0].Rank;
            suffix[0].Rank = rank;
            index[suffix[0].Index] = 0;
            for (var i = 1; i < wordLength; i++)
            {
                if (suffix[i].Rank == previous && suffix[i].Next == suffix[i - 1].Next)
                {
                    previous = suffix[i].Rank;
                    suffix[i].Rank = rank;
                }
                else
                {
                    previous = suffix[i].Rank;
                    suffix[i].Rank = ++rank;
                }

                index[suffix[i].Index] = i;
            }

            for (var i = 0; i < wordLength; i++)
            {
                var nextP = suffix[i].Index + length / 2;
                suffix[i].Next = nextP < wordLength ? suffix[index[nextP]].Rank : -1;
            }

            Array.Sort(suffix);
        }

        ArrayOfSuffixes = new int[wordLength];

        for (var i = 0; i < wordLength; i++)
            ArrayOfSuffixes[i] = suffix[i].Index;
    }

    /// <summary>
    /// Suffix class for <see cref="SuffixArray"/> realization
    /// </summary>
    private class Suffix : IComparable<Suffix>
    {
        public int Index { get; }
        public int Rank { get; set; }
        public int Next { get; set; }

        /// <summary>
        /// Constructor of <see cref="Suffix(int, int, int)"/> class instance
        /// </summary>
        public Suffix(int index, int rank, int next)
        {
            Index = index;
            Rank = rank;
            Next = next;
        }

        /// <summary>
        /// Compare method for <see cref="T:IComparable(Suffix)"/> realization
        /// </summary>
        public int CompareTo(Suffix? that)
        {
            return Rank != that?.Rank ? Rank.CompareTo(that?.Rank) : Rank.CompareTo(that.Next);
        }
    }
}
