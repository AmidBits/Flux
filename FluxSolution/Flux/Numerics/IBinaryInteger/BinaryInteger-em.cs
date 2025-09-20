namespace Flux
{
  public static partial class IBinaryIntegers
  {
    /// <summary>
    /// </summary>
    /// <param name="value">The total number of items in the set. Greater than or equal to <paramref name="k"/>.</param>
    /// <returns></returns>
    extension<TInteger>(TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      ///// <summary>
      ///// <para>Computes the factorial of <paramref name="value"/>, e.g. Factorial(5); = "5 * 4 * 3 * 2 * 1"</para>
      ///// <para><see href="https://en.wikipedia.org/wiki/Factorial"/></para>
      ///// </summary>
      ///// <remarks>This plain-and-simple iterative version of factorials is faster with numbers smaller than 200 or so, but is significantly slower than <see cref="SplitFactorial{TValue}(TValue)"/> on larger numbers.</remarks>
      ///// <typeparam name="TInteger"></typeparam>
      ///// <param name="value"></param>
      ///// <returns></returns>
      //public TInteger BasicFactorial()
      //{
      //  System.ArgumentOutOfRangeException.ThrowIfNegative(value);

      //  if (TInteger.IsZero(value))
      //    return TInteger.One;

      //  var f = TInteger.One;

      //  if (value > f) // Only loop if value is greater than 1.
      //  {
      //    f++;

      //    for (var m = f + TInteger.One; m <= value; m++)
      //      checked { f *= m; }
      //  }

      //  return f;
      //}

      ///// <summary>
      ///// <para>The binomial coefficients are the positive integers that occur as coefficients in the binomial theorem. Commonly, a binomial coefficient is indexed by a pair of integers "n >= k >= 0".</para>
      ///// <para>This implementation can easily overflow, use larger storage types when possible.</para>
      ///// <para><also href="https://en.wikipedia.org/wiki/Binomial_coefficient"/></para>
      ///// <para><seealso href="https://en.wikipedia.org/wiki/Binomial_coefficient#In_programming_languages"/></para>
      ///// <para><seealso href="https://cp-algorithms.com/combinatorics/binomial-coefficients.html"/></para>
      ///// </summary>
      ///// <remarks>
      ///// <para>Also known as "nCk", i.e. "<paramref name="n"/> choose <paramref name="k"/>", because there are nCk ways to choose an (unordered) subset of <paramref name="k"/> elements from a fixed set of <paramref name="n"/> elements.</para>
      ///// <para>(k &lt; 0 or k > n) = 0</para>
      ///// <para>(k = 0 or k = n) = 1</para>
      ///// </remarks>
      ///// <typeparam name="TInteger"></typeparam>
      ///// <param name="n">The total number of items in the set. Greater than or equal to <paramref name="k"/>.</param>
      ///// <param name="k">The number of items to choose. Greater than or equal to 0.</param>
      ///// <returns></returns>
      //public TInteger BinomialCoefficient(TInteger k)
      //{
      //  if (TInteger.IsNegative(k) || k > value)
      //    return TInteger.Zero; // (k < 0 || k > n) = 0
      //  else if (TInteger.IsZero(k) || k == value)
      //    return TInteger.One; // (k == 0 || k == n) = 1

      //  checked
      //  {
      //    k = TInteger.Min(k, value - k); // Optimize for the loop below.

      //    var c = TInteger.One;

      //    for (var i = TInteger.One; i <= k; i++)
      //      c = c * (value - k + i) / i;
      //    //c = c * n-- / i;

      //    return c;
      //  }
      //}

      //public TInteger CountCombinationsWithRepetition(TInteger totalCount, TInteger combinationCount)
      //  => (combinationCount + totalCount - TInteger.One).Factorial() / (combinationCount.Factorial() * (totalCount - TInteger.One).Factorial());

      //public TInteger CountCombinationsWithoutRepetition(TInteger totalCount, TInteger combinationCount)
      //  => BinomialCoefficient(totalCount, combinationCount);

      //public TInteger CountPermutationsWithRepetition(TInteger totalCount, TInteger permutationCount)
      //  => TInteger.CreateChecked(System.Numerics.BigInteger.Pow(System.Numerics.BigInteger.CreateChecked(totalCount), int.CreateChecked(permutationCount)));

      //public TInteger CountPermutationsWithoutRepetition(TInteger totalCount, TInteger permutationCount)
      //  => totalCount.TopDownFactorialWithThreshold(totalCount - permutationCount);

      ///// <summary>Drop the trailing (least significant) digit of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
      //public TInteger DropLeastSignificantDigit<TRadix>(TRadix radix)
      //  where TRadix : System.Numerics.IBinaryInteger<TRadix>
      //  => value / TInteger.CreateChecked(Flux.Units.Radix.AssertMember(radix));

      ///// <summary>Drop <paramref name="count"/> trailing (least significant) digits from <paramref name="value"/> using base <paramref name="radix"/>.</summary>
      //public TInteger DropLeastSignificantDigits<TRadix>(TRadix radix, TInteger count)
      //  where TRadix : System.Numerics.IBinaryInteger<TRadix>
      //  => value / TInteger.CreateChecked(Flux.Units.Radix.AssertMember(radix).FastIntegerPow(count, Flux.UniversalRounding.WholeTowardZero, out var _));

      ///// <summary>Drop <paramref name="count"/> leading (most significant) digits of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
      //public TInteger DropMostSignificantDigits<TRadix>(TRadix radix, TInteger count)
      //  where TRadix : System.Numerics.IBinaryInteger<TRadix>
      //  => value % TInteger.CreateChecked(radix.FastIntegerPow(FastDigitCount(value, radix) - count, Flux.UniversalRounding.WholeTowardZero, out var _));

      //public TInteger Factorial()
      //{
      //  System.ArgumentOutOfRangeException.ThrowIfNegative(value);

      //  if (TInteger.IsZero(value))
      //    return TInteger.One;

      //  if (value <= TInteger.CreateChecked(sbyte.MaxValue))
      //    return BasicFactorial(value);

      //  return SplitFactorial(value);

      //}

      ///// <summary>
      ///// <para></para>
      ///// </summary>
      ///// <typeparam name="TRadix"></typeparam>
      ///// <param name="value"></param>
      ///// <param name="radix"></param>
      ///// <returns></returns>
      //public TInteger FastDigitCount<TRadix>(TRadix radix)
      //  where TRadix : System.Numerics.INumber<TRadix>
      //  => TInteger.One + value.FastIntegerLog(radix, UniversalRounding.WholeTowardZero, out var _);

      ///// <summary>Returns the greatest common divisor of all values.</summary>
      ///// <see href="https://en.wikipedia.org/wiki/Greatest_common_divisor"/>
      //public TInteger Gcd(params TInteger[] other)
      //{
      //  System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(other.Length);

      //  return value.GreatestCommonDivisor(other.Aggregate(GreatestCommonDivisor));
      //}

      ///// <summary>Returns the greatest common divisor of <paramref name="a"/> and <paramref name="b"/>.</summary>
      ///// <see href="https://en.wikipedia.org/wiki/Greatest_common_divisor"/>
      //public TInteger GreatestCommonDivisor(TInteger b)
      //{
      //  while (b != TInteger.Zero)
      //  {
      //    var t = b;
      //    b = value % b;
      //    value = t;
      //  }

      //  return TInteger.Abs(value);
      //}

      ///// <summary>The extended GCD (or Euclidean algorithm) yields in addition the GCD of <paramref name="a"/> and <paramref name="b"/>, also the addition the coefficients of Bézout's identity.</summary>
      ///// <remarks>When a and b are coprime (i.e. GCD equals 1), x is the modular multiplicative inverse of a modulo b, and y is the modular multiplicative inverse of b modulo a.</remarks>
      ///// <see href="https://en.wikipedia.org/wiki/Extended_Euclidean_algorithm"/>
      ///// <seealso href="https://en.wikipedia.org/wiki/Bézout%27s_identity"/>
      //public TInteger GreatestCommonDivisorEx(TInteger b, out TInteger x, out TInteger y)
      //{
      //  System.ArgumentOutOfRangeException.ThrowIfNegative(value);
      //  System.ArgumentOutOfRangeException.ThrowIfNegative(b);

      //  x = TInteger.One;
      //  y = TInteger.Zero;

      //  var u = TInteger.Zero;
      //  var v = TInteger.One;

      //  while (!TInteger.IsZero(b))
      //  {
      //    value = b;
      //    b = value % b;

      //    var q = value / b;

      //    var u1 = x - q * u;
      //    var v1 = y - q * v;

      //    x = u;
      //    y = v;

      //    u = u1;
      //    v = v1;
      //  }

      //  return value;
      //}

      ///// <summary>
      ///// <para>When x is a positive integer, the falling factorial gives the number of n-permutations (sequences of distinct elements) from an x-element set.</para>
      ///// <example>
      ///// <para>The number (3) of different podiums (assignments of gold, silver, and bronze medals) possible in an eight-person race: <c>FallingFactorialPower(8, 3)</c></para>
      ///// </example>
      ///// <para><see href="https://en.wikipedia.org/wiki/Falling_and_rising_factorials"/></para>
      ///// </summary>
      ///// <typeparam name="TInteger"></typeparam>
      ///// <param name="value"></param>
      ///// <param name="exponent"></param>
      ///// <returns></returns>
      ///// <remarks>The count of permutations no repetitions.</remarks>
      //public TInteger FallingFactorial(TInteger exponent)
      //{
      //  System.ArgumentOutOfRangeException.ThrowIfNegative(value);
      //  System.ArgumentOutOfRangeException.ThrowIfNegative(exponent);

      //  return checked(SplitFactorial(value) / SplitFactorial(value - exponent));
      //}

      ///// <summary>Retreive <paramref name="count"/> least significant digits of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
      //public TInteger KeepLeastSignificantDigit<TRadix>(TRadix radix)
      //  where TRadix : System.Numerics.IBinaryInteger<TRadix>
      //  => value % TInteger.CreateChecked(Units.Radix.AssertMember(radix));

      ///// <summary>Retreive <paramref name="count"/> least significant digits of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
      //public TInteger KeepLeastSignificantDigits<TRadix>(TRadix radix, TInteger count)
      //  where TRadix : System.Numerics.IBinaryInteger<TRadix>
      //  => value % TInteger.CreateChecked(Units.Radix.AssertMember(radix).FastIntegerPow(count, UniversalRounding.WholeTowardZero, out var _));

      ///// <summary>Drop the leading digit of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
      //public TInteger KeepMostSignificantDigits<TRadix>(TRadix radix, TInteger count)
      //  where TRadix : System.Numerics.IBinaryInteger<TRadix>
      //  => value / TInteger.CreateChecked(radix.FastIntegerPow(value.FastDigitCount(radix) - count, UniversalRounding.WholeTowardZero, out var _));

      ///// <summary>Returns the least common multiple of all values.</summary>
      ///// <see href="https://en.wikipedia.org/wiki/Least_common_multiple"/>
      //public TInteger Lcm(params TInteger[] other)
      //{
      //  System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(other.Length);

      //  return value.LeastCommonMultiple(other.Aggregate(LeastCommonMultiple));
      //}

      ///// <summary>Returns the least common multiple of <paramref name="a"/> and <paramref name="b"/>.</summary>
      ///// <see href="https://en.wikipedia.org/wiki/Least_common_multiple"/>
      //public TInteger LeastCommonMultiple(TInteger b)
      //  => value / value.GreatestCommonDivisor(b) * b;

      ///// <summary>
      ///// <para>The rising factorial gives the number of partitions of an n-element set into x ordered sequences (possibly empty).</para>
      ///// <example>
      ///// <para>The "the number of ways to arrange n flags on x flagpoles", where all flags must be used and each flagpole can have any number of flags.</para>
      ///// <para>Equivalently, this is the number of ways to partition a set of size n (the flags) into x distinguishable parts (the poles), with a linear order on the elements assigned to each part (the order of the flags on a given pole).</para>
      ///// </example>
      ///// <para><see href="https://en.wikipedia.org/wiki/Falling_and_rising_factorials"/></para>
      ///// </summary>
      ///// <typeparam name="TInteger"></typeparam>
      ///// <param name="value"></param>
      ///// <param name="exponent"></param>
      ///// <returns></returns>
      //public TInteger RisingFactorial(TInteger exponent)
      //{
      //  System.ArgumentOutOfRangeException.ThrowIfNegative(value);
      //  System.ArgumentOutOfRangeException.ThrowIfNegative(exponent);

      //  return checked(SplitFactorial(value + exponent - TInteger.One) / SplitFactorial(value - TInteger.One));
      //}

      ///// <summary>
      ///// <para>Compute the split-factorial of the <paramref name="value"/>.</para>
      ///// <para><see href="https://en.wikipedia.org/wiki/Factorial"/></para>
      ///// <para><see href="http://www.luschny.de/math/factorial/csharp/FactorialSplit.cs.html"/></para>
      ///// </summary>
      ///// <remarks>This divide-and-conquer version is faster with numbers larger than 200 or so, but is slower than <see cref="Factorial{TValue}(TValue)"/> on smaller numbers.</remarks>
      ///// <typeparam name="TInteger"></typeparam>
      ///// <param name="value"></param>
      ///// <returns></returns>
      //public TInteger SplitFactorial()
      //{
      //  System.ArgumentOutOfRangeException.ThrowIfNegative(value);

      //  if (value <= TInteger.One)
      //    return TInteger.One;

      //  var two = (TInteger.One + TInteger.One);

      //  var p = TInteger.One;
      //  var r = TInteger.One;
      //  var currentN = TInteger.One;

      //  var h = TInteger.Zero;
      //  var shift = TInteger.Zero;
      //  var high = TInteger.One;

      //  var log2n = int.CreateChecked(TInteger.Log2(value));

      //  checked
      //  {
      //    while (h != value)
      //    {
      //      shift += h;
      //      h = value >>> log2n--;
      //      var len = high;
      //      high = (h - TInteger.One) | TInteger.One;
      //      len = (high - len) >>> 1;

      //      if (len > TInteger.Zero)
      //      {
      //        p *= Product(len);
      //        r *= p;
      //      }
      //    }

      //    return r << int.CreateChecked(shift);
      //  }

      //  TInteger Product(TInteger n)
      //  {
      //    checked
      //    {
      //      var m = n >> 1;

      //      if (TInteger.IsZero(m))
      //        return currentN += two;

      //      if (n == two)
      //        return (currentN += two) * (currentN += two);

      //      return Product(n - m) * Product(m);
      //    }
      //  }
      //}

      ///// <summary>
      ///// <para>Computes the factorial of <paramref name="value"/>, e.g. Factorial(5); = "5 * 4 * 3 * 2 * 1", but this specialized version adds a <paramref name="threshold"/> at which the algorithm stops, e.g. Factorial(5, 3); = "5 * 4 * 3".</para>
      ///// <para>This enables the prevention of double runs for certain scenarios invovling factorials, e.g. Factorial(5) / Factorial(3).</para>
      ///// <para>It also means that Factorial(5, 1) or Factorial(5, 2) operates the same as Factorial(5).</para>
      ///// <para><see href="https://en.wikipedia.org/wiki/Factorial"/></para>
      ///// </summary>
      ///// <typeparam name="TInteger"></typeparam>
      ///// <param name="value"></param>
      ///// <param name="threshold">This cannot be less than one and cannot be greater than or equal to <paramref name="value"/>.</param>
      ///// <returns></returns>
      //public TInteger TopDownFactorialWithThreshold(TInteger threshold)
      //{
      //  System.ArgumentOutOfRangeException.ThrowIfNegative(value);

      //  System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(threshold);
      //  System.ArgumentOutOfRangeException.ThrowIfGreaterThan(threshold, value);

      //  var f = TInteger.One;

      //  if (value > f) // Only loop if value is greater than 1.
      //    for (var m = value; m > threshold; m--) // Only loop from value down to the minimumThreshold.
      //      checked { f *= m; }

      //  return f;
      //}

      ///// <summary>
      ///// <para>Attempts to compute the cube-root of a <paramref name="value"/> and then rounded using the rounding <paramref name="mode"/>. The resulting <paramref name="cbrt"/> (double) and <paramref name="icbrt"/> are returned as out parameters.</para>
      ///// <para>This is a faster method, but is limited to integer input less-than-or-equal to 53 bits in size.</para>
      ///// </summary>
      ///// <typeparam name="TInteger"></typeparam>
      ///// <typeparam name="TICbrt"></typeparam>
      ///// <param name="value">The cubed number to find the root of.</param>
      ///// <param name="mode"></param>
      ///// <param name="icbrt">The integer cube-root of <paramref name="value"/>.</param>
      ///// <param name="cbrt">The floating-point cube-root of <paramref name="value"/>.</param>
      ///// <returns>Whether the operation was successful.</returns>
      //public bool TryFastIntegerCbrt<TICbrt>(Flux.UniversalRounding mode, out TICbrt icbrt, out double cbrt)
      //  where TICbrt : System.Numerics.IBinaryInteger<TICbrt>
      //{
      //  try
      //  {
      //    if (GetBitLength(value) <= 53)
      //    {
      //      icbrt = TICbrt.CreateChecked(value.FastIntegerCbrt(mode, out cbrt));

      //      return true;
      //    }
      //  }
      //  catch { }

      //  icbrt = TICbrt.Zero;
      //  cbrt = 0.0;

      //  return false;
      //}
    }
  }
}
