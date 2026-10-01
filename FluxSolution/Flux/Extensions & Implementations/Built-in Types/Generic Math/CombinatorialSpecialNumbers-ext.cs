namespace Flux
{
  public static partial class BinaryInteger
  {
    extension<TInteger>(TInteger)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      #region BellNumbers (sequence of)

      /// <summary>
      /// <para>Creates a new sequence of Bell numbers.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Bell_number"/></para>
      /// </summary>
      /// <remarks>This function generate results until the type <typeparamref name="TInteger"/> under/overflows in any calculation. No exception is thrown.</remarks>
      /// <typeparam name="TInteger"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TInteger> BellNumbers()
        => BellTriangle<TInteger>().Select(a => a[0]);

      #endregion

      #region BellTriangle (sequence of lists)

      /// <summary>
      /// <para>Creates a new sequence with arrays (i.e. row) of Bell numbers in a Bell triangle.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Bell_triangle"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Bell_number"/></para>
      /// </summary>
      /// <remarks>This function generate results until the type <typeparamref name="TInteger"/> under/overflows in any calculation. No exception is thrown.</remarks>
      /// <typeparam name="TInteger"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<TInteger>> BellTriangle()
      {
        foreach (var list in BellTriangleAugmented<TInteger>().Skip(1))
        {
          list.RemoveAt(0);

          yield return list;
        }
      }

      #endregion

      #region BellTriangleAugmented (sequence of lists)

      /// <summary>
      /// <para>Creates a new sequence with arrays (i.e. row) of Bell numbers in an augmented Bell triangle.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Bell_triangle"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Bell_number"/></para>
      /// </summary>
      /// <remarks>This function generate results until the type <typeparamref name="TInteger"/> under/overflows in any calculation. No exception is thrown.</remarks>
      /// <typeparam name="TInteger"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<TInteger>> BellTriangleAugmented()
      {
        var lc = new System.Collections.Generic.List<TInteger>() { TInteger.One }; // This is the current.
        var lp = new System.Collections.Generic.List<TInteger>(); // This is the previous.

        while (true)
        {
          yield return lc.ToList();

          try
          {
            checked
            {
              (lc, lp) = (lp, lc); // Rotate and reuse the lists is much faster and less resource intensive.

              lc.Clear(); // This is now the current, and l0 became the previous.

              lc.Add(lp[^1]);
              lc.Insert(0, lc[0] - lp[0]);

              for (var i = 2; i <= lp.Count; i++)
                lc.Add(lp[i - 1] + lc[i - 1]);
            }
          }
          catch { break; }
        }
      }

      #endregion

      #region GetCatalanNumber

      /// <summary>
      /// <para>Returns the Catalan number for the specified <paramref name="number"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Catalan_number"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="number"></param>
      /// <returns></returns>
      public static TInteger CatalanNumber(TInteger n)
        => checked(Factorial(n + n) / (Factorial(n + TInteger.One) * Factorial(n)));

      #endregion

      #region CatalanNumbers (sequence of)

      /// <summary>
      /// <para>Creates a new sequence with Catalan numbers.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Catalan_number"/></para>
      /// </summary>
      /// <remarks>This function runs indefinitely, if allowed.</remarks>
      /// <typeparam name="TInteger"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TInteger> CatalanSequence()
        => Number.ArithmeticSequence(TInteger.Zero, TInteger.One).AsParallel().AsOrdered().Select(CatalanNumber);

      #endregion

      #region LahNumber

      /// <summary>
      /// <para>In mathematics, the (signed and unsigned) Lah numbers are coefficients expressing rising factorials in terms of falling factorials and vice versa.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Lah_number"/></para>
      /// <para><see href="https://rosettacode.org/wiki/Lah_numbers"/></para>
      /// </summary>
      /// <remarks>Lah numbers are sometimes called Stirling numbers of the third kind.</remarks>
      /// <param name="n"></param>
      /// <param name="k"></param>
      /// <returns></returns>
      public static TInteger LahNumber(TInteger n, TInteger k)
      {
        if (k == TInteger.One)
          return Factorial(n);

        if (k == n)
          return TInteger.One;

        if (k > n)
          return TInteger.Zero;

        if (k < TInteger.One || n < TInteger.One)
          return TInteger.Zero;

        checked
        {
          var fnM1 = Factorial(n - TInteger.One);
          var fkM1 = Factorial(k - TInteger.One);

          return (fnM1 * n * fnM1) / (fkM1 * k * fkM1) / Factorial(n - k);
        }
      }

      #endregion

      #region Sheffer polynomial/sequence

      /// <summary>
      /// <para>Compute the nth Sheffer polynomial at x.</para>
      /// </summary>
      /// <param name="n">The degree of the polynomial.</param>
      /// <param name="x">The variable for which the polynomial is evaluated.</param>
      /// <returns></returns>
      public static TFloat ShefferPolynomial<TFloat>(TInteger n, TFloat x)
        where TFloat : System.Numerics.IFloatingPoint<TFloat>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(n);

        if (TInteger.IsZero(n))
          return TFloat.One; // P_0(x) = 1

        if (n == TInteger.One)
          return x; // P_1(x) = x

        // Recurrence relation: P_n(x) = x * P_(n-1)(x) - (n-1) * P_(n-2)(x)

        var prev1 = x; // P_1(x)
        var prev2 = TFloat.One; // P_0(x)
        var current = TFloat.Zero;

        for (var i = TInteger.CreateChecked(2); i <= n; i++)
        {
          current = x * prev1 - TFloat.CreateChecked(i - TInteger.One) * prev2;
          prev2 = prev1;
          prev1 = current;
        }

        return current;
      }

      #endregion

      #region Stirling numbers

      /// <summary>
      /// <para>Stirling numbers of the first kind arise in the study of permutations. In particular, the unsigned Stirling numbers of the first kind count permutations according to their number of cycles (counting fixed points as cycles of length one).</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Stirling_number"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="n"></param>
      /// <param name="k"></param>
      /// <param name="dp"></param>
      /// <returns></returns>
      public static TInteger StirlingNumber1stKind(TInteger n, TInteger k, out TInteger[,]? dp)
      {
        if (TInteger.IsNegative(n) || TInteger.IsNegative(k) || k > n)
        {
          dp = null;

          return TInteger.Zero;
        }

        var ni = int.CreateChecked(n);
        var ki = int.CreateChecked(k);

        dp = new TInteger[ni + 1, ki + 1];

        dp[0, 0] = TInteger.One; // c(0, 0) = 1

        for (var i = 1; i <= ni; i++)
          dp[i, 0] = TInteger.Zero; // c(n, 0) = 0 for n > 0

        for (var j = 1; j <= ki; j++)
          dp[0, j] = TInteger.Zero; // c(0, k) = 0 for k > 0

        for (var i = 1; i <= ni; i++)
          for (var j = 1; j <= ki; j++)
            dp[i, j] = dp[i - 1, j - 1] + TInteger.CreateChecked(i - 1) * dp[i - 1, j]; // Fill the table using the recurrence relation.

        return dp[ni, ki];
      }

      public static TInteger StirlingNumber1stKind(TInteger n, TInteger k)
        => StirlingNumber1stKind(n, k, out var _);

      /// <summary>
      /// <para>a Stirling number of the second kind (or Stirling partition number) is the number of ways to partition a set of n objects into k non-empty subsets.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Stirling_number"/></para>
      /// <para><see href="https://dmitrybrant.com/2008/04/29/binomial-coefficients-stirling-numbers-csharp"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="n"></param>
      /// <param name="k"></param>
      /// <returns></returns>
      public static TInteger StirlingNumber2ndKind(TInteger n, TInteger k)
      {
        var sum = TInteger.Zero;
        var neg = TInteger.One;

        if ((TInteger.IsZero(n) ^ TInteger.IsZero(k)) || (k > n)) return sum;
        if (n == k) return neg;

        checked
        {
          for (var i = TInteger.Zero; i <= k; i++)
          {
            sum += neg * BinomialCoefficient(k, i) * Pow(k - i, n);
            neg = -neg;
          }
        }

        sum /= Factorial(k);

        return sum;
      }

      #endregion
    }
  }
}
