namespace Flux
{
  public static partial class Maths
  {
    /// <summary>
    /// <para>The binomial coefficients are the positive integers that occur as coefficients in the binomial theorem. Commonly, a binomial coefficient is indexed by a pair of integers "n >= k >= 0".</para>
    /// <para><also href="https://en.wikipedia.org/wiki/Binomial_coefficient"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Binomial_coefficient#In_programming_languages"/></para>
    /// </summary>
    /// <remarks>Also known as "nCk", i.e. "<paramref name="n"/> choose <paramref name="k"/>", because there are nCk ways to choose an (unordered) subset of <paramref name="k"/> elements from a fixed set of <paramref name="n"/> elements.</remarks>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="n">Greater than or equal to <paramref name="k"/>.</param>
    /// <param name="k">Greater than or equal to 0.</param>
    /// <returns></returns>
    public static TSelf BinomialCoefficient<TSelf>(TSelf n, TSelf k)
       where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (TSelf.IsNegative(k) || k > n)
        return TSelf.Zero;

      if (TSelf.IsZero(k) || k == n)
        return TSelf.One;

      k = TSelf.Min(k, n - k);

      var c = TSelf.One;

      for (var d = TSelf.One; d <= k; d++)
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
