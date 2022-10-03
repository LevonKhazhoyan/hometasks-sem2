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
    public static (char[], int) BwTransformation(String inputString)
    {
        var inputLength = inputString.Length;
        var inputSuffix = new SuffixArray(inputString).SufArray;
        var bwt = new char[inputLength];
        for (var i = 0; i < inputLength; i++)
        {
            var j = inputSuffix[i] - 1;
            if (j < 0)
                j += inputLength;
            bwt[i] = inputString[j];
        }
        return (bwt, Array.IndexOf(inputSuffix, 0));
    }
    
    /// <summary>
    /// Inversion of <see cref="BwTransformation(String)"/> to <see cref="T:char[]"/>
    /// </summary>
    public static char[] InverseTransform((char[], int) bwt)
    {
        
        var (stringInBwtFormat, first) = bwt;
        var next = new int[stringInBwtFormat.Length];
        var count = new int[charAmount + 1];
        var sortedBwt = new char[stringInBwtFormat.Length];
        foreach (var el in stringInBwtFormat)
            count[el + 1]++;
        for (var i = 0; i < charAmount; i++)
            count[i + 1] += count[i];
        for (var i = 0; i < stringInBwtFormat.Length; i++) {
            var posI = count[stringInBwtFormat[i]]++;
            sortedBwt[posI] = stringInBwtFormat[i];
            next[posI] = i;
        }
        var result = new char[stringInBwtFormat.Length];
        for (var i = 0; i < stringInBwtFormat.Length; i++)
        {
            result[i] = sortedBwt[first];
            first = next[first];
        }
        return result;
    }
}
