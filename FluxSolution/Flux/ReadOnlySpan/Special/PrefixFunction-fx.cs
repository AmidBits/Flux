namespace Flux
{
  public static partial class ReadOnlySpans
  {
    /// <summary>
    /// <para>The Prefix function for this span is an array of length n, where p[i] is the length of the longest proper prefix of the sub-span <paramref name="source"/>[0...i] which is also a suffix of this sub-span.</para>
    /// <para>A proper prefix of a span is a prefix that is not equal to the span itself.</para>
    /// <para>I.e., z[i] is the length of the longest common prefix between source and the suffix of source starting at i.</para>
    /// <para>Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Knuth%E2%80%93Morris%E2%80%93Pratt_algorithm"/></para>
    /// <para><see href="https://cp-algorithms.com/string/prefix-function.html"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static int[] PrefixFunction<T>(this System.ReadOnlySpan<T> source, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var pi = new int[source.Length];

      for (int i = 1; i < source.Length; i++)
      {
        var j = pi[i - 1];

        while (j > 0 && !equalityComparer.Equals(source[i], source[j]))
          j = pi[j - 1];

        if (equalityComparer.Equals(source[i], source[j]))
          j++;

        pi[i] = j;
      }

      return pi;
    }
  }
}
