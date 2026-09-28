namespace Flux
{
  public static partial class BinaryInteger
  {
    #region m_factorialTable (static factorial table)

    private static readonly long[] m_factorialTable =
    {
      1L,                          // 0!
      1L,                          // 1!
      2L,                          // 2!
      6L,                          // 3!
      24L,                         // 4!
      120L,                        // 5!
      720L,                        // 6!
      5040L,                       // 7!
      40320L,                      // 8!
      362880L,                     // 9!
      3628800L,                    // 10!
      39916800L,                   // 11!
      479001600L,                  // 12!
      6227020800L,                 // 13!
      87178291200L,                // 14!
      1307674368000L,              // 15!
      20922789888000L,             // 16!
      355687428096000L,            // 17!
      6402373705728000L,           // 18!
      121645100408832000L,         // 19!
      2432902008176640000L         // 20!
    };

    #endregion

    #region m_doubleFactorialTable (static double factorial table)

    private static readonly long[] m_doubleFactorialTable =
    {
      1L,                                   // 0!!
      1L,                                   // 1!!
      2L,                                   // 2!!
      3L,                                   // 3!!
      8L,                                   // 4!!
      15L,                                  // 5!!
      48L,                                  // 6!!
      105L,                                 // 7!!
      384L,                                 // 8!!
      945L,                                 // 9!!
      3840L,                                // 10!!
      10395L,                               // 11!!
      46080L,                               // 12!!
      135135L,                              // 13!!
      645120L,                              // 14!!
      2027025L,                             // 15!!
      10321920L,                            // 16!!
      34459425L,                            // 17!!
      185794560L,                           // 18!!
      654729075L,                           // 19!!
      3715891200L,                          // 20!!
      155195155200L,                        // 21!!
      817496064640L,                        // 22!!
      18803915073920L,                      // 23!!
      19619905551360L,                      // 24!!
      11281445692032000L,                   // 25!!
      293317587993000000L,                  // 26!!
      7919574875811000000L,                 // 27!!
    };

    #endregion

    extension<TInteger>(TInteger)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      #region DoubleFactorial

      /// <summary>
      /// <para>The double factorial of a number n, denoted by n‼, is the product of all the positive integers up to n that have the same parity (odd or even) as n.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Double_factorial"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="n"></param>
      /// <returns></returns>
      public static TInteger DoubleFactorial(TInteger n)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(n);

        if (TryGetStaticDoubleFactorial(n, out var sdf))
          return TInteger.CreateChecked(sdf);

        var result = TInteger.One;
        var two = TInteger.CreateChecked(2);

        checked
        {
          for (var i = n; i > TInteger.One; i -= two)
            result *= i;
        }

        return result;
      }

      private static bool TryGetStaticDoubleFactorial(TInteger n, out long value)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(n);

        var i = uint.CreateChecked(n);

        if (i < m_doubleFactorialTable.Length)
        {
          value = m_doubleFactorialTable[i];
          return true;
        }

        value = 0;
        return false;
      }

      #endregion

      #region Factorial

      /// <summary>
      /// <para>The factorial of a non-negative integer n, denoted by n!, is the product of all positive integers less than or equal to n.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Factorial"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="n"></param>
      /// <returns></returns>
      public static TInteger Factorial(TInteger n)
      {
        if (TryGetStaticFactorial(n, out var sf))
          return TInteger.CreateChecked(sf);

        if (n < TInteger.CreateChecked(47))
          return NaiveFactorial(n);

        return SplitFactorial(n);
      }

      public static TInteger MaxFactorialNForBitCount(TInteger bitCount)
      {
        var lo = 1;
        var hi = 1 << (BinaryInteger.GetBitCount(bitCount) - 2);

        var bc = double.CreateChecked(bitCount);

        while (lo < hi)
        {
          var mid = (lo + hi + 1) / 2;

          var log2 = (mid <= 50)
            ? double.CreateChecked(BinaryInteger.GetBitCount(BinaryInteger.Factorial(System.Numerics.BigInteger.CreateChecked(mid)))) // If less than or equal to 50, actually compute the factorial and get the bit count.
            : DoubleExtensions.Log2FactorialStirlingApproximation(double.CreateChecked(mid)); // If greater than 50, use the approximation of log2(n!) = n * log2(n) - n * log2(e) + 0.5 * log2(2 * pi * n).

          if (log2 <= bc)
            lo = mid;
          else
            hi = mid - 1;
        }

        return TInteger.CreateChecked(lo);
      }

      /// <summary>
      /// <para>Computes the factorial of <paramref name="value"/>, e.g. <c>Factorial(5) => 1 * 2 * 3 * 4 * 5</c></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Factorial"/></para>
      /// </summary>
      /// <remarks>This plain-and-simple iterative version of factorials is faster with numbers smaller than 60 or so, and starts loosing with larger numbers.</remarks>
      /// <typeparam name="TInteger"></typeparam>
      private static TInteger NaiveFactorial(TInteger n)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(n);

        var result = TInteger.One;

        if (n <= result)
          return result;

        checked
        {
          for (var m = TInteger.CreateChecked(2); m <= n; m++)
            result *= m;
        }

        return result;
      }

      /// <summary>
      /// <para>Compute the factorial using divide-and-conquer, a.k.a. split-factorial of <paramref name="value"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Factorial"/></para>
      /// <para><see href="http://www.luschny.de/math/factorial/csharp/FactorialSplit.cs.html"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      private static TInteger SplitFactorial(TInteger n)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(n);

        var two = (TInteger.One + TInteger.One);

        var p = TInteger.One;
        var r = TInteger.One;
        var currentN = TInteger.One;

        var h = TInteger.Zero;
        var shift = TInteger.Zero;
        var high = TInteger.One;

        var log2n = int.CreateChecked(TInteger.Log2(n));

        while (h != n)
          checked
          {
            shift += h;
            h = n >>> log2n--;
            var len = high;
            high = (h - TInteger.One) | TInteger.One;
            len = (high - len) >>> 1;

            if (len > TInteger.Zero)
            {
              p *= Product(len);
              r *= p;
            }
          }

        return r << int.CreateChecked(shift);

        TInteger Product(TInteger n)
        {
          checked
          {
            var m = n >> 1;

            if (TInteger.IsZero(m))
              return currentN += two;

            if (n == two)
              return (currentN += two) * (currentN += two);

            return Product(n - m) * Product(m);
          }
        }
      }

      private static bool TryGetStaticFactorial(TInteger n, out TInteger factorial)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(n);

        var i = uint.CreateChecked(n);

        if (i < m_factorialTable.Length)
        {
          factorial = TInteger.CreateChecked(m_factorialTable[i]);
          return true;
        }

        factorial = TInteger.Zero;
        return false;
      }

      #endregion

      #region MultiFactorial

      /// <summary>
      /// <para>Naive implementation of n! (k = 1, factorial), n!! (k = 2, a.k.a. double factorial), n!!! (k = 3, triple factorial), etc.</para>
      /// </summary>
      /// <param name="n"></param>
      /// <param name="k"></param>
      /// <returns></returns>
      public static TInteger MultiFactorial(TInteger n, TInteger k)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(n);
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(k);

        var result = TInteger.One;

        checked
        {
          for (var i = n; i > TInteger.Zero; i -= k)
            result *= i;
        }

        return result;
      }

      #endregion

      #region FallingFactorial

      /// <summary>
      /// <para>When n is a positive integer, the falling factorial, (x)_n, gives the number of n-permutations (sequences of distinct elements) from an n-element set.</para>
      /// <example>
      /// <para>The number (3) of different podiums (assignments of gold, silver, and bronze medals) possible in an eight-person race: <c>FallingFactorial(8, 3)</c></para>
      /// </example>
      /// <para><see href="https://en.wikipedia.org/wiki/Falling_and_rising_factorials"/></para>
      /// <para><see href="https://dmitrybrant.com/2008/04/29/binomial-coefficients-stirling-numbers-csharp"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="x">The base, or starting value of the sequence of factors. Plays the same role as in ordinary factorial‑like expressions.</param>
      /// <param name="n">The order, or number of factors in the product. Must be non-negative. If 0, the defined result is 1.</param>
      /// <returns>
      /// <para>The count of permutations no repetitions.</para>
      /// </returns>
      public static TInteger FallingFactorial(TInteger x, TInteger n)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(n);

        if (System.Numerics.BigInteger.CreateChecked(n) > System.Numerics.BigInteger.CreateChecked(uint.MaxValue)) throw new System.ArgumentOutOfRangeException(nameof(n), "n is too large to iterate."); // n must fit in uint for a practical loop.

        var count = uint.CreateChecked(n);

        if (count == 0)
          return TInteger.One;
        if (count == 1)
          return x;

        var one = TInteger.One;
        var result = one;

        checked
        {
          var term = x;
          for (var i = 0u; i < count; i++)
          {
            result *= term;
            term -= one;
          }
        }

        return result;
      }

      #endregion

      // PockHammer (ambiguous, removed) - use FallingFactorial/RisingFactorial instead. There is also generalized versions in the FloatingPoint extensions class.

      #region RisingFactorial

      /// <summary>
      /// <para>The rising factorial, x^(n), gives the number of partitions of an n-element set into x ordered sequences (possibly empty).</para>
      /// <example>
      /// <para>The "the number of ways to arrange n flags on x flagpoles", where all flags must be used and each flagpole can have any number of flags.</para>
      /// <para>Equivalently, this is the number of ways to partition a set of size n (e.g. 3 flags) into x distinguishable parts (e.g. 2 poles), with a linear order on the elements assigned to each part (the order of the flags on a given pole). <c>RisingFactorial(2, 3);</c></para>
      /// </example>
      /// <para><see href="https://en.wikipedia.org/wiki/Falling_and_rising_factorials"/></para>
      /// </summary>
      /// <param name="x">The base, or starting value of the sequence of factors. Plays the same role as in ordinary factorial‑like expressions.</param>
      /// <param name="n">The order, or number of factors in the product. Must be non-negative. If 0, the defined result is 1.</param>
      /// <returns></returns>
      public static TInteger RisingFactorial(TInteger x, TInteger n)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(n);

        if (System.Numerics.BigInteger.CreateChecked(n) > System.Numerics.BigInteger.CreateChecked(uint.MaxValue)) throw new System.ArgumentOutOfRangeException(nameof(n), "n is too large to iterate."); // n must fit in uint for a practical loop.

        var count = uint.CreateChecked(n);

        if (count == 0)
          return TInteger.One;
        if (count == 1)
          return x;

        var one = TInteger.One;
        var result = one;

        checked
        {
          var term = x;
          for (var i = 0u; i < count; i++)
          {
            result *= term;
            term += one;
          }
        }

        return result;
      }

      #endregion
    }
  }

  public static partial class FloatingPoint
  {
    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      #region FactorialPower

      /// <summary>
      /// <para>Generalized rising factorial: x^(n)_rising(h) = x * (x + h) * (x + 2h) * ... * (x + (n-1)h)</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="x">The base, or starting value of the sequence of factors. Plays the same role as in ordinary factorial‑like expressions.</param>
      /// <param name="n">The order, or number of factors in the product. Must be non-negative. If 0, the defined result is 1.</param>
      /// <param name="h">Step size, or increment between factors. Determines how far apart the terms are spaced.</param>
      /// <returns></returns>
      public static TFloat FactorialPower<TInteger>(TFloat x, TInteger n, TFloat h)
        where TInteger : System.Numerics.IBinaryInteger<TInteger>
      {
        TFloat result = TFloat.One;

        while (n-- > TInteger.Zero)
        {
          result *= x;

          x += h; // increment x by step h
        }

        return result;
      }

      #endregion
    }
  }
}
