namespace Flux
{
  public static partial class FloatingPoint
  {
    public const int DefaultMaxUlps = 11;
    public const double DefaultToleranceFactor = 11.0;

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      #region EffectiveTolerance

      /// <summary>
      /// <para>Computes an effective tolerance for a floating-point value by returning the larger of a scaled machine epsilon and a scaled unit-in-the-last-place (ULP).</para>
      /// </summary>
      /// <remarks>Throws ArgumentOutOfRangeException if toleranceFactor is less than or equal to zero. Uses <c>RelativeEpsilon()</c> as a minimum precision bound and <c>Number.Ulp(scale)</c> as the local spacing measure.</remarks>
      /// <param name="scale">The floating-point value for which to compute the tolerance.</param>
      /// <param name="toleranceFactor">Factor used to scale both relative epsilon and ULP; must be >= 0.</param>
      /// <returns>The maximum of toleranceFactor * RelativeEpsilon() and toleranceFactor * Ulp(value) where toleranceFactor > 0.</returns>
      public static TFloat EffectiveTolerance(TFloat scale, TFloat toleranceFactor)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(toleranceFactor);

        var minimumPrecision = RelativeEpsilon<TFloat>();
        var localSpacing = Number.Ulp(scale);

        return toleranceFactor * TFloat.Max(minimumPrecision, localSpacing);
      }

      #endregion

      #region IsNearInteger

      /// <summary>
      /// <para>Check if a <typeparamref name="TFloat"/> value is near an integer within one unit in the last place (ULP).</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="nearestInteger"></param>
      /// <returns></returns>
      public static bool IsNearInteger(TFloat value, out TFloat nearestInteger, int maxUlps = DefaultMaxUlps)
      {
        nearestInteger = RoundToInteger(value).RoundedInteger;

        return Number.IsWithinUlps(value, nearestInteger, maxUlps);
      }

      #endregion

      #region IsNearZero

      /// <summary>
      /// <para>Returns true when the absolute value of value is less than or equal to toleranceFactor multiplied by FloatingPoint.RelativeEpsilon<TFloat>().</para>
      /// </summary>
      /// <remarks>Uses a tolerance based on the floating-point type's relative epsilon. For scale-dependent comparisons or domain-specific thresholds, use an appropriate absolute or scaled tolerance.</remarks>
      /// <param name="value">Value to test for nearness to zero.</param>
      /// <param name="toleranceFactor">Multiplicative factor applied to FloatingPoint.RelativeEpsilon<TFloat>() to form the tolerance; expected to be non-negative.</param>
      /// <returns>True if the absolute value of value is less than or equal to toleranceFactor multiplied by FloatingPoint.RelativeEpsilon<TFloat>(); otherwise, false.</returns>
      public static bool IsNearZero(TFloat value, TFloat toleranceFactor)
        => TFloat.Abs(value) <= toleranceFactor * RelativeEpsilon<TFloat>();

      /// <summary>
      /// <para>Returns true when the absolute value of value is less than or equal to DefaultToleranceFactor multiplied by FloatingPoint.RelativeEpsilon<TFloat>().</para>
      /// </summary>
      /// <remarks>Uses a tolerance based on the floating-point type's relative epsilon. For scale-dependent comparisons or domain-specific thresholds, use an appropriate absolute or scaled tolerance.</remarks>
      /// <param name="value">Value to test for nearness to zero.</param>
      /// <returns>True if the absolute value of value is less than or equal to DefaultToleranceFactor multiplied by FloatingPoint.RelativeEpsilon<TFloat>(); otherwise, false.</returns>
      public static bool IsNearZero(TFloat value) => IsNearZero(value, TFloat.CreateChecked(DefaultToleranceFactor));

      #endregion

      #region IsWithinPrecision

      /// <summary>
      /// <para>Determines whether value is within a scale-dependent tolerance of target.</para>
      /// </summary>
      /// <remarks>Tolerance is computed as EffectiveTolerance(Max(|value|, |target|), toleranceFactor) and applied to the absolute difference between the values.</remarks>
      /// <param name="value">Value to compare with the target.</param>
      /// <param name="target">Target value for comparison.</param>
      /// <param name="toleranceFactor">Relative factor used to compute an effective tolerance based on the larger magnitude of value and target.</param>
      /// <returns>True if the absolute difference between value and target is less than or equal to the computed tolerance; otherwise, false.</returns>
      public static bool IsWithinPrecision(TFloat value, TFloat target, TFloat toleranceFactor)
      {
        var scale = TFloat.Max(TFloat.Abs(value), TFloat.Abs(target));

        var tolerance = EffectiveTolerance(scale, toleranceFactor);

        return TFloat.Abs(value - target) <= tolerance;
      }

      /// <summary>
      /// <para>Determines whether value is within a scale-dependent tolerance of target.</para>
      /// </summary>
      /// <remarks>Tolerance is computed as EffectiveTolerance(Max(|value|, |target|), DefaultToleranceFactor) and applied to the absolute difference between the values.</remarks>
      /// <param name="value">Value to compare with the target.</param>
      /// <param name="target">Target value for comparison.</param>
      /// <returns>True if the absolute difference between value and target is less than or equal to the computed tolerance; otherwise, false.</returns>
      public static bool IsWithinPrecision(TFloat value, TFloat target) => IsWithinPrecision(value, target, TFloat.CreateChecked(DefaultToleranceFactor));

      #endregion

      #region RelativeEpsilon

      public static TFloat RelativeEpsilon()
        => (typeof(TFloat) == typeof(decimal))
        ? TFloat.CreateChecked(1e-28m)
        : (typeof(TFloat) == typeof(double) || (typeof(TFloat) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 8))
        ? TFloat.CreateChecked(2.220446049250313e-16)
        : (typeof(TFloat) == typeof(float) || (typeof(TFloat) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 4))
        ? TFloat.CreateChecked(1.1920929e-7f)
        : (typeof(TFloat) == typeof(System.Half))
        ? TFloat.CreateChecked((System.Half)0.0009765625)
        : throw new System.NotImplementedException(typeof(TFloat).Name);

      #endregion

      #region SnapNearInteger/Try

      /// <summary>
      /// <para>Snap a <typeparamref name="TFloat"/> value to the nearest integer if it is within one ULP of that integer.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static TFloat SnapNearInteger(TFloat value, int maxUlps = DefaultMaxUlps)
        => IsNearInteger(value, out var nearestInteger, maxUlps) ? nearestInteger : value;

      public static bool TrySnapNearInteger(TFloat value, out TFloat nearestInteger, int maxUlps = DefaultMaxUlps)
      {
        var isNearInteger = IsNearInteger(value, out nearestInteger, maxUlps);

        if (!isNearInteger)
          nearestInteger = value;

        return isNearInteger;
      }

      #endregion

      #region SnapNearZero/Try

      public static TFloat SnapNearZero(TFloat value, TFloat toleranceFactor)
        => IsNearZero(value, toleranceFactor) ? TFloat.Zero : value;

      public static TFloat SnapNearZero(TFloat value) => SnapNearZero(value, TFloat.CreateChecked(DefaultToleranceFactor));

      public static bool TrySnapNearZero(TFloat value, out TFloat result, TFloat toleranceFactor)
      {
        var isNearZero = IsNearZero(value, toleranceFactor);

        result = isNearZero ? TFloat.Zero : value;

        return isNearZero;
      }

      public static bool TrySnapNearZero(TFloat value, out TFloat result) => TrySnapNearZero(value, out result, TFloat.CreateChecked(DefaultToleranceFactor));

      #endregion
    }
  }

  public static partial class Number
  {
    public const int DefaultMaxUlps = 11;

    extension<TNumber>(TNumber)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      #region IsNearMultiple

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="baseValue"></param>
      /// <param name="nearestMultiple"></param>
      /// <returns></returns>
      public static bool IsNearMultiple(TNumber value, TNumber baseValue, out TNumber nearestMultiple, int maxUlps = DefaultMaxUlps)
      {
        nearestMultiple = IntegerDivRemNearest(value, baseValue).Quotient * baseValue;

        return IsWithinUlps(value, nearestMultiple, maxUlps);
      }

      #endregion

      #region IsWithinSignificantDigits

      /// <summary>
      /// <para>Perform an equality test involving the most (integer part) or the least (fraction part) <typeparamref name="TSignificantDigits"/> using the specified <paramref name="radix"/>.</para>
      /// <para>Negative means most <paramref name="significantDigits"/> tolerance on the fraction part.</para>
      /// <para>Positive means least <paramref name="significantDigits"/> tolerance on the integer part.</para>
      /// <para><see href="https://stackoverflow.com/questions/9180385/is-this-value-valid-float-comparison-that-accounts-for-value-set-number-of-decimal-place"/></para>
      /// </summary>
      /// <typeparam name="TSignificantDigits"></typeparam>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="value">The value of interest.</param>
      /// <param name="other">The value to compare to.</param>
      /// <param name="significantDigits">The number of significant digits considered for equality. A negative value for most significant digits on the right side (fraction part). A positive value for least significant digits on the left side (integer part).</param>
      /// <param name="radix">&gt; 2</param>
      /// <remarks>
      /// <para><see langword="true"/> = EqualsWithin(1000.02, 1000.015, -2, 10); // The difference of abs(<paramref name="value"/> - <paramref name="other"/>) is less than or equal to <paramref name="significantDigits"/> in <paramref name="radix"/>, i.e. 0.01 for radix 10.</para>
      /// <para><see langword="true"/> = EqualsWithin(1334.261, 1235.272, 2, 10); // The difference of abs(<paramref name="value"/> - <paramref name="other"/>) is less than or equal to negative <paramref name="significantDigits"/> in <paramref name="radix"/>, i.e. 100 for radix 10.</para>
      /// </remarks>
      /// <returns></returns>
      public static bool IsWithinSignificantDigits<TSignificantDigits, TRadix>(TNumber value, TNumber other, TSignificantDigits significantDigits, TRadix radix, DirectedRoundingMode mode)
        where TSignificantDigits : System.Numerics.IBinaryInteger<TSignificantDigits>
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        var scale = double.Pow(double.CreateChecked(radix), double.CreateChecked(significantDigits));

        var a = FloatingPoint.RoundToInteger(double.CreateChecked(value) / scale, mode).RoundedInteger;
        var b = FloatingPoint.RoundToInteger(double.CreateChecked(other) / scale, mode).RoundedInteger;

        return a == b;
      }

      /// <summary>
      /// <para>Determines whether two numbers are equal within a specified number of significant digits using the given radix and rounding rule.</para>
      /// </summary>
      /// <typeparam name="TSignificantDigits">The type representing the number of significant digits.</typeparam>
      /// <typeparam name="TRadix">The type representing the numeric base (radix).</typeparam>
      /// <param name="value">The first number to compare.</param>
      /// <param name="other">The second number to compare.</param>
      /// <param name="significantDigits">The number of significant digits to consider.</param>
      /// <param name="radix">The numeric base used for scaling.</param>
      /// <param name="rule">The rounding rule to apply when comparing values.</param>
      /// <returns>true if the numbers are equal within the specified significant digits; otherwise, false.</returns>
      public static bool IsWithinSignificantDigits<TSignificantDigits, TRadix>(TNumber value, TNumber other, TSignificantDigits significantDigits, TRadix radix, NearestRoundingRule rule)
        where TSignificantDigits : System.Numerics.IBinaryInteger<TSignificantDigits>
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        var scale = double.Pow(double.CreateChecked(radix), double.CreateChecked(significantDigits));

        var a = FloatingPoint.RoundToInteger(double.CreateChecked(value) / scale, rule).RoundedInteger;
        var b = FloatingPoint.RoundToInteger(double.CreateChecked(other) / scale, rule).RoundedInteger;

        return a == b;
      }

      #endregion

      #region IsWithinTolerance

      /// <summary>
      /// <para>Compares two values using a hybrid absolute and relative tolerance.</para>
      /// <para>The comparison uses: tolerance * (1 + max(|value|, |other|))</para>
      /// <para>This provides a minimum absolute tolerance near zero while automatically scaling for larger magnitudes.</para>
      /// </summary>
      /// <typeparam name="TTolerance"></typeparam>
      /// <param name="value">The value of interest.</param>
      /// <param name="other">The value to compare to.</param>
      /// <param name="tolerance">E.g. 1e-10.</param>
      /// <returns></returns>
      public static bool IsWithinTolerance<TTolerance>(TNumber value, TNumber other, TTolerance tolerance)
        where TTolerance : System.Numerics.IFloatingPoint<TTolerance>
      {
        if (value == other)
          return true;

        if (TNumber.IsNaN(value) || TNumber.IsNaN(other))
          return false;

        if (TNumber.IsInfinity(value) || TNumber.IsInfinity(other))
          return value == other;

        var difference = TTolerance.CreateChecked(TNumber.Abs(value - other));
        var comparison = tolerance * (TTolerance.One + TTolerance.CreateChecked(TNumber.Abs(TNumber.MaxMagnitude(value, other))));

        return difference <= comparison;
      }

      #endregion

      #region IsWithinUlps

      /// <summary>
      /// <para>Determines whether two numeric values differ by no more than a specified number of ULPs (units in the last place).</para>
      /// </summary>
      /// <remarks>
      /// <i>ULP is the spacing between adjacent representable values at the specified value:</i>
      /// <list type=">">
      /// <item>For integral types, the ULP is always 1.</item>
      /// <item>For <see cref="decimal"/>, the ULP depends on the value's scale.</item>
      /// <item>For IEEE 754 binary floating-point types (e.g. <see langword="double"/>) the ULP corresponds to the traditional "Unit in the Last Place" definition.</item>
      /// </list>
      /// </remarks>
      /// <param name="left">The first value to compare.</param>
      /// <param name="right">The second value to compare.</param>
      /// <param name="maxUlps">The maximum allowed difference measured in units in the last place (ULPs).</param>
      /// <returns>true if the ULP distance between left and right is less than or equal to maxUlps; otherwise false.</returns>
      public static bool IsWithinUlps(TNumber left, TNumber right, int maxUlps = 1)
        => UlpDistance(left, right) <= maxUlps;

      #endregion

      #region SnapNearMultiple/Try

      public static TNumber SnapNearMultiple(TNumber value, TNumber baseValue, int maxUlps = DefaultMaxUlps)
        => IsNearMultiple(value, baseValue, out var nearestMultiple, maxUlps) ? nearestMultiple : value;

      public static bool TrySnapNearMultiple(TNumber value, TNumber baseValue, out TNumber nearestMultiple, int maxUlps = DefaultMaxUlps)
      {
        var isNearMultiple = IsNearMultiple(value, baseValue, out nearestMultiple, maxUlps);

        if (!isNearMultiple)
          nearestMultiple = value;

        return isNearMultiple;
      }

      #endregion

      #region Ulp/TryGet

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <remarks>
      /// <i>ULP is the spacing between adjacent representable values at the specified value:</i>
      /// <list type=">">
      /// <item>For integral types, the ULP is always 1.</item>
      /// <item>For <see cref="decimal"/>, the ULP depends on the value's scale.</item>
      /// <item>For IEEE 754 binary floating-point types (e.g. <see langword="double"/>) the ULP corresponds to the traditional "Unit in the Last Place" definition.</item>
      /// </list>
      /// </remarks>
      /// <param name="value"></param>
      /// <returns></returns>
      /// <exception cref="System.ArithmeticException"></exception>
      /// <exception cref="System.NotImplementedException"></exception>
      public static TNumber Ulp(TNumber value)
      {
        if (TNumber.IsNaN(value))
          throw new System.ArithmeticException("NaN has no meaningful ULP.");

        if (TNumber.IsInfinity(value))
          throw new System.ArithmeticException("Infinity has no meaningful ULP.");

        if (typeof(TNumber) == typeof(decimal))
        {
          var tv = decimal.CreateChecked(value);
          var bits = decimal.GetBits(tv);
          var scale = (uint)((bits[3] >> 16) & 0xFF);
          var ulp = DecimalExtensions.Pow10Reciprocal[scale];
          return TNumber.CreateChecked(ulp);
        }
        else if ((typeof(TNumber) == typeof(double) || (typeof(TNumber) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 8)))
        {
          var tv = double.CreateChecked(value);
          var ulp = tv == double.MaxValue ? tv - double.BitDecrement(tv) : double.BitIncrement(tv) - tv;
          return TNumber.CreateChecked(ulp);
        }
        else if ((typeof(TNumber) == typeof(float) || (typeof(TNumber) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 4)))
        {
          var tv = float.CreateChecked(value);
          var ulp = tv == float.MaxValue ? tv - float.BitDecrement(tv) : float.BitIncrement(tv) - tv;
          return TNumber.CreateChecked(ulp);
        }
        else if (typeof(TNumber) == typeof(System.Half))
        {
          var tv = System.Half.CreateChecked(value);
          var ulp = tv == System.Half.MaxValue ? tv - System.Half.BitDecrement(tv) : System.Half.BitIncrement(tv) - tv;
          return TNumber.CreateChecked(ulp);
        }
#if NET11_0_OR_GREATER
        else if (typeof(TNumber) == typeof(System.Numerics.BFloat16))
        {
          var tv = System.Numerics.BFloat16.CreateChecked(value);
          var ulp = tv == System.Numerics.BFloat16.MaxValue ? tv - System.Numerics.BFloat16.BitDecrement(tv) : System.Numerics.BFloat16.BitIncrement(tv) - tv;
          return TNumber.CreateChecked(ulp);
        }
        else if (typeof(TNumber) == typeof(System.Numerics.Decimal32))
        {
          var tv = System.Numerics.Decimal32.CreateChecked(value);
          var ulp = tv == System.Numerics.Decimal32.MaxValue ? tv - System.Numerics.Decimal32.BitDecrement(tv) : System.Numerics.Decimal32.BitIncrement(tv) - tv;
          return TNumber.CreateChecked(ulp);
        }
        else if (typeof(TNumber) == typeof(System.Numerics.Decimal64))
        {
          var tv = System.Numerics.Decimal64.CreateChecked(value);
          var ulp = tv == System.Numerics.Decimal64.MaxValue ? tv - System.Numerics.Decimal64.BitDecrement(tv) : System.Numerics.Decimal64.BitIncrement(tv) - tv;
          return TNumber.CreateChecked(ulp);
        }
        else if (typeof(TNumber) == typeof(System.Numerics.Decimal128))
        {
          var tv = System.Numerics.Decimal128.CreateChecked(value);
          var ulp = tv == System.Numerics.Decimal128.MaxValue ? tv - System.Numerics.Decimal128.BitDecrement(tv) : System.Numerics.Decimal128.BitIncrement(tv) - tv;
          return TNumber.CreateChecked(ulp);
        }
#endif
        else if (NumericTypeInfo<TNumber>.ImplementsIBinaryInteger)
          return TNumber.One;
        else
          throw new System.NotImplementedException(typeof(TNumber).Name);
      }

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <remarks>
      /// <i>ULP is the spacing between adjacent representable values at the specified value:</i>
      /// <list type=">">
      /// <item>For integral types, the ULP is always 1.</item>
      /// <item>For <see cref="decimal"/>, the ULP depends on the value's scale.</item>
      /// <item>For IEEE 754 binary floating-point types (e.g. <see langword="double"/>) the ULP corresponds to the traditional "Unit in the Last Place" definition.</item>
      /// </list>
      /// </remarks>
      /// <param name="value"></param>
      /// <param name="ulp"></param>
      /// <returns></returns>
      public static bool TryGetUlp(TNumber value, out TNumber ulp)
      {
        ulp = Ulp(value);

        return TNumber.IsFinite(ulp);
      }

      #endregion

      #region UlpDecrement

      /// <summary>
      /// <para>Increments a value by <paramref name="count"/> ULPs.</para>
      /// </summary>
      /// <remarks>
      /// <i>ULP is the spacing between adjacent representable values at the specified value:</i>
      /// <list type=">">
      /// <item>For integral types, the ULP is always 1.</item>
      /// <item>For <see cref="decimal"/>, the ULP depends on the value's scale.</item>
      /// <item>For IEEE 754 binary floating-point types (e.g. <see langword="double"/>) the ULP corresponds to the traditional "Unit in the Last Place" definition.</item>
      /// </list>
      /// </remarks>
      /// <param name="value"></param>
      /// <param name="count"></param>
      /// <returns></returns>
      /// <exception cref="System.NotImplementedException"></exception>
      public static TNumber UlpDecrement(TNumber value, long count)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);

        checked
        {
          if (typeof(TNumber) == typeof(decimal)) // Decrement by ulps * ULP(value).
            return checked(value - Ulp(value) * TNumber.CreateChecked(count));
          else if (typeof(TNumber) == typeof(double) || (typeof(TNumber) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 8))
          {
            var ordered = double.ToOrdered(double.CreateChecked(value));
            ordered -= (ulong)count;
            return TNumber.CreateChecked(double.FromOrdered(ordered));
          }
          else if (typeof(TNumber) == typeof(float) || (typeof(TNumber) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 4))
          {
            var ordered = float.ToOrdered(float.CreateChecked(value));
            ordered -= (ulong)count;
            return TNumber.CreateChecked(float.FromOrdered(ordered));
          }
          else if (typeof(TNumber) == typeof(System.Half))
          {
            var ordered = System.Half.ToOrdered(System.Half.CreateChecked(value));
            ordered -= (ulong)count;
            return TNumber.CreateChecked(System.Half.FromOrdered(ordered));
          }
          else if (NumericTypeInfo<TNumber>.ImplementsIBinaryInteger) // Is integer, decrement by ulps.
            return value - TNumber.CreateChecked(count);
          else // Unrecognized type, throw an exception.
            throw new System.NotImplementedException(typeof(TNumber).Name);
        }
      }

      /// <summary>
      /// <para>Increments a value by one ULP.</para>
      /// </summary>
      /// <remarks>
      /// <i>ULP is the spacing between adjacent representable values at the specified value:</i>
      /// <list type=">">
      /// <item>For integral types, the ULP is always 1.</item>
      /// <item>For <see cref="decimal"/>, the ULP depends on the value's scale.</item>
      /// <item>For IEEE 754 binary floating-point types (e.g. <see langword="double"/>) the ULP corresponds to the traditional "Unit in the Last Place" definition.</item>
      /// </list>
      /// </remarks>
      /// <param name="value"></param>
      /// <returns></returns>
      /// <exception cref="System.NotImplementedException"></exception>
      public static TNumber UlpDecrement(TNumber value) => UlpDecrement(value, 1);

      #endregion

      #region UlpDistance

      /// <summary>
      /// <para>Returns the distance in adjacent representable values.</para>
      /// <list type="bullet">
      /// <item>For integer types, the result is the absolute difference between the values.</item>
      /// <item>For decimal, the result is the distance between normalized decimal significands.</item>
      /// <item>For IEEE floating-point types, the result is measured in ULPs (Units in the Last Place).</item>
      /// </list>
      /// </summary>
      /// <remarks>
      /// <i>ULP is the spacing between adjacent representable values at the specified value:</i>
      /// <list type="bullet">
      /// <item>For integral types, the ULP is always 1.</item>
      /// <item>For <see cref="decimal"/>, the ULP depends on the value's scale.</item>
      /// <item>For IEEE 754 binary floating-point types (e.g. <see langword="double"/>) the ULP corresponds to the traditional "Unit in the Last Place" definition.</item>
      /// </list>
      /// </remarks>
      /// <param name="left"></param>
      /// <param name="right"></param>
      /// <returns></returns>
      /// <exception cref="System.ArithmeticException"></exception>
      /// <exception cref="System.NotImplementedException"></exception>
      public static System.Numerics.BigInteger UlpDistance(TNumber left, TNumber right)
      {
        if (TNumber.IsNaN(left) || TNumber.IsNaN(right))
          throw new System.ArithmeticException("NaN has no meaningful ULP distance.");

        if (left == right)
          return System.Numerics.BigInteger.Zero;

        System.Numerics.BigInteger leftOrdered, rightOrdered;

        if (typeof(TNumber) == typeof(decimal))
        {
          var (_, leftScale, leftSignificand) = decimal.ExtractDecimalFields(decimal.CreateChecked(left));
          var (_, rightScale, rightSignificand) = decimal.ExtractDecimalFields(decimal.CreateChecked(right));
          var scale = int.Max(leftScale, rightScale);
          leftOrdered = leftSignificand * (System.Numerics.BigInteger)decimal.Pow10(scale - leftScale);
          rightOrdered = rightSignificand * (System.Numerics.BigInteger)decimal.Pow10(scale - rightScale);
        }
        else if (typeof(TNumber) == typeof(double) || (typeof(TNumber) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 8))
        {
          leftOrdered = System.BitConverter.DoubleToInt64Bits(double.CreateChecked(left));
          rightOrdered = System.BitConverter.DoubleToInt64Bits(double.CreateChecked(right));
          if (leftOrdered < 0) leftOrdered = (System.Numerics.BigInteger)long.MinValue - leftOrdered;
          if (rightOrdered < 0) rightOrdered = (System.Numerics.BigInteger)long.MinValue - rightOrdered;
        }
        else if (typeof(TNumber) == typeof(float) || (typeof(TNumber) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 4))
        {
          leftOrdered = System.BitConverter.SingleToInt32Bits(float.CreateChecked(left));
          rightOrdered = System.BitConverter.SingleToInt32Bits(float.CreateChecked(right));
          if (leftOrdered < 0) leftOrdered = (System.Numerics.BigInteger)int.MinValue - leftOrdered;
          if (rightOrdered < 0) rightOrdered = (System.Numerics.BigInteger)int.MinValue - rightOrdered;
        }
        else if (typeof(TNumber) == typeof(System.Half))
        {
          leftOrdered = System.BitConverter.HalfToInt16Bits(System.Half.CreateChecked(left));
          rightOrdered = System.BitConverter.HalfToInt16Bits(System.Half.CreateChecked(right));
          if (leftOrdered < 0) leftOrdered = (System.Numerics.BigInteger)short.MinValue - leftOrdered;
          if (rightOrdered < 0) rightOrdered = (System.Numerics.BigInteger)short.MinValue - rightOrdered;
        }
        else if (NumericTypeInfo<TNumber>.ImplementsIBinaryInteger)
        {
          leftOrdered = System.Numerics.BigInteger.CreateChecked(left);
          rightOrdered = System.Numerics.BigInteger.CreateChecked(right);
        }
        else throw new System.NotImplementedException(typeof(TNumber).Name);

        return System.Numerics.BigInteger.Abs(leftOrdered - rightOrdered);
      }

      #endregion

      #region UlpIncrement

      /// <summary>
      /// <para>Increments a value by <paramref name="count"/> ULPs.</para>
      /// </summary>
      /// <remarks>
      /// <i>ULP is the spacing between adjacent representable values at the specified value:</i>
      /// <list type=">">
      /// <item>For integral types, the ULP is always 1.</item>
      /// <item>For <see cref="decimal"/>, the ULP depends on the value's scale.</item>
      /// <item>For IEEE 754 binary floating-point types (e.g. <see langword="double"/>) the ULP corresponds to the traditional "Unit in the Last Place" definition.</item>
      /// </list>
      /// </remarks>
      /// <param name="value"></param>
      /// <param name="count"></param>
      /// <returns></returns>
      /// <exception cref="System.NotImplementedException"></exception>
      public static TNumber UlpIncrement(TNumber value, long count)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);

        checked
        {
          if (typeof(TNumber) == typeof(decimal)) // Increment by ulps * ULP(value).
            return value + Ulp(value) * TNumber.CreateChecked(count);
          else if (typeof(TNumber) == typeof(double) || (typeof(TNumber) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 8))
          {
            var ordered = double.ToOrdered(double.CreateChecked(value));
            ordered += (ulong)count;
            return TNumber.CreateChecked(double.FromOrdered(ordered));
          }
          else if (typeof(TNumber) == typeof(float) || (typeof(TNumber) == typeof(System.Runtime.InteropServices.NFloat) && System.Runtime.InteropServices.NFloat.Size == 4))
          {
            var ordered = float.ToOrdered(float.CreateChecked(value));
            ordered += (ulong)count;
            return TNumber.CreateChecked(float.FromOrdered(ordered));
          }
          else if (typeof(TNumber) == typeof(System.Half))
          {
            var ordered = System.Half.ToOrdered(System.Half.CreateChecked(value));
            ordered += (ulong)count;
            return TNumber.CreateChecked(System.Half.FromOrdered(ordered));
          }
          else if (NumericTypeInfo<TNumber>.ImplementsIBinaryInteger) // Is integer, increment by ulps.
            return value + TNumber.CreateChecked(count);
          else // Unrecognized type, throw an exception.
            throw new System.NotImplementedException(typeof(TNumber).Name);
        }
      }

      /// <summary>
      /// <para>Increments a value by one ULP.</para>
      /// </summary>
      /// <remarks>
      /// <i>ULP is the spacing between adjacent representable values at the specified value:</i>
      /// <list type=">">
      /// <item>For integral types, the ULP is always 1.</item>
      /// <item>For <see cref="decimal"/>, the ULP depends on the value's scale.</item>
      /// <item>For IEEE 754 binary floating-point types (e.g. <see langword="double"/>) the ULP corresponds to the traditional "Unit in the Last Place" definition.</item>
      /// </list>
      /// </remarks>
      /// <param name="value"></param>
      /// <returns></returns>
      /// <exception cref="System.NotImplementedException"></exception>
      public static TNumber UlpIncrement(TNumber value) => UlpIncrement(value, 1);

      #endregion
    }
  }
}
