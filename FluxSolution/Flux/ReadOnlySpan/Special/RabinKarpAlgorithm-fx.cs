namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Given a <paramref name="pattern"/> and a <paramref name="text"/>, determine if the pattern appears in the text and if it does, return the indices of all occurences in <c>O(|pattern| + |text|)</c> time.</para>
    /// <para><see href="https://cp-algorithms.com/string/rabin-karp.html"/></para>
    /// </summary>
    /// <param name="pattern"></param>
    /// <param name="text"></param>
    /// <returns>The indices of all occurences of <paramref name="pattern"/> in <paramref name="text"/>.</returns>
    public static System.Collections.Generic.List<int> RabinKarpIndices(this System.ReadOnlySpan<char> pattern, System.ReadOnlySpan<char> text)
    {
      const int p = 31;
      const int m = 1000000009;

      var sl = pattern.Length;
      var tl = text.Length;

      var p_pow = new long[int.Max(sl, tl)];
      p_pow[0] = 1;
      for (var i = 1; i < (int)p_pow.Length; i++)
        p_pow[i] = (p_pow[i - 1] * p) % m;

      var h = new long[tl + 1];
      for (var i = 0; i < tl; i++)
        h[i + 1] = (h[i] + (text[i] - 'a' + 1) * p_pow[i]) % m;

      var h_s = 0L;
      for (var i = 0; i < sl; i++)
        h_s = (h_s + (pattern[i] - 'a' + 1) * p_pow[i]) % m;

      var occurrences = new System.Collections.Generic.List<int>();

      for (var i = 0; i + sl - 1 < tl; i++)
      {
        var cur_h = (h[i + sl] + m - h[i]) % m;

        if (cur_h == h_s * p_pow[i] % m)
          occurrences.Add(i);
      }

      return occurrences;
    }
  }
}
