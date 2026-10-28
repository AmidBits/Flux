namespace Flux
{
  public static partial class BinaryInteger
  {
    extension<TInteger>(TInteger)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      internal static (TInteger LogFloor, TInteger LogCeiling, bool IsExactLog) InternalIntegerLog(TInteger value, TInteger radix)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value);
        System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radix, TInteger.One);

        var zero = TInteger.Zero;
        var one = TInteger.One;

        if (value < radix)
          return (zero, one, false);

        var log2n = TInteger.Log2(value);
        var log2b = TInteger.Log2(radix);

        TInteger exponent = log2n / log2b; // Approximate exponent derived from Log2.

        TryPowAtMost(radix, exponent, value, out var power, out exponent); // power == radix^exponent and power <= value

        while (power <= value / radix) // Correct upward.
        {
          power *= radix;
          exponent++;
        }

        var isExact = power == value;

        return (exponent, isExact ? exponent : exponent + TInteger.One, isExact);
      }

      #region CreateFormatStringWithCountDecimals

      /// <summary>
      /// <para>Returns a string format for a specified number of fractional digits.</para>
      /// </summary>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static string CreateFormatStringWithCountDecimals(TInteger count)
      {
        System.ArgumentOutOfRangeException.ThrowIfLessThan(count, TInteger.One);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(count, TInteger.CreateChecked(339));

        return "0." + new string('#', int.CreateChecked(count));
      }

      #endregion

      #region DirichletConvolution

      /// <summary>
      /// <para>Computes the Dirichlet convolution of two arithmetic functions f and g for a given n.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Dirichlet_convolution"/></para>
      /// </summary>
      /// <param name="n">The positive integer for which to compute the convolution.</param>
      /// <param name="f">Function f: int -> long</param>
      /// <param name="g">Function g: int -> long</param>
      /// <returns>The value of (f * g)(n)</returns>
      public static TInteger DirichletConvolution(TInteger n, Func<TInteger, TInteger> f, Func<TInteger, TInteger> g)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);

        return GetDivisors(n).Select(d => f(d) * g(n / d)).Sum();
      }

      ///// <summary>
      ///// <para></para>
      ///// <para><see href="https://en.wikipedia.org/wiki/Dirichlet_convolution"/></para>
      ///// </summary>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger DirichletConvolutionFunction(TInteger n)
      //{
      //  var result = TInteger.Zero;

      //  for (var d = TInteger.One; d <= n; d++)
      //    if (TInteger.IsZero(n % d)) // If d is a divisor of n, add f(d) * g(n//d) to the result.
      //      result += SumDivisors(d).Sum * EulerTotient(n / d);

      //  return result;
      //}

      #endregion

      #region GenerateSubRanges

      /// <summary>
      /// <para>Generates a new sequence with <paramref name="count"/> <see cref="System.Range"/> objects.</para>
      /// <para>How many sub-ranges do you want?</para>
      /// </summary>
      /// <param name="length"></param>
      /// <param name="count"></param>
      /// <returns></returns>
      public static System.Collections.Generic.List<System.Range> GenerateSubRangesByCount(TInteger length, TInteger count)
        => GenerateSubRangesBySubLength(length, Number.IntegerDivRemCeiling(length, count).Quotient);

      /// <summary>
      /// <para>Generates a new sequence of <see cref="System.Range"/> objects, each with <paramref name="subLength"/> (the last may contain less) elements from the total length of a super-sequence.</para>
      /// <para>How many items do you want in each sub-range?</para>
      /// </summary>
      /// <param name="length"></param>
      /// <param name="subLength"></param>
      /// <returns></returns>
      public static System.Collections.Generic.List<System.Range> GenerateSubRangesBySubLength(TInteger length, TInteger subLength)
      {
        var subRanges = new System.Collections.Generic.List<System.Range>();

        for (var index = TInteger.Zero; index < length; index += subLength)
          subRanges.Add(RangeExtensions.FromOffsetAndLength(int.CreateChecked(index), int.CreateChecked(TInteger.Min(subLength, length - index))));

        return subRanges;
      }

      #endregion

      #region CubeRoot functions

      /// <summary>
      /// <para>Computes the integer (floor) cube-root of a value.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <returns>The square-root of the value.</returns>
      public static TInteger CubeRoot(TInteger value)
      {
        if (TInteger.IsZero(value))
          return value;

        var abs = TInteger.Abs(value);
        var sign = TInteger.CopySign(TInteger.One, value);

        if (TryConvertTo(abs, out ulong ulv))
          return TInteger.CreateChecked(ulong.Cbrt(ulv)) * sign;

        return TInteger.CreateChecked(System.Numerics.BigInteger.Cbrt(System.Numerics.BigInteger.CreateChecked(abs))) * sign;
      }

      /// <summary>
      /// <para>Indicates whether <paramref name="value"/> is the integer (not necessarily perfect) square of <paramref name="root"/>.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value">The square value to find the square-<paramref name="root"/> of.</param>
      /// <param name="root">The resulting square-root of <paramref name="value"/>.</param>
      /// <returns>Whether the <paramref name="value"/> is the integer (not necessarily perfect) square of <paramref name="root"/>.</returns>
      public static bool IsCubeRoot(TInteger value, TInteger root)
        => value >= (root * root * root) // If GTE to cube of root.
        && value < (root + TInteger.One) * (root + TInteger.One) * (root + TInteger.One); // And if LT to cube of (root + 1).

      /// <summary>
      /// <para>Indicates whether <paramref name="square"/> is a perfect square of <paramref name="root"/>.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="square">The square value to find the square-<paramref name="root"/> of.</param>
      /// <param name="root">The resulting square-root of <paramref name="square"/>.</param>
      /// <returns>Whether the <paramref name="square"/> is a perfect square of <paramref name="root"/>.</returns>
      /// <remarks>Not using "y == (x * x)" because risk of overflow.</remarks>
      public static bool IsPerfectCubeRoot(TInteger value, TInteger root)
        => value == (root * root * root);

      #endregion

      #region JosephusProblem

      /// <summary>
      /// <para>Calculates the last longest surviving position (it's not a 0-based index) of the Flavius Josephus problem where <paramref name="value"/> people stand in a circle and every <paramref name="k"/> person commits suicide.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Josephus_problem"/></para>
      /// </summary>
      /// <remarks>This is about counting positions, so it is 1-based position that is computed.</remarks>
      /// <param name="value">The number of people in the initial circle.</param>
      /// <param name="k">The count of each step. I.e. k-1 people are skipped and the k-th is executed.</param>
      /// <returns>The 1-indexed position that the survivor occupies.</returns>
      public static TInteger JosephusProblem(TInteger n, TInteger k)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(k);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(k, n);

        var survivingPosition = TInteger.Zero;

        for (var positionCounter = TInteger.One; positionCounter <= n; positionCounter++)
          survivingPosition = (survivingPosition + k) % positionCounter;

        return survivingPosition + TInteger.One;
      }

      #endregion

      #region Log

      /// <summary>
      /// <para>Returns the integer (toward-zero, away-from-zero) logarithm of specified a <paramref name="value"/> in a specified <paramref name="radix"/>.</para>
      /// </summary>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="value"></param>
      /// <param name="radix"></param>
      /// <returns></returns>
      public static (TInteger LogTowardZero, TInteger LogAwayFromZero, bool IsExactLog) Log<TRadix>(TInteger value, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radix, TRadix.One);

        if (TInteger.IsZero(value))
          return (value, value, false);

        var abs = TInteger.Abs(value);
        var sign = TInteger.CopySign(TInteger.One, value);

        if (TryConvertTo(abs, out ulong ulv) && TryConvertTo(radix, out ulong ulr))
        {
          var (LogF, LogC, IsExactLog) = ulong.Log(ulv, ulr);

          return (TInteger.CreateChecked(LogF) * sign, TInteger.CreateChecked(LogC) * sign, IsExactLog);
        }
        else // Fall back on BigInteger LOG implementation.
        {
          var (LogF, LogC, IsExactLog) = System.Numerics.BigInteger.Log(System.Numerics.BigInteger.CreateChecked(abs), System.Numerics.BigInteger.CreateChecked(radix));

          return (TInteger.CreateChecked(LogF) * sign, TInteger.CreateChecked(LogC) * sign, IsExactLog);
        }
      }

      #endregion

      #region LogE

      /// <summary>
      /// <para>Returns the integer (floor) natural logarithm of specified a <paramref name="value"/> in a base/radix E.</para>
      /// </summary>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="value"></param>
      /// <returns></returns>
      public static (TInteger LogTowardZero, TInteger LogAwayFromZero) LogE<TRadix>(TInteger value)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        if (TInteger.IsZero(value))
          return (value, value);

        var absV = TInteger.Abs(value);
        var sign = TInteger.CopySign(TInteger.One, value);

        if (TryConvertTo(absV, out ulong ulv))
        {
          var (ilogf, ilogc) = ulong.LogE(ulv);

          return (TInteger.CreateChecked(ilogf) * sign, TInteger.CreateChecked(ilogc) * sign);
        }
        else // Fall-back on BigInteger ILog, which is more expensive but can handle any size of integer.
        {
          var (ilogf, ilogc) = System.Numerics.BigInteger.LogE(System.Numerics.BigInteger.CreateChecked(absV));

          return (TInteger.CreateChecked(ilogf) * sign, TInteger.CreateChecked(ilogc) * sign);
        }
      }

      #endregion

      #region RootN functions

      private static TInteger RootNCore(TInteger value, TInteger degree)
      {
        if (value <= TInteger.One || degree <= TInteger.One)
          return value;

        var x = TInteger.One << int.CreateChecked(TInteger.Log2(value) / degree); // Initial estimate: 2^(floor(log2(value)/degree))

        while (true)
        {
          var prev = x;

          var t = PowOrLimit(x, degree - TInteger.One, value);

          if (t == TInteger.Zero)
            throw new ArithmeticException();

          x = ((degree - TInteger.One) * x + value / t) / degree;

          if (x == prev)
          {
            while (PowOrLimit(x + TInteger.One, degree, value) <= value) // Floor-root correction.
              x++;

            while (PowOrLimit(x, degree, value) > value)
              x--;

            return x;
          }
        }
      }

      /// <summary>
      /// <para>Computes the integer nth-root of a value.</para>
      /// </summary>
      /// <typeparam name="TNth"></typeparam>
      /// <param name="value"></param>
      /// <param name="exponent"></param>
      /// <returns></returns>
      public static TInteger RootN(TInteger value, TInteger degree)
      {
        //return TInteger.CreateChecked(BigIntegerExtensions.RootN(System.Numerics.BigInteger.CreateChecked(value), int.CreateChecked(degree)));
        System.ArgumentOutOfRangeException.ThrowIfNegative(value);
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(degree);

        return RootNCore(value, degree);
      }

      public static bool TryGetExactRoot(TInteger value, TInteger degree, out TInteger root)
      {
        root = RootNCore(value, degree);

        return PowOrLimit(root, degree, value) == value;
      }

      public static bool IsRootN<TNth>(TInteger value, TNth n, TInteger root)
        where TNth : System.Numerics.IBinaryInteger<TNth>
        => value >= Pow(root, n) // If GTE to nth of root.
        && value < Pow(root + TInteger.One, n); // And if LT to nth of (root + 1).

      public static bool IsPerfectRootN<TNth>(TInteger value, TNth n, TInteger root)
        where TNth : System.Numerics.IBinaryInteger<TNth>
        => value == Pow(root, n);

      #endregion

      #region SquareRoot functions

      /// <summary>
      /// <para>Computes the integer square-root of a value.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static TInteger SquareRoot(TInteger value)
      {
        if (TInteger.IsZero(value))
          return value;

        var abs = TInteger.Abs(value);
        var sign = TInteger.CopySign(TInteger.One, value);

        if (TryConvertTo(abs, out ulong ulv))
          return TInteger.CreateChecked(ulong.Sqrt(ulv)) * sign;
        else // Fall back on BigInteger SQRT implementation.
          return TInteger.CreateChecked(System.Numerics.BigInteger.Sqrt(System.Numerics.BigInteger.CreateChecked(abs))) * sign;
      }

      /// <summary>
      /// <para>Indicates whether <paramref name="value"/> is the integer (not necessarily perfect) square of <paramref name="root"/>.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value">The square value to find the square-<paramref name="root"/> of.</param>
      /// <param name="root">The resulting square-root of <paramref name="value"/>.</param>
      /// <returns>Whether the <paramref name="value"/> is the integer (not necessarily perfect) square of <paramref name="root"/>.</returns>
      public static bool IsSquareRoot(TInteger value, TInteger root)
        => value >= (root * root) // If GTE to square of root.
        && value < (root + TInteger.One) * (root + TInteger.One); // And if LT to square of (root + 1).

      /// <summary>
      /// <para>Indicates whether <paramref name="square"/> is a perfect square of <paramref name="root"/>.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="square">The square value to find the square-<paramref name="root"/> of.</param>
      /// <param name="root">The resulting square-root of <paramref name="square"/>.</param>
      /// <returns>Whether the <paramref name="square"/> is a perfect square of <paramref name="root"/>.</returns>
      /// <remarks>Not using "y == (x * x)" because risk of overflow.</remarks>
      public static bool IsPerfectSquareRoot(TInteger value, TInteger root)
        => value == (root * root);

      #endregion

      #region Pow

      /// <summary>
      /// <para>Indicates whether <paramref name="value"/> is a power of <paramref name="radix"/> (base).</para>
      /// </summary>
      /// <remarks>This version also handles negative values simply by mirroring the corresponding positive value. Zero return as false.</remarks>
      public static bool IsPowOf<TRadix>(TInteger value, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        if (value <= TInteger.Zero)
          return false;

        if (radix == TRadix.One + TRadix.One) // Fast path for radix = 2 (bit trick).
          return (value & (value - TInteger.One)) == TInteger.Zero;

        AssertRadix(radix, out TInteger r);

        var v = value;

        while (true) // General case: repeatedly divide by radix until it no longer divides, which is O(log n) but extremely fast in practice.
        {
          var q = v / r;
          var p = q * r;

          if (p != v) // If not divisible, it's not a power.
            return false;

          if (q == TInteger.One) // If quotient is 1, we reached radix^k.
            return true;

          v = q;
        }
      }

      public static int GetDecimalDigitLength(TInteger value)
      {
        var abs = TInteger.Abs(value);

        if (abs <= TInteger.CreateChecked(9)) // 0-9.
          return 1;

        var digits = ((abs.GetShortestBitLength() * 1233) >> 12) + 1;

        if (abs < TInteger.CreateChecked(Pow(10, digits - 1)))
          digits--;

        return digits;
      }

      /// <summary>
      /// <para>Computes the required bit length for the result of <paramref name="value"/> raised to the power of <paramref name="exponent"/>.</para>
      /// </summary>
      /// <typeparam name="TExponent"></typeparam>
      /// <param name="value"></param>
      /// <param name="exponent"></param>
      /// <returns></returns>
      public static int PowRequiredBitLength<TExponent>(TInteger value, TExponent exponent)
        where TExponent : System.Numerics.IBinaryInteger<TExponent>
        => checked(value.BitLength * int.CreateChecked(exponent));

      ///// <summary>
      ///// Computes the power using binary exponentiation (exponentiation by squaring).
      ///// </summary>
      ///// <remarks>Performs exponentiation by squaring and uses checked arithmetic to detect overflow. If
      ///// exponent is less than or equal to TExponent.Zero, returns TInteger.One. Requires TInteger to support
      ///// multiplication and provide a One value.</remarks>
      ///// <typeparam name="TExponent">The exponent type that implements System.Numerics.IBinaryInteger<TExponent>; represents the integer exponent
      ///// and must support binary operations and comparisons.</typeparam>
      ///// <param name="value">Base value to be raised.</param>
      ///// <param name="exponent">Exponent specifying the power; expected to be non-negative and manipulated via bitwise operations and right
      ///// shifts.</param>
      ///// <returns>The computed value raised to the specified exponent.</returns>
      //public static TInteger PowBinaryExponentiation<TExponent>(TInteger value, TExponent exponent)
      //  where TExponent : System.Numerics.IBinaryInteger<TExponent>
      //{
      //  var result = TInteger.One;

      //  while (exponent > TExponent.Zero)
      //    checked // Detect overflow.
      //    {
      //      if (!TExponent.IsZero(exponent & TExponent.One)) // If the current exponent is odd, multiply the result by the current value.
      //        result *= value;

      //      exponent >>= 1;

      //      if (exponent > TExponent.Zero) // If the exponent is still greater than zero, square the current value for the next iteration.
      //        value *= value;
      //    }

      //  return result;
      //}
      //public static TInteger Pow2<TExponent>(TInteger exponent)
      //{
      //  if (TInteger.IsNegative(exponent)) throw new System.ArgumentOutOfRangeException(nameof(exponent));

      //  if (TInteger.IsZero(exponent))
      //    return TInteger.One;

      //  if (exponent == TInteger.One)
      //    return TInteger.CreateChecked(2);

      //  if (exponent >= exponent.BitCount && typeof(TInteger).IsNumericsUnsignedInteger)
      //}

      ///// <summary>
      ///// <para>Computes 10^exponent using a precomputed table for results up to 128 bits.</para>
      ///// </summary>
      ///// <param name="exponent"></param>
      ///// <param name="result"></param>
      ///// <returns></returns>
      //public static bool TryPowFast<TExponent>(TInteger value, TExponent exponent, out TInteger result)
      //  where TExponent : System.Numerics.IBinaryInteger<TExponent>
      //{
      //  var e = int.CreateChecked(exponent);

      //  if (e == 1) // value^1
      //  {
      //    result = value;
      //    return true;
      //  }

      //  if (e == 2) // value^2
      //  {
      //    result = checked(value * value);
      //    return true;
      //  }

      //  if (e == 3) // value^3
      //  {
      //    result = checked(value * value * value);
      //    return true;
      //  }

      //  if (value == TInteger.CreateChecked(2)) // 2^e
      //  {
      //    result = TInteger.One << int.CreateChecked(exponent);
      //    return true;
      //  }

      //  if (value == TInteger.CreateChecked(10) && e < UInt128Extensions.m_pow10.Length) // 10^e where e is an existing index in the table.
      //  {
      //    result = TInteger.CreateChecked(UInt128Extensions.m_pow10[int.CreateChecked(exponent)]);
      //    return true;
      //  }

      //  result = TInteger.Zero;
      //  return false;
      //}

      /// <summary>
      /// <para>Computes <paramref name="value"/> raised to the power of <paramref name="exponent"/>.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="exponent"></param>
      /// <returns></returns>
      public static TInteger Pow<TExponent>(TInteger value, TExponent exponent)
        where TExponent : System.Numerics.IBinaryInteger<TExponent>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(exponent);

        if (TExponent.IsZero(exponent)) // (value^0 : 1)
          return TInteger.One;

        if (TInteger.IsZero(value)) // (0^exponent : 0)
          return TInteger.Zero;

        if (exponent == TExponent.One) // (value^1 : value)
          return value;

        if (exponent == TExponent.CreateChecked(2)) // (value^2 : value * value)
          return checked(value * value);

        if (value == TInteger.CreateChecked(2)) // (2^exponent : 1 << exponent) 
          return (int.CreateChecked(exponent) is var ie && ie < value.BitWidth)
            ? checked(TInteger.One << ie)
            : throw new System.OverflowException($"The result of {value}^{exponent} cannot be represented by {typeof(TInteger)}.");

        if (value == TInteger.CreateChecked(10) && exponent < TExponent.CreateChecked(UInt128Extensions.m_pow10.Length)) // 10^exponent, where exponent is the size of an existing index in the table.
          return TInteger.CreateChecked(UInt128Extensions.m_pow10[int.CreateChecked(exponent)]);

        if (typeof(TInteger) == typeof(System.Numerics.BigInteger)) // If BigInteger, use its optimized static pow() method.
          return (TInteger)(object)System.Numerics.BigInteger.Pow((System.Numerics.BigInteger)(object)value, int.CreateChecked(exponent)); // If TInteger is BigInteger, use it as such.

        // Otherwise, do binary exponentiation (exponentiation by squaring), which is really fast for the non-BigInteger types.

        var result = TInteger.One;

        while (exponent > TExponent.Zero)
          checked // Detect overflow.
          {
            if (!TExponent.IsZero(exponent & TExponent.One)) // If the current exponent is odd, multiply the result by the current value.
              result *= value;

            exponent >>= 1;

            if (exponent > TExponent.Zero) // If the exponent is still greater than zero, square the current value for the next iteration.
              value *= value;
          }

        return result;
      }

      public static TInteger Pow10(TInteger exponent)
        => Pow(TInteger.CreateChecked(10), exponent);

      ///// <summary>
      ///// <para>Computes <paramref name="value"/> raised to the power of <paramref name="exponent"/>.</para>
      ///// <para>Uses the built-in <see cref="System.Numerics.BigInteger"/> function.</para>
      ///// </summary>
      ///// <typeparam name="TInteger"></typeparam>
      ///// <param name="exponent">The exponent with which to raise the value.</param>
      ///// <returns>The value raised to the <paramref name="exponent"/>-of.</returns>
      ///// <remarks>If <paramref name="value"/> and/or <paramref name="exponent"/> are zero, 1 is returned. I.e. 0&#x2070;, x&#x2070; and 0&#x02E3; all return 1 in this version.</remarks>
      ///// <exception cref="System.ArgumentOutOfRangeException"></exception>
      //public static TInteger Pow<TExponent>(TInteger value, TExponent exponent)
      //  where TExponent : System.Numerics.IBinaryInteger<TExponent>
      //  => TInteger.CreateChecked(System.Numerics.BigInteger.Pow(System.Numerics.BigInteger.CreateChecked(value), int.CreateChecked(exponent)));

      /// <summary>
      /// <para>Attempts to compute value^exponent without exceeding limit.</para>
      /// <para>Returns true if the full exponent was achieved.</para>
      /// <para>Returns false if the next multiplication would exceed the limit.</para>
      /// <para>In either case:</para>
      /// <list type="bullet">
      /// <item>result == value^achievedExponent</item>
      /// <item>achievedExponent <= exponent</item>
      /// <item>result <= limit</item>
      /// </list>
      /// </summary>
      internal static bool TryPowAtMost<TExponent>(TInteger value, TExponent exponent, TInteger limit, out TInteger result, out TExponent achievedExponent)
        where TExponent : System.Numerics.IBinaryInteger<TExponent>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(exponent);

        result = TInteger.One;
        achievedExponent = TExponent.Zero;

        while (achievedExponent < exponent)
        {
          if (result > limit / value)
            return false;

          result *= value;
          achievedExponent++;
        }

        return true;
      }

      /// <summary>
      /// <para>Overflow‑safe exponentiation: x^k, stops early if exceeding limit.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="exponent"></param>
      /// <param name="limit"></param>
      /// <returns></returns>
      public static TInteger PowOrLimit(TInteger value, TInteger exponent, TInteger limit)
      {
        if (TInteger.IsZero(value))
          return TInteger.IsZero(exponent) ? TInteger.One : TInteger.Zero;

        var result = TInteger.One;
        var overflow = limit + TInteger.One;

        while (exponent > TInteger.Zero)
        {
          if (!TInteger.IsZero(exponent & TInteger.One))
          {
            if (result > limit / value) // Check: result * value > limit ?
              return overflow;

            result *= value;

            if (result > limit)
              return overflow;
          }

          exponent >>= 1;

          if (TInteger.IsZero(exponent))
            break;

          if (value > limit / value) // Check: value * value > limit ?
            return overflow;

          value *= value;

          if (value > limit)
            return overflow;
        }

        return result;
      }

      private static int PowCompare1(TInteger value, TInteger exponent, TInteger limit)
      {
        var result = TInteger.One;

        while (!TInteger.IsZero(exponent))
        {
          if (!TInteger.IsZero(exponent & TInteger.One))
          {
            result *= value;

            if (result > limit)
              return +1;
          }

          exponent >>= 1;

          if (!TInteger.IsZero(exponent))
          {
            value *= value;

            if (value > limit && !TInteger.IsZero(exponent))
              value = limit + TInteger.One;
          }
        }

        return result.CompareTo(limit);
      }

      private static int PowCompare2(TInteger value, TInteger exponent, TInteger limit)
      {
        var result = TInteger.One;

        while (!TInteger.IsZero(exponent))
        {
          if (!TInteger.IsZero(exponent & TInteger.One))
          {
            result *= value;

            if (result > limit)
              return 1;
          }

          exponent >>= 1;

          if (!TInteger.IsZero(exponent))
          {
            value *= value;

            if (value > limit) // Saturate once the remaining computation can no longer produce a value <= limit.
              value = limit + TInteger.One;
          }
        }

        return result.CompareTo(limit);
      }
      #endregion

      #region ToOrdinalFieldName(s)

      /// <summary>
      /// <para>Returns a generic <paramref name="fieldNamePrefix"/> for the <paramref name="fieldIndex"/> as if it was an index of a 0-based column-structure.</para>
      /// <para>+1 is added to the <paramref name="fieldIndex"/> so that the first column (the zeroth) is always "Column1", and the second column (#1) is "Column2", i.e. the column names are ordinal.</para>
      /// </summary>
      /// <param name="fieldIndex"></param>
      /// <param name="numericWidth"></param>
      /// <param name="fieldNamePrefix"></param>
      /// <returns></returns>
      public static string ToOrdinalFieldName(TInteger fieldIndex, int numericWidth, string fieldNamePrefix = "Column")
        => fieldNamePrefix + (fieldIndex + TInteger.One).ToString($"D{numericWidth}", null);

      /// <summary>
      /// <para>Returns a generic <paramref name="fieldNamePrefix"/> for the <paramref name="fieldIndex"/> as if it was an index of a 0-based column-structure.</para>
      /// <para>+1 is added to the <paramref name="fieldIndex"/> so that the first column (the zeroth) is always "Column1", and the second column (#1) is "Column2", i.e. the column names are ordinal.</para>
      /// </summary>
      /// <param name="fieldIndex"></param>
      /// <param name="fieldNamePrefix"></param>
      /// <returns></returns>
      public static string ToOrdinalFieldName(TInteger fieldIndex, string fieldNamePrefix = "Column")
        => ToOrdinalFieldName(fieldIndex, int.CreateChecked(fieldIndex <= TInteger.Zero ? TInteger.Zero : DigitCount(fieldIndex, TInteger.CreateChecked(10))), fieldNamePrefix);

      /// <summary>
      /// <para>Creates an array of generic column-<paramref name="fieldNamePrefix"/>s for <paramref name="fieldCount"/> amount of columns.</para>
      /// <example><paramref name="fieldCount"/> = 3, returns <c>["Column1", "Column2", "Column3"]</c></example>
      /// </summary>
      /// <param name="fieldCount"></param>
      /// <param name="fieldNamePrefix"></param>
      /// <returns></returns>
      public static string[] ToOrdinalFieldNames(TInteger fieldCount, string fieldNamePrefix = "Column")
      {
        var maxWidth = int.CreateChecked(DigitCount(fieldCount, 10));

        return [.. Number.ArithmeticSequence(TInteger.One, fieldCount).Select(ci => ToOrdinalFieldName(ci, maxWidth, fieldNamePrefix))];
      }

      #endregion

      #region TryConvertTo

      public static bool TryConvertTo<TTarget>(TInteger source, out TTarget target)
        where TTarget : System.Numerics.IBinaryInteger<TTarget>
      {
        target = TTarget.CreateTruncating(source); // Convert TInteger → TTarget (truncating, never throws)

        var roundTrip = TInteger.CreateTruncating(target); // Convert back TTarget → TInteger (truncating, never throws)

        return roundTrip == source; // If round-trip preserved the value, it fits exactly.
      }

      #endregion

      //#region Twelvefold way

      ///// <summary>
      ///// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      ///// </summary>
      ///// <param name="x"></param>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger AnyDistinct(TInteger x, TInteger n) => Pow(x, n);

      ///// <summary>
      ///// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      ///// </summary>
      ///// <param name="x"></param>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger InjectiveDistinct(TInteger x, TInteger n) => FallingFactorial(x, n);

      ///// <summary>
      ///// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      ///// </summary>
      ///// <param name="x"></param>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger SurjectiveDistinct(TInteger x, TInteger n) => Factorial(x) * StirlingNumber2ndKind(n, x);

      ///// <summary>
      ///// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      ///// </summary>
      ///// <param name="x"></param>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger AnySnOrbits(TInteger x, TInteger n) => BinomialCoefficient(x + n - TInteger.One, n);

      ///// <summary>
      ///// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      ///// </summary>
      ///// <param name="x"></param>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger InjectiveSnOrbits(TInteger x, TInteger n) => BinomialCoefficient(x, n);

      ///// <summary>
      ///// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      ///// </summary>
      ///// <param name="x"></param>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger SurjectiveSnOrbits(TInteger x, TInteger n) => BinomialCoefficient(n - TInteger.One, n - x);

      ///// <summary>
      ///// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      ///// </summary>
      ///// <param name="x"></param>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger AnySxOrbits(TInteger x, TInteger n) => throw new System.NotImplementedException();

      ///// <summary>
      ///// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      ///// </summary>
      ///// <param name="x"></param>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger InjectiveSxOrbits(TInteger x, TInteger n) => throw new System.NotImplementedException();

      ///// <summary>
      ///// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      ///// </summary>
      ///// <param name="x"></param>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger SurjectiveSxOrbits(TInteger x, TInteger n) => StirlingNumber2ndKind(n, x);

      ///// <summary>
      ///// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      ///// </summary>
      ///// <param name="x"></param>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger AnySnSxOrbits(TInteger x, TInteger n) => throw new System.NotImplementedException();

      ///// <summary>
      ///// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      ///// </summary>
      ///// <param name="x"></param>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger InjectiveSnSxOrbits(TInteger x, TInteger n) => throw new System.NotImplementedException();

      ///// <summary>
      ///// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      ///// </summary>
      ///// <param name="x"></param>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger SurjectiveSnSxOrbits(TInteger x, TInteger n) => throw new System.NotImplementedException();

      //#endregion
    }

    #region ..DeBruijnSequence.. (has nested methods)

    /// <summary>
    /// <para>Returns the total length of the DeBruijn sequence.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/De_Bruijn_sequence"/></para>
    /// <para><seealso href="https://www.rosettacode.org/wiki/De_Bruijn_sequences"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="k"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    /// <remarks>The formula for the length is <c>(<paramref name="k"/> * <paramref name="n"/> + <paramref name="n"/> - 1)</c>.</remarks>
    public static int DeBruijnSequenceLength(int k, int n)
    {
      System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(k);
      System.ArgumentOutOfRangeException.ThrowIfNegative(n);

      return (int)System.Numerics.BigInteger.Pow(k, n) + n - 1;
    }

    /// <summary>
    /// <para>Creates a new DeBruijn sequence with DeBruijn numbers, which are the indices in a <paramref name="k"/> alphabet (e.g. 10 digit number pad) of <paramref name="n"/> size (e.g. 4 digit codes).</para>
    /// <para>The indices can be translated into symbols using an "alphabet".</para>
    /// <para><see href="https://en.wikipedia.org/wiki/De_Bruijn_sequence"/></para>
    /// <para><seealso href="https://www.rosettacode.org/wiki/De_Bruijn_sequences"/></para>
    /// </summary>
    /// <param name="k"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Collections.Generic.List<int> GenerateDeBruijnSequence(int k, int n)
    {
      var sequence = new System.Collections.Generic.List<int>(DeBruijnSequenceLength(k, n));

      var a = new int[k * n];

      DeBruijn(1, 1);

      sequence.AddRange(sequence.GetRange(0, n - 1));

      return sequence;

      void DeBruijn(int t, int p)
      {
        if (t > n)
        {
          if ((n % p) == 0)
            sequence.AddRange(new System.ArraySegment<int>(a, 1, p));
        }
        else
        {
          a[t] = a[t - p];
          DeBruijn(t + 1, p);
          var j = a[t - p] + 1;

          while (j < k)
          {
            a[t] = j;
            DeBruijn(t + 1, t);
            j++;
          }
        }
      }
    }

    /// <summary>
    /// <para>Creates a new expanded DeBruijn sequence of indices based on <paramref name="k"/> and <paramref name="n"/>.</para>
    /// </summary>
    /// <param name="k"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<int[]> GenerateDeBruijnSequenceExpanded(int k, int n)
      => GenerateDeBruijnSequence(k, n).PartitionNgram(n, (e, i) => e.ToArray());

    /// <summary>
    /// <para>Creates a new expanded DeBruijn sequence of symbols based on <paramref name="k"/>, <paramref name="n"/> and <paramref name="alphabet"/>.</para>
    /// </summary>
    /// <typeparam name="TSymbol"></typeparam>
    /// <param name="k"></param>
    /// <param name="n"></param>
    /// <param name="alphabet"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<TSymbol>> GenerateDeBruijnSequenceExpandedSymbols<TSymbol>(int k, int n, params TSymbol[] alphabet)
      => GenerateDeBruijnSequence(k, n).PartitionNgram(n, (e, i) => e.Select(i => alphabet[i]).ToList());

    #endregion
  }
}
