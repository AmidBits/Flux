namespace Flux
{
  public static partial class GenericMath
  {
#if NET7_0_OR_GREATER

    /// <summary>
    /// <para>The binomial coefficients are the positive integers that occur as coefficients in the binomial theorem. Commonly, a binomial coefficient is indexed by a pair of integers "n >= k >= 0".</para>
    /// <para><also href="https://en.wikipedia.org/wiki/Binomial_coefficient"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Binomial_coefficient#In_programming_languages"/></para>
    /// </summary>
    /// <remarks>This function is also known as "nCk", i.e. "n choose k", because there are nCk ways to choose an (unordered) subset of k elements from a fixed set of n elements.</remarks>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="n"></param>
    /// <param name="k"></param>
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

      for (var i = TSelf.Zero; i < k; i++)
        c = c * (n - i) / (i + TSelf.One);

      return c;
    }

#else

    /// <summary>Binomial coefficient.</summary>
    public static System.Numerics.BigInteger BinomialCoefficient(System.Numerics.BigInteger n, System.Numerics.BigInteger k)
    {
      if (k < 0 || k > n)
        return System.Numerics.BigInteger.Zero;

      if (k.IsZero || k == n)
        return System.Numerics.BigInteger.One;

      k = System.Numerics.BigInteger.Min(k, n - k);

      var c = System.Numerics.BigInteger.One;

      for (var i = System.Numerics.BigInteger.Zero; i < k; i++)
        c = c * (n - i) / (i + System.Numerics.BigInteger.One);

      return c;
    }

    /// <summary>Binomial coefficient.</summary>
    public static int BinomialCoefficient(int n, int k)
    {
      if (k < 0 || k > n)
        return 0;

      if (k == 0 || k == n)
        return 1;

      k = System.Math.Min(k, n - k);

      var c = 1;

      for (var i = 0; i < k; i++)
        c = c * (n - i) / (i + 1);

      return c;
    }

#endif
  }
}