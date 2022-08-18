namespace BWT;

/// <summary>
/// Burrows Wheeler Transformation implementation and inversion of it
/// </summary>
public static class Bwt
{
    private const int charAmount = 256;
    
    /// <summary>
    /// BWT based on building <see cref="SuffixArray"/> 
    /// </summary>
    public static Tuple<char[], int> BwTransformation(String inputString)
    {
        var inputLength = inputString.Length;
        var inputSuffix = new SuffixArray(inputString).sufArray;
        var bwt = new char[inputLength];
        for (var i = 0; i < inputLength; i++)
        {
            var j = inputSuffix[i] - 1;
            if (j < 0)
                j += inputLength;
            bwt[i] = inputString[j];
        }
        return new Tuple<char[], int>(bwt, Array.IndexOf(inputSuffix, 0));
    }
    
    /// <summary>
    /// Inversion of <see cref="BwTransformation(String)"/> to <see cref="T:char[]"/>
    /// </summary>
    public static char[] InverseTransform(Tuple<char[], int> bwt)
    {
        
        var bwtStr = bwt.Item1;
        var first = bwt.Item2;
        var next = new int[bwtStr.Length];
        var count = new int[charAmount + 1];
        var sortedBwt = new char[bwtStr.Length];
        foreach (var el in bwtStr)
            count[el + 1]++;
        for (var i = 0; i < charAmount; i++)
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
