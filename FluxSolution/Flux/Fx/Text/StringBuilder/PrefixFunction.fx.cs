namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>The Prefix function for this string-builder is an array of length n, where p[i] is the length of the longest proper prefix of the substring <paramref name="source"/>[0...i] which is also a suffix of this substring.</para>
    /// <para>A proper prefix of a string is a prefix that is not equal to the string itself.</para>
    /// <para>I.e., z[i] is the length of the longest common prefix between <paramref name="source"/> and the suffix of <paramref name="source"/> starting at i.</para>
    /// <para>Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Knuth%E2%80%93Morris%E2%80%93Pratt_algorithm"/></para>
    /// <para><see href="https://cp-algorithms.com/string/prefix-function.html"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
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
