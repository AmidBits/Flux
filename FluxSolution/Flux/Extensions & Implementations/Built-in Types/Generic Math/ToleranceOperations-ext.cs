namespace Flux
{
  public static partial class FloatingPoint
  {
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
    extension<TNumber>(TNumber)
      where TNumber : System.Numerics.INumber<TNumber>
    {
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

        var a = FloatingPoint.RoundToInteger(double.CreateChecked(value) / scale, mode);
        var b = FloatingPoint.RoundToInteger(double.CreateChecked(other) / scale, mode);

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

        var a = FloatingPoint.RoundToNearestInteger(double.CreateChecked(value) / scale, rule);
        var b = FloatingPoint.RoundToNearestInteger(double.CreateChecked(other) / scale, rule);

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
    }
  }
}
