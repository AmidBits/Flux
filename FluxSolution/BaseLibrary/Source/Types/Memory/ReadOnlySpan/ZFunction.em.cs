namespace Flux
{
  public static partial class SystemReadOnlySpanEm
  {
    /// <summary>The Z-function for this sequence is an array of length n where the i-th element is equal to the greatest number of elements starting from the position i that coincide with the first elements of source. I.e., z[i] is the length of the longest common prefix between source and the suffix of source starting at i.</summary>
    // https://cp-algorithms.com/string/z-function.html
    public static int[] ZFunction<T>(this System.ReadOnlySpan<T> source, System.Collections.Generic.IEqualityComparer<T> comparer)
    {
      var length = source.Length;

      var z = new int[length];

      for (int i = 1, l = 0, r = 0; i < length; ++i)
      {
        if (i <= r)
          z[i] = System.Math.Min(r - i + 1, z[i - l]);

        while (i + z[i] < length && comparer.Equals(source[z[i]], source[i + z[i]]))
          ++z[i];

        if (i + z[i] - 1 > r)
        {
          l = i;
          r = i + z[i] - 1;
        }
      }

      return z;
    }
    /// <summary>The Z-function for this string is an array of length n where the i-th element is equal to the greatest number of characters starting from the position i that coincide with the first characters of s. I.e., z[i] is the length of the longest common prefix between s and the suffix of s starting at i.</summary>
    public static int[] ZFunction<T>(this System.ReadOnlySpan<T> source)
      => ZFunction(source, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}