namespace BWT;
/// <summary>
/// Suffix Array class
/// </summary>
public class SuffixArray
{
    public readonly int[] SufArray;
    private readonly Suffix StartString; 

    /// <summary>
    /// Constructor of <see cref="SuffixArray"/>
    /// </summary>
    public SuffixArray(String s)
    {

        var n = s.Length;
        var suffix = new Suffix[n];
        
        for (var i = 0; i < n; i++)
        {
            suffix[i] = new Suffix(i, s[i] - '$', 0);
        }

        for (var i = 0; i < n; i++)
            suffix[i].Next = i + 1 < n ? suffix[i + 1].Rank : -1;

        Array.Sort(suffix);
        var index = new int[n];

        for (var length = 4; length < 2 * n; length <<= 1)
        {
            var rank = 0;
            var prev = suffix[0].Rank;
            suffix[0].Rank = rank;
            index[suffix[0].Index] = 0;
            for (var i = 1; i < n; i++)
            {
                if (suffix[i].Rank == prev && suffix[i].Next == suffix[i - 1].Next)
                {
                    prev = suffix[i].Rank;
                    suffix[i].Rank = rank;
                }
                else
                {
                    prev = suffix[i].Rank;
                    suffix[i].Rank = ++rank;
                }

                index[suffix[i].Index] = i;
            }

            for (int i = 0; i < n; i++)
            {
                var nextP = suffix[i].Index + length / 2;
                suffix[i].Next = nextP < n ? suffix[index[nextP]].Rank : -1;
            }

            Array.Sort(suffix);
        }

        StartString = suffix[0];
        SufArray = new int[n];

        for (var i = 0; i < n; i++)
            SufArray[i] = suffix[i].Index;
    }

    /// <summary>
    /// Suffix class for <see cref="SuffixArray"/> realization
    /// </summary>
    public class Suffix : IComparable<Suffix>
    {
        public readonly int Index;
        public int Rank;
        public int Next;

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
        public int CompareTo(Suffix that)
        {
            if (Rank != that.Rank) return Rank.CompareTo(that.Rank);
            return Next.CompareTo(that.Next);
        }
    }
}
