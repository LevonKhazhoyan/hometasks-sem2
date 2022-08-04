namespace BWT;
/// <summary>
/// Burrows Wheeler Transformation implementation and inversion of it
/// </summary>
public class BwtProj
{
    private const int R = 256;
    
    /// <summary>
    /// BWT based on building <see cref="SuffixArray"/> 
    /// </summary>
    public static Tuple<char[], int> BwTransformation(String txt)
    {
        var n = txt.Length;
        var suffixArr = new SuffixArray(txt).SufArray;
        var bwt = new char[n];
        for (var i = 0; i < n; i++)
        {
            var j = suffixArr[i] - 1;
            if (j < 0)
                j += n;
            bwt[i] = txt[j];
        }
        return new Tuple<char[], int>(bwt, Array.IndexOf(suffixArr, 0));
    }
    
    /// <summary>
    /// Inversion of <see cref="BwTransformation(String)"/> to <see cref="T:char[]"/>
    /// </summary>
    public static char[] InverseTransform(Tuple<char[], int> bwt)
    {
        var bwtStr = bwt.Item1;
        var first = bwt.Item2;
        var next = new int[bwtStr.Length];
        var count = new int[R + 1];
        var sortedBwt = new char[bwtStr.Length];
        foreach (var el in bwtStr)
            count[el + 1]++;
        for (var i = 0; i < R; i++)
            count[i + 1] += count[i];
        for (var i = 0; i < bwtStr.Length; i++) {
            var posI = count[bwtStr[i]]++;
            sortedBwt[posI] = bwtStr[i];
            next[posI] = i;
        }
        var res = new char[bwtStr.Length];
        for (var i = 0; i < bwtStr.Length; i++)
        {
            res[i] = sortedBwt[first];
            first = next[first];
        }
        return res;
    }
}
