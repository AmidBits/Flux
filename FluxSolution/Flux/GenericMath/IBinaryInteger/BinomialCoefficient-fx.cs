namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>The binomial coefficients are the positive integers that occur as coefficients in the binomial theorem. Commonly, a binomial coefficient is indexed by a pair of integers "n >= k >= 0".</para>
    /// <para>This implementation can easily overflow, use larger storage types when possible.</para>
    /// <para><also href="https://en.wikipedia.org/wiki/Binomial_coefficient"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Binomial_coefficient#In_programming_languages"/></para>
    /// <para><seealso href="https://cp-algorithms.com/combinatorics/binomial-coefficients.html"/></para>
    /// </summary>
    /// <remarks>
    /// <para>Also known as "nCk", i.e. "<paramref name="n"/> choose <paramref name="k"/>", because there are nCk ways to choose an (unordered) subset of <paramref name="k"/> elements from a fixed set of <paramref name="n"/> elements.</para>
    /// <para>(k &lt; 0 or k > n) = 0</para>
    /// <para>(k = 0 or k = n) = 1</para>
    /// </remarks>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="n">Greater than or equal to <paramref name="k"/>.</param>
    /// <param name="k">Greater than or equal to 0.</param>
    /// <returns></returns>
    public static TNumber BinomialCoefficient<TNumber>(this TNumber n, TNumber k)
       where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      //if (TNumber.IsNegative(k) || k > n) return TNumber.Zero; // (k < 0 || k > n) = 0
      //else if (TNumber.IsZero(k) || k == n) return TNumber.One; // (k == 0 || k == n) = 1

      if (k > TNumber.Zero && k < n) // 0 < k < n
        checked
        {
          k = TNumber.Min(k, n - k); // Optimize for the loop below.

          var c = TNumber.One;

          for (var i = TNumber.One; i <= k; i++)
            c = c * (n - k + i) / i;
          //c = c * n-- / i;

          return c;
        }
      else if (k.Equals(TNumber.Zero) || k.Equals(n)) return TNumber.One; // 1 if (k == 0 || k == n)
      else return TNumber.Zero; // 0 if (k < 0 || k > n)
    }

    //public static int BinomialCoefficient(this int n, int k)
    //{
    //  var c = 1L;
    //  for (var i = n - k + 1; i <= n; ++i)
    //    c *= i;
    //  for (var i = 2; i <= k; ++i)
    //    c /= i;
    //  return int.CreateChecked(c);
    //}

    ///// <summary>
    ///// <para></para>
    ///// </summary>
    ///// <param name="n"></param>
    ///// <param name="k"></param>
    ///// <returns></returns>
    //public static long BinomialCoefficientEx(this long n, long k)
    //{
    //  var c = 1L;
    //  for (var i = 1; i <= k; i++)
    //    c = c * (n - k + i) / i;
    //  return c;
    //}
  }
}
