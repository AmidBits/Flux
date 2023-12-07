namespace Flux
{
  public static partial class Fx
  {
    /// <summary>The Prefix function for this sequence is an array of length n where the i-th element is equal to the greatest number of elements starting from the position i that coincide with the first elements of source. I.e., z[i] is the length of the longest common prefix between source and the suffix of source starting at i.</summary>
    // https://cp-algorithms.com/string/prefix-function.html
    public static int[] PrefixFunction(this System.Text.StringBuilder source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var p = new int[source.Length];

      for (int i = 1; i < source.Length; i++)
      {
        var j = p[i - 1];

        while (j > 0 && source[i] != source[j])
          j = p[j - 1];

        if (source[i] == source[j])
          j++;

        p[i] = j;
      }

      return p;
    }
  }
}
