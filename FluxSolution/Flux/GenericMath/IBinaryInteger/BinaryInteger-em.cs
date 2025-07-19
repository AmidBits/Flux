namespace Flux
{
  public static class GenericMath // (or BinaryInteger)
  {
    extension<TInteger>(TInteger value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
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
      public TInteger BinomialCoefficient<TNumber>(TInteger k)
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
        else if (k.Equals(TNumber.Zero) || k.Equals(n))
          return TNumber.One; // 1 if (k == 0 || k == n)
  
        return TNumber.Zero; // 0 if (k < 0 || k > n)
      }
    }
  }
}
