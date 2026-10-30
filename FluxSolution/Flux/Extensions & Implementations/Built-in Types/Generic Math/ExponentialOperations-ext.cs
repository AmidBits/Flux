namespace Flux
{
  public static partial class BinaryInteger
  {
    extension<TInteger>(TInteger)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      #region IntegerCbrt functions

      /// <summary>
      /// <para>Computes the integer (floor) cube-root of a value.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <returns>The square-root of the value.</returns>
      public static TInteger IntegerCbrt(TInteger value)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(value);

        if (value < NumberConstants<TInteger>.Eight)
          return value == TInteger.Zero ? TInteger.Zero : TInteger.One;

        var x = TInteger.One << int.CreateChecked((TInteger.Log2(value) + NumberConstants<TInteger>.Two) / NumberConstants<TInteger>.Three);

        while (true)
        {
          var y = (NumberConstants<TInteger>.Two * x + value / (x * x)) / NumberConstants<TInteger>.Three;

          if (y >= x)
            return x;

          x = y;
        }
      }

      /// <summary>
      /// <para>Indicates whether <paramref name="value"/> is the integer (not necessarily perfect) square of <paramref name="root"/>.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value">The square value to find the square-<paramref name="root"/> of.</param>
      /// <param name="root">The resulting square-root of <paramref name="value"/>.</param>
      /// <returns>Whether the <paramref name="value"/> is the integer (not necessarily perfect) square of <paramref name="root"/>.</returns>
      public static bool IsIntegerCbrt(TInteger value, TInteger root)
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
      public static bool IsPerfectIntegerCbrt(TInteger value, TInteger root)
        => value == (root * root * root);

      #endregion

      #region IntegerLog

      //public static (TInteger LogFloor, TInteger LogCeiling, bool IsExactLog) IntegerLogCore(TInteger value, TInteger radix)
      //{
      //  System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radix, TInteger.One);

      //  if (TInteger.IsZero(value))
      //    return (TInteger.Zero, TInteger.Zero, true);

      //  if (value < radix)
      //    return (TInteger.Zero, TInteger.One, false);

      //  if (value == radix)
      //    return (TInteger.One, TInteger.One, true);

      //  var logFloor = TInteger.Log2(value) / TInteger.Log2(radix);

      //  var power = Pow(radix, logFloor, value);

      //  while (power > value)
      //  {
      //    power /= radix;
      //    logFloor--;
      //  }

      //  while (power <= value / radix)
      //  {
      //    power *= radix;
      //    logFloor++;
      //  }

      //  var exact = power == value;

      //  return (logFloor, exact ? logFloor : logFloor + TInteger.One, exact);
      //}

      /// <summary>
      /// <para>Returns the integer (toward-zero, away-from-zero) logarithm of specified a <paramref name="value"/> in a specified <paramref name="radix"/>.</para>
      /// </summary>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="value"></param>
      /// <param name="radix"></param>
      /// <returns></returns>
      public static (TInteger LogFloor, TInteger LogCeiling, bool IsExactLog) IntegerLog<TRadix>(TInteger value, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(value);
        System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radix, TRadix.One);

        var zero = TInteger.Zero;
        var one = TInteger.One;

        var rdx = TInteger.CreateChecked(radix);

        if (TInteger.IsZero(value))
          return (zero, zero, false);

        if (value < rdx)
          return (zero, one, false);

        var log2n = TInteger.Log2(value);
        var log2b = TInteger.Log2(rdx);

        TInteger exponent = log2n / log2b; // Approximate logarithm exponent derived from Log2.

        TryPow(rdx, exponent, value, out var power, out exponent); // power == radix^exponent and power <= value

        while (power <= value / rdx) // Correct upward.
        {
          power *= rdx;
          exponent++;
        }

        var isExact = power == value;

        return (exponent, isExact ? exponent : exponent + TInteger.One, isExact);
      }
      //public static (TInteger IntegerLogTowardZero, TInteger IntegerLogAwayFromZero, bool IsExactLog) IntegerLog<TRadix>(TInteger value, TRadix radix)
      //  where TRadix : System.Numerics.IBinaryInteger<TRadix>
      //{
      //  System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radix, TRadix.One);

      //  var rdx = TInteger.CreateChecked(radix);

      //  if (TInteger.IsZero(value))
      //    return (TInteger.Zero, TInteger.Zero, true);

      //  if (value < rdx)
      //    return (TInteger.Zero, TInteger.One, false);

      //  if (value == rdx)
      //    return (TInteger.One, TInteger.One, true);

      //  var logFloor = TInteger.Log2(value) / TInteger.Log2(rdx);

      //  var power = Pow(rdx, logFloor, value);

      //  while (power > value)
      //  {
      //    power /= rdx;
      //    logFloor--;
      //  }

      //  while (power <= value / rdx)
      //  {
      //    power *= rdx;
      //    logFloor++;
      //  }

      //  var exact = power == value;

      //  return (logFloor, exact ? logFloor : logFloor + TInteger.One, exact);
      //}
      //{
      //  System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radix, TRadix.One);

      //  if (TInteger.IsZero(value))
      //    return (value, value, false);

      //  var abs = TInteger.Abs(value);
      //  var sign = TInteger.CopySign(TInteger.One, value);

      //  if (TryConvertTo(abs, out ulong ulv) && TryConvertTo(radix, out ulong ulr))
      //  {
      //    var (LogF, LogC, IsExactLog) = ulong.IntegerLog(ulv, ulr);

      //    return (TInteger.CreateChecked(LogF) * sign, TInteger.CreateChecked(LogC) * sign, IsExactLog);
      //  }
      //  else // Fall back on BigInteger LOG implementation.
      //  {
      //    var (LogF, LogC, IsExactLog) = System.Numerics.BigInteger.IntegerLog(System.Numerics.BigInteger.CreateChecked(abs), System.Numerics.BigInteger.CreateChecked(radix));

      //    return (TInteger.CreateChecked(LogF) * sign, TInteger.CreateChecked(LogC) * sign, IsExactLog);
      //  }
      //}

      #endregion

      #region IntegerRootN functions

      /// <summary>
      /// <para>Computes the integer nth-root of a value.</para>
      /// </summary>
      /// <typeparam name="TNth"></typeparam>
      /// <param name="value"></param>
      /// <param name="exponent"></param>
      /// <returns></returns>
      public static TInteger IntegerRootN(TInteger value, TInteger degree)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(value);
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(degree);

        if (value <= TInteger.One || degree == TInteger.One)
          return value;

        var x = TInteger.One << int.CreateChecked((TInteger.Log2(value) + degree - TInteger.One) / degree); // Initial estimate: 2^(ceil(log2(value) / degree))

        while (true)
        {
          var t = Pow(x, degree - TInteger.One, value);

          var y = ((degree - TInteger.One) * x + value / t) / degree;

          if (y >= x)
            return x;

          x = y;
        }
      }

      public static bool TryGetExactRoot(TInteger value, TInteger degree, out TInteger root)
      {
        root = IntegerRootN(value, degree);

        return Pow(root, degree, value) == value;
      }

      public static bool IsIntegerRootN<TNth>(TInteger value, TNth n, TInteger root)
        where TNth : System.Numerics.IBinaryInteger<TNth>
        => value >= Pow(root, n) // If GTE to nth of root.
        && value < Pow(root + TInteger.One, n); // And if LT to nth of (root + 1).

      public static bool IsPerfectIntegerRootN<TNth>(TInteger value, TNth n, TInteger root)
        where TNth : System.Numerics.IBinaryInteger<TNth>
        => value == Pow(root, n);

      #endregion

      #region IntegerSqrt functions

      /// <summary>
      /// <para>Computes the integer square-root of a value.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static TInteger IntegerSqrt(TInteger value)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(value);

        if (value < NumberConstants<TInteger>.Four)
          return value == TInteger.Zero ? TInteger.Zero : TInteger.One;

        var x = TInteger.One << int.CreateChecked((TInteger.Log2(value) + TInteger.One) / NumberConstants<TInteger>.Two);

        while (true)
        {
          var y = (x + value / x) / NumberConstants<TInteger>.Two;

          if (y >= x)
            return x;

          x = y;
        }
      }

      /// <summary>
      /// <para>Indicates whether <paramref name="value"/> is the integer (not necessarily perfect) square of <paramref name="root"/>.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value">The square value to find the square-<paramref name="root"/> of.</param>
      /// <param name="root">The resulting square-root of <paramref name="value"/>.</param>
      /// <returns>Whether the <paramref name="value"/> is the integer (not necessarily perfect) square of <paramref name="root"/>.</returns>
      public static bool IsIntegerSqrt(TInteger value, TInteger root)
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
      public static bool IsPerfectIntegerSqrt(TInteger value, TInteger root)
        => value == (root * root);

      #endregion

      #region Pow functions

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
            ? TInteger.One << ie
            : throw new System.OverflowException();

        if (value == TInteger.CreateChecked(10) && exponent < TExponent.CreateChecked(UInt128Extensions.m_pow10.Length)) // 10^exponent, where exponent is the size of an existing index in the table.
          return TInteger.CreateChecked(UInt128Extensions.m_pow10[int.CreateChecked(exponent)]);

        if (typeof(TInteger) == typeof(System.Numerics.BigInteger)) // If BigInteger, use its optimized static pow() method.
          return (TInteger)(object)System.Numerics.BigInteger.Pow((System.Numerics.BigInteger)(object)value, int.CreateChecked(exponent)); // If TInteger is BigInteger, use it as such.

        // Otherwise, do binary exponentiation (exponentiation by squaring), which is really fast for the non-BigInteger types.

        var result = TInteger.One;

        while (exponent > TExponent.Zero)
          checked // Detect overflow.
          {
            if (TExponent.IsOddInteger(exponent)) // If the current exponent is odd, multiply the result by the current value.
              result *= value;

            exponent >>= 1;

            if (exponent > TExponent.Zero) // If the exponent is still greater than zero, square the current value for the next iteration.
              value *= value;
          }

        return result;
      }

      public static TInteger Pow10(TInteger exponent)
        => Pow(TInteger.CreateChecked(10), exponent);

      /// <summary>
      /// <para>Computes <paramref name="value"/>^<paramref name="exponent"/> using exponentiation by squaring. Stops early if the result would exceed <paramref name="powerLimit"/>.</para>
      /// </summary>
      /// <remarks>
      /// <para>For fixed-width integer types, if <paramref name="powerLimit"/> equals <see cref="TInteger.MaxValue"/>, an <see cref="OverflowException"/> is thrown.</para>
      /// <para>The identity 0^0 = 1 is preserved.</para>
      /// </remarks>
      /// <param name="value">The non-negative base.</param>
      /// <param name="exponent">The non-negative exponent.</param>
      /// <param name="powerLimit">The maximum permitted power.</param>
      /// <returns><paramref name="value"/>^<paramref name="exponent"/> if the result does not exceed <paramref name="powerLimit"/>; otherwise returns a value greater than <paramref name="powerLimit"/>.</returns>
      public static TInteger Pow(TInteger value, TInteger exponent, TInteger powerLimit)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(value);
        System.ArgumentOutOfRangeException.ThrowIfNegative(exponent);
        System.ArgumentOutOfRangeException.ThrowIfNegative(powerLimit);

        if (TInteger.IsZero(value))
          return TInteger.IsZero(exponent) ? TInteger.One : TInteger.Zero;

        var result = TInteger.One;
        var overflow = checked(powerLimit + TInteger.One);

        while (!TInteger.IsZero(exponent))
        {
          if (TInteger.IsOddInteger(exponent))
          {
            if (result > powerLimit / value)
              return overflow;

            result *= value;
          }

          exponent >>= 1;

          if (TInteger.IsZero(exponent))
            break;

          if (value > powerLimit / value)
            return overflow;

          value *= value;
        }

        return result;
      }

      /// <summary>
      /// <para>Attempts to compute value^exponent without exceeding powerLimit.</para>
      /// <para>Returns true if value^exponent was computed successfully.</para>
      /// <para>Returns false if computing the next power would exceed powerLimit.</para>
      /// </summary>
      /// <remarks>
      /// Upon return:
      /// <list type="bullet">
      /// <item>maxPower == value^maxExponent</item>
      /// <item>0 &lt;= maxExponent &lt;= exponent</item>
      /// <item>maxPower &lt;= powerLimit</item>
      /// </list>
      /// If the method returns true:
      /// <list type="bullet">
      /// <item>maxExponent == exponent</item>
      /// <item>maxPower == value^exponent</item>
      /// </list>
      /// If the method returns false:
      /// <list type="bullet">
      /// <item>maxExponent is the largest exponent such that value^maxExponent &lt;= powerLimit</item>
      /// <item>maxPower == value^maxExponent</item>
      /// </list>
      /// </remarks>   
      public static bool TryPow<TExponent>(TInteger value, TExponent exponent, TInteger powerLimit, out TInteger maxPower, out TExponent maxExponent)
        where TExponent : System.Numerics.IBinaryInteger<TExponent>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value);
        System.ArgumentOutOfRangeException.ThrowIfNegative(exponent);
        System.ArgumentOutOfRangeException.ThrowIfNegative(powerLimit);

        maxPower = TInteger.One;
        maxExponent = TExponent.Zero;

        while (maxExponent < exponent)
        {
          if (maxPower > powerLimit / value)
            return false;

          maxPower *= value;
          maxExponent++;
        }

        return true;
      }

      #endregion
    }
  }
}
