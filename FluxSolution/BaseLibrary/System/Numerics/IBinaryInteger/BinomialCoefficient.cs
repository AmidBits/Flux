namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>The binomial coefficients are the positive integers that occur as coefficients in the binomial theorem. Commonly, a binomial coefficient is indexed by a pair of integers "n >= k >= 0".</para>
    /// <para><also href="https://en.wikipedia.org/wiki/Binomial_coefficient"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Binomial_coefficient#In_programming_languages"/></para>
    /// </summary>
    /// <remarks>Also known as "nCk", i.e. "<paramref name="n"/> choose <paramref name="k"/>", because there are nCk ways to choose an (unordered) subset of <paramref name="k"/> elements from a fixed set of <paramref name="n"/> elements.</remarks>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="n">Greater than or equal to <paramref name="k"/>.</param>
    /// <param name="k">Greater than or equal to 0.</param>
    /// <returns></returns>
    public static TValue BinomialCoefficient<TValue>(this TValue n, TValue k)
       where TValue : System.Numerics.IBinaryInteger<TValue>
    {
      if (TValue.IsNegative(k) || k > n)
        return TValue.Zero;

      if (TValue.IsZero(k) || k == n)
        return TValue.One;

      k = TValue.Min(k, n - k); // Optimize for the loop below.

      var c = TValue.One;

      for (var d = TValue.One; d <= k; d++)
      {
        c *= n--;
        c /= d;
      }

      #region Alternative loop (not verified)
      //for (var i = TSelf.Zero; i < k; i++)
      //  c = c * (n - i) / (i + TSelf.One);
      #endregion

      return c;
    }
  }
}
