namespace Flux
{
  public static partial class StringBuilders
  {
    /// <summary>
    /// <para>The Z-function for this string-builder is an array of length n where the i-th character is equal to the greatest number of characters starting from the position i that coincide with the first characters of <paramref name="source"/>.</para>
    /// <para>I.e. z[i] is the length of the longest string that is, at the same time, a prefix of <paramref name="source"/> and a prefix of the suffix of <paramref name="source"/> starting at i.</para>
    /// <para>Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// <para><see href="https://cp-algorithms.com/string/z-function.html"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    public static int[] Zfunction(this System.Text.StringBuilder source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var z = new int[source.Length];

      for (int i = 1, l = 0, r = 0; i < source.Length; i++)
      {
        if (i <= r)
          z[i] = int.Min(r - i + 1, z[i - l]);

        while (i + z[i] < source.Length && source[z[i]] == source[i + z[i]])
          z[i]++;

        if (i + z[i] - 1 > r)
        {
          l = i;
          r = i + z[i] - 1;
        }
      }

      return z;
    }
  }
}
