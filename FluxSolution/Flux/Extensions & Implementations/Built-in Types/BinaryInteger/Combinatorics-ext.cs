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

      #region BinomialCoefficient

      /// <summary>
      /// <para>The binomial coefficients are the positive integers that occur as coefficients in the binomial theorem. Commonly, a binomial coefficient is indexed by a pair of integers "n >= k >= 0".</para>
      /// <para>This implementation can easily overflow, use larger storage types when possible.</para>
      /// <para><also href="https://en.wikipedia.org/wiki/Binomial_coefficient"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Binomial_coefficient#In_programming_languages"/></para>
      /// <para><seealso href="https://cp-algorithms.com/combinatorics/binomial-coefficients.html"/></para>
      /// <para><see href="https://dmitrybrant.com/2008/04/29/binomial-coefficients-stirling-numbers-csharp"/></para>
      /// </summary>
      /// <remarks>
      /// <para>Also known as "nCk", i.e. "<paramref name="n"/> choose <paramref name="k"/>", because there are nCk ways to choose an (unordered) subset of <paramref name="k"/> elements from a fixed set of <paramref name="n"/> elements.</para>
      /// <para>(k &lt; 0 or k > n) = 0</para>
      /// <para>(k = 0 or k = n) = 1</para>
      /// </remarks>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="n"></param>
      /// <param name="k"></param>
      /// <returns></returns>
      public static TInteger BinomialCoefficient(TInteger n, TInteger k)
      {
        k = TInteger.Min(k, n - k); // Optimization.

        if (TInteger.IsNegative(k))
          return TInteger.Zero;

        if (TInteger.IsZero(k)) // Because of the optimization above, only half of "(TInteger.IsZero(k) || k == n)" is needed.
          return TInteger.One;

        return FallingFactorial(n, k) / Factorial(k);
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

      #region CountCombinationsWithRepetition

      /// <summary>
      /// <para>Combinations with repetition are a way to select items from a set where the order does not matter (combination), and items can be chosen more than once (with repeats).</para>
      /// <para>Computes the number of combinations with repeats:</para>
      /// <para><c>"n multichoose k"</c> = <c>Cr(n + k - 1, k)</c> = <c>(n + k - 1)! / (k! * (n - 1)!)</c></para>
      /// <para>A <i>combination</i> is a way of choosing items from a set when the order of choice does not matter. If you rearrange the same chosen items, it still counts as the same combination.</para>
      /// <para><i><b>With</b> repetition</i> means that each item can be chosen more than once.</para>
      /// </summary>
      /// <param name="total">Number of distinct item types.</param>
      /// <param name="choose">Number of items to choose.</param>
      /// <returns></returns>
      public static TInteger CountCombinationsWithRepetition(TInteger total, TInteger choose)
        => BinomialCoefficient(total + choose - TInteger.One, choose);

      #endregion

      #region CountCombinationsWithoutRepetition

      /// <summary>
      /// <para>Combinations without repetition refer to the selection of items from a larger set, where the order of selection does not matter (combination), and each item can only be chosen once (no repeats).</para>
      /// <para>Counts combinations without repeats:</para>
      /// <para><c>"n choose k"</c> = <c>C(n, k)</c> = <c>n! / (k! * (n - k)!)</c></para>
      /// <para>A <i>combination</i> is a way of choosing items from a set when the order of choice does not matter. If you rearrange the same chosen items, it still counts as the same combination.</para>
      /// <para><i><b>Without</b> repetition</i> means that once an item is chosen, it cannot be selected again.</para>
      /// </summary>
      /// <param name="total">Number of distinct item types.</param>
      /// <param name="choose">Number of items to choose.</param>
      /// <returns></returns>
      public static TInteger CountCombinationsWithoutRepetition(TInteger total, TInteger choose)
        => BinomialCoefficient(total, choose);

      #endregion

      #region CountPermutationsWithRepetition

      /// <summary>
      /// <para>Permutations with repetition count the number of ordered selections where each selected item may be chosen more than once.</para>
      /// <para>Counts permutations with repeats:</para>
      /// <para><c>P(n, k) = n^k</c></para>
      /// <para>A <i>permutation</i> is an arrangement of items where the order matters. If you change the order, you create a different permutation.</para>
      /// <para><i><b>With</b> repetition</i> means that each item can be chosen more than once.</para>
      /// </summary>
      /// <param name="total">Number of distinct item types.</param>
      /// <param name="choose">Number of items to choose.</param>
      /// <returns></returns>
      public static TInteger CountPermutationsWithRepetition(TInteger total, TInteger choose)
        => TInteger.CreateChecked(System.Numerics.BigInteger.Pow(System.Numerics.BigInteger.CreateChecked(total), int.CreateChecked(choose)));

      #endregion

      #region CountPermutationsWithoutRepetition

      /// <summary>
      /// <para>Permutations without repetition refer to different groups of elements that can be done, so that two groups differ from each other only in the order the elements are placed. This situation frequently occurs when you’re working with unique physical objects that can occur only once in a permutation.</para>
      /// <para>Counts permutations without repeats:</para>
      /// <para><c>P(n, k)</c> = <c>n! / (n - k)!</c></para>
      /// <para>A <i>permutation</i> is an arrangement of items where the order matters. If you change the order, you create a different permutation.</para>
      /// <para><i><b>Without</b> repetition</i> means that once an item is chosen, it cannot be chosen again.</para>
      /// </summary>
      /// <param name="total">Number of distinct item types.</param>
      /// <param name="choose">Number of items to choose.</param>
      /// <returns></returns>
      public static TInteger CountPermutationsWithoutRepetition(TInteger total, TInteger choose)
        => FallingFactorial(total, choose);

      #endregion

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

      #region LahNumber

      /// <summary>
      /// <para>In mathematics, the (signed and unsigned) Lah numbers are coefficients expressing rising factorials in terms of falling factorials and vice versa.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Lah_number"/></para>
      /// <para><see href="https://rosettacode.org/wiki/Lah_numbers"/></para>
      /// </summary>
      /// <remarks>Lah numbers are sometimes called Stirling numbers of the third kind.</remarks>
      /// <typeparam name="TInteger"></typeparam>
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
