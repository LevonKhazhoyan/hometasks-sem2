namespace BWT;
/// <summary>
/// Suffix Array class
/// </summary>
public class SuffixArray
{
    public readonly int[] sufArray;
    private readonly Suffix startString; 

    /// <summary>
    /// Constructor of <see cref="SuffixArray"/>
    /// </summary>
    public SuffixArray(String s)
    {

        var n = s.Length;
        var suffix = new Suffix[n];
        
        for (var i = 0; i < n; i++)
        {
            suffix[i] = new Suffix(i, s[i] - s[n-1], 0);
        }

        for (var i = 0; i < n; i++)
            suffix[i].next = i + 1 < n ? suffix[i + 1].rank : -1;

        Array.Sort(suffix);
        var index = new int[n];

        for (var length = 4; length < 2 * n; length <<= 1)
        {
            var rank = 0;
            var prev = suffix[0].rank;
            suffix[0].rank = rank;
            index[suffix[0].index] = 0;
            for (var i = 1; i < n; i++)
            {
                if (suffix[i].rank == prev && suffix[i].next == suffix[i - 1].next)
                {
                    prev = suffix[i].rank;
                    suffix[i].rank = rank;
                }
                else
                {
                    prev = suffix[i].rank;
                    suffix[i].rank = ++rank;
                }

                index[suffix[i].index] = i;
            }

            for (int i = 0; i < n; i++)
            {
                var nextP = suffix[i].index + length / 2;
                suffix[i].next = nextP < n ? suffix[index[nextP]].rank : -1;
            }

            Array.Sort(suffix);
        }

        startString = suffix[0];
        sufArray = new int[n];

        for (var i = 0; i < n; i++)
            sufArray[i] = suffix[i].index;
    }

    /// <summary>
    /// Suffix class for <see cref="SuffixArray"/> realization
    /// </summary>
    class Suffix : IComparable<Suffix>
    {
        public readonly int index;
        public int rank;
        public int next;

        /// <summary>
        /// Constructor of <see cref="Suffix(int, int, int)"/> class instance
        /// </summary>
        public Suffix(int index, int rank, int next)
        {
            this.index = index;
            this.rank = rank;
            this.next = next;
        }

        /// <summary>
        /// Compare method for <see cref="T:IComparable(Suffix)"/> realization
        /// </summary>
        public int CompareTo(Suffix? that)
        {
            return rank != that?.rank ? rank.CompareTo(that?.rank) : rank.CompareTo(that.next);
        }
    }
}
