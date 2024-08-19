namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>The Prefix function for this sequence is an array of length n where the i-th element is equal to the greatest number of elements starting from the position i that coincide with the first elements of source. I.e., z[i] is the length of the longest common prefix between source and the suffix of source starting at i.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Knuth%E2%80%93Morris%E2%80%93Pratt_algorithm"/></para>
    /// <para><see href="https://cp-algorithms.com/string/prefix-function.html"/></para>
    /// </summary>
    public static int[] PrefixFunction<T>(this System.ReadOnlySpan<T> source, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var p = new int[source.Length];

      for (int i = 1; i < source.Length; i++)
      {
        var j = p[i - 1];

        while (j > 0 && !equalityComparer.Equals(source[i], source[j]))
          j = p[j - 1];

        if (equalityComparer.Equals(source[i], source[j]))
          j++;

        p[i] = j;
      }

      return p;
    }
  }
}
