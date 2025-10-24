namespace Flux
{
  public static partial class NumberFunctions
  {
    extension<TNumber>(TNumber value)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      #region Detent

      /// <summary>
      /// <para>Snaps the <paramref name="value"/> to the nearest <paramref name="interval"/> if it's within the specified <paramref name="proximity"/> of an <paramref name="interval"/> position, otherwise unaltered.</para>
      /// </summary>
      /// <remarks>This is similar to a knob that has notches which latches the knob at certain positions.</remarks>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="value"></param>
      /// <param name="interval">The number will snap to any multiple of the specified <paramref name="interval"/>.</param>
      /// <param name="proximity">This is the absolute tolerance of proximity, on either side of an <paramref name="interval"/>.</param>
      /// <returns></returns>
      public TNumber DetentInterval(TNumber interval, TNumber proximity)
        => TNumber.CreateChecked(int.CreateChecked(value / interval)) * interval is var tzInterval && TNumber.Abs(tzInterval - value) <= proximity
        ? tzInterval
        : tzInterval + interval is var afzInterval && TNumber.Abs(afzInterval - value) <= proximity
        ? afzInterval
        : value;

      /// <summary>
      /// <para>Snaps a <paramref name="value"/> to a <paramref name="position"/> if it's within the specified <paramref name="proximity"/> of the <paramref name="position"/>, otherwise unaltered.</para>
      /// </summary>
      /// <remarks>This is similar to a knob that has a notch which latches the knob at a certain position.</remarks>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="value"></param>
      /// <param name="position">E.g. a 0 snaps the <paramref name="value"/> to zero within the <paramref name="proximity"/>.</param>
      /// <param name="proximity">This is the absolute tolerance of proximity, on either side of the <paramref name="position"/>.</param>
      /// <returns></returns>
      public TNumber DetentPosition(TNumber position, TNumber proximity)
        => position.EqualsWithinAbsoluteTolerance(value, proximity)
        ? position // Detent to the position.
        : value;

      #endregion

      //#region EqualsWithin

      ///// <summary>
      ///// <para>Perform an absolute equality test.</para>
      ///// <para>Absolute equality checks if the absolute difference between <paramref name="value"/> and <paramref name="other"/> is smaller than a predefined <paramref name="absoluteTolerance"/>. This is useful when you want to ensure the numbers are "close enough" without considering their scale.</para>
      ///// <para>Absolute equality is simpler and works well for small numbers or fixed tolerances.</para>
      ///// </summary>
      ///// <typeparam name="TNumber"></typeparam>
      ///// <param name="value"></param>
      ///// <param name="other"></param>
      ///// <param name="absoluteTolerance">E.g. 1e-10.</param>
      ///// <returns></returns>
      //public bool EqualsWithinAbsoluteTolerance(TNumber other, TNumber absoluteTolerance)
      //  => value == other
      //  || TNumber.Abs(value - other) <= absoluteTolerance;

      ///// <summary>
      ///// <para>Perform a relative equality test.</para>
      ///// <para>Relative equality considers the scale of the numbers by dividing the absolute difference by the magnitude of the numbers. This is useful when comparing numbers that may vary significantly in scale.</para>
      ///// <para>Relative equality is better for large numbers or numbers with varying scales, as it adjusts the tolerance dynamically.</para>
      ///// </summary>
      ///// <typeparam name="TNumber"></typeparam>
      ///// <typeparam name="TRelativeTolerance"></typeparam>
      ///// <param name="value"></param>
      ///// <param name="other"></param>
      ///// <param name="relativeTolerance">E.g. 1e-10.</param>
      ///// <returns></returns>
      //public bool EqualsWithinRelativeTolerance<TRelativeTolerance>(TNumber other, TRelativeTolerance relativeTolerance)
      //  where TRelativeTolerance : System.Numerics.IFloatingPoint<TRelativeTolerance>
      //  => value == other
      //  || TRelativeTolerance.CreateChecked(TNumber.Abs(value - other)) <= relativeTolerance * TRelativeTolerance.CreateChecked(TNumber.Abs(TNumber.MaxMagnitude(value, other)));

      ///// <summary>
      ///// <para>Perform both an absolute and a relative equality test for more robust comparisons. Returns true if any test is considered equal, otherwise false.</para>
      ///// </summary>
      ///// <typeparam name="TNumber"></typeparam>
      ///// <typeparam name="TRelativeTolerance"></typeparam>
      ///// <param name="value"></param>
      ///// <param name="other"></param>
      ///// <param name="absoluteTolerance">E.g. 1e-10.</param>
      ///// <param name="relativeTolerance">E.g. 1e-10.</param>
      ///// <returns></returns>
      //public bool EqualsWithinTolerance<TRelativeTolerance>(TNumber other, TNumber absoluteTolerance, TRelativeTolerance relativeTolerance)
      //  where TRelativeTolerance : System.Numerics.IFloatingPoint<TRelativeTolerance>
      //  => value.EqualsWithinAbsoluteTolerance(other, absoluteTolerance) || value.EqualsWithinRelativeTolerance(other, relativeTolerance);

      ///// <summary>
      ///// <para>Perform an equality test involving the most (integer part) or the least (fraction part) <typeparamref name="TSignificantDigits"/> using the specified <paramref name="radix"/>.</para>
      ///// <para>Positive means most <paramref name="significantDigits"/> tolerance on the fraction part.</para>
      ///// <para>Negative means least <paramref name="significantDigits"/> tolerance on the integer part.</para>
      ///// <para><see href="https://stackoverflow.com/questions/9180385/is-this-value-valid-float-comparison-that-accounts-for-value-set-number-of-decimal-place"/></para>
      ///// </summary>
      ///// <typeparam name="TNumber"></typeparam>
      ///// <typeparam name="TSignificantDigits"></typeparam>
      ///// <typeparam name="TRadix"></typeparam>
      ///// <param name="value"></param>
      ///// <param name="other"></param>
      ///// <param name="significantDigits">The tolerance, as the number of significant digits, considered for equality. A positive value for most significant digits on the right side (fraction part). A negative value for least significant digits on the left side (integer part).</param>
      ///// <param name="radix"></param>
      ///// <remarks>
      ///// <para>EqualsWithinSignificantDigits(1000.02, 1000.015, 2, 10); // The difference of abs(<paramref name="value"/> - <paramref name="other"/>) is less than or equal to <paramref name="significantDigits"/> in <paramref name="radix"/>, i.e. 0.01 for radix 10.</para>
      ///// <para>EqualsWithinSignificantDigits(1334.261, 1235.272, -2, 10); // The difference of abs(<paramref name="value"/> - <paramref name="other"/>) is less than or equal to negative <paramref name="significantDigits"/> in <paramref name="radix"/>, i.e. 100 for radix 10.</para>
      ///// </remarks>
      ///// <returns></returns>
      //public bool EqualsWithinSignificantDigits<TSignificantDigits, TRadix>(TNumber other, TSignificantDigits significantDigits, TRadix radix)
      //  where TSignificantDigits : System.Numerics.IBinaryInteger<TSignificantDigits>
      //  where TRadix : System.Numerics.IBinaryInteger<TRadix>
      //  => value == other
      //  || (double.CreateChecked(TNumber.Abs(value - other)) <= double.Pow(double.CreateChecked(Units.Radix.AssertMember(radix)), -double.CreateChecked(significantDigits)));

      //#endregion

      #region FoldAcross

      /// <summary>
      /// <para>Folds an out-of-bound <paramref name="value"/> (back and forth) across the closed interval [<paramref name="minValue"/>, <paramref name="maxValue"/>], until the <paramref name="value"/> is within the closed interval.</para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="value"></param>
      /// <param name="minValue"></param>
      /// <param name="maxValue"></param>
      /// <returns></returns>
      public TNumber FoldAcross(TNumber minValue, TNumber maxValue)
        => (value > maxValue)
        ? (value - maxValue).TruncRem(maxValue - minValue) is var (tqHi, remHi) && TNumber.IsEvenInteger(tqHi) ? maxValue - remHi : minValue + remHi
        : (value < minValue)
        ? (minValue - value).TruncRem(maxValue - minValue) is var (tqLo, remLo) && TNumber.IsEvenInteger(tqLo) ? minValue + remLo : maxValue - remLo
        : value;

      #endregion

      #region GetInfimum & GetSupremum

      /// <summary>
      /// <para>Gets the infimum (the largest number that is less than <paramref name="value"/>).</para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="value"></param>
      /// <returns></returns>
      /// <remarks>Infimum, for integer types equal (<paramref name="value"/> - 1) and for floating point types equal (<paramref name="value"/> - epsilon). Other types are not implemented at this time.</remarks>
      public TNumber GetInfimum()
      {
        if (value.GetType().IsNumericTypeInteger(false)) // All integers are the same, and therefor subtract one.
          return checked(value - TNumber.One);
        else if (value is decimal dfp128) // All floating point types has different structures, and therefor their own bit-increment.
          return TNumber.CreateChecked(dfp128 - 1e-28m);
        else if (value is double bfp64) // All floating point types has different structures, and therefor their own bit-decrement.
          return TNumber.CreateChecked(double.BitDecrement(bfp64));
        else if (value is float bfp32) // All floating point types has different structures, and therefor their own bit-decrement.
          return TNumber.CreateChecked(float.BitDecrement(bfp32));
        else if (value is System.Half bfp16) // All floating point types has different structures, and therefor their own bit-decrement.
          return TNumber.CreateChecked(System.Half.BitDecrement(bfp16));
        else if (value is System.Runtime.InteropServices.NFloat nf) // All floating point types has different structures, and therefor their own bit-decrement.
          return TNumber.CreateChecked(System.Runtime.InteropServices.NFloat.BitDecrement(nf));
        else
          throw new System.NotImplementedException();
      }

      /// <summary>
      /// <para>Gets the supremum (the smallest number that is greater than <paramref name="value"/>).</para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="value"></param>
      /// <returns></returns>
      /// <remarks>Supremum, for integer types equal (<paramref name="value"/> + 1) and for floating point types equal (<paramref name="value"/> + epsilon). Other types are not implemented at this time.</remarks>
      public TNumber GetSupremum()
      {
        if (value.GetType().IsNumericTypeInteger(false)) // All integers are the same, and therefor add one.
          return checked(value + TNumber.One);
        else if (value is decimal dfp128) // All floating point types has different structures, and therefor their own bit-increment.
          return TNumber.CreateChecked(dfp128 + 1e-28m);
        else if (value is double bfp64) // All floating point types has different structures, and therefor their own bit-increment.
          return TNumber.CreateChecked(double.BitIncrement(bfp64));
        else if (value is float bfp32) // All floating point types has different structures, and therefor their own bit-increment.
          return TNumber.CreateChecked(float.BitIncrement(bfp32));
        else if (value is System.Half bfp16) // All floating point types has different structures, and therefor their own bit-increment.
          return TNumber.CreateChecked(System.Half.BitIncrement(bfp16));
        else if (value is System.Runtime.InteropServices.NFloat nf) // All floating point types has different structures, and therefor their own bit-increment.
          return TNumber.CreateChecked(System.Runtime.InteropServices.NFloat.BitIncrement(nf));
        else
          throw new System.NotImplementedException();
      }

      #endregion

      #region IsConsideredPlural

      /// <summary>
      /// <para>Determines whether the number is considered plural in terms of writing.</para>
      /// <para></para>
      /// </summary>
      /// <remarks>This function consider all numbers (e.g. 1.0, 2, etc.), except <c>integer</c> types equal to 1, to be plural.</remarks>
      /// <returns></returns>
      public bool IsConsideredPlural()
        => value != TNumber.One || !value.GetType().IsNumericTypeInteger(); // Only an integer 1 (not 1.0) is singular, otherwise a number is considered plural.

      #endregion

      #region MultipleOf

      /// <summary>
      /// <para>Determines whether the <paramref name="value"/> is of a <paramref name="multiple"/>.</para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="value">The number for which the nearest <paramref name="multiple"/> will be found.</param>
      /// <param name="multiple">The multiple to which <paramref name="value"/> is measured.</param>
      /// <returns></returns>
      public bool IsMultipleOf(TNumber multiple)
        => TNumber.IsZero(value % multiple);

      /// <summary>
      /// <para>Round a <paramref name="value"/> to the nearest <paramref name="multiple"/> away-from-zero and whether to ensure it is <paramref name="unequal"/>.</para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="value"></param>
      /// <param name="multiple"></param>
      /// <param name="unequal"></param>
      /// <returns></returns>
      public TNumber MultipleOfAwayFromZero(TNumber multiple, bool unequal = false)
        => TNumber.CopySign(multiple, value) is var msv && value - (value % multiple) is var motz && (motz != value || (unequal || !TNumber.IsInteger(value))) ? motz + msv : motz;

      /// <summary>
      /// <para>Rounds the <paramref name="value"/> to the closest <paramref name="multiple"/> (i.e. of <paramref name="multipleOfTowardsZero"/> and <paramref name="multipleOfAwayFromZero"/> which are computed and returned as out parameters) according to <paramref name="unequal"/> and the strategy <paramref name="mode"/>. Negative <paramref name="value"/> resilient.</para>
      /// </summary>
      /// <param name="value">The value for which the nearest multiples of will be found.</param>
      /// <param name="multiple">The multiple to which the results will align.</param>
      /// <param name="unequal">Proper means nearest but not <paramref name="value"/> if it's a multiple-of, i.e. the two multiple-of will be properly "nearest" (but not the same), or LT/GT rather than LTE/GTE.</param>
      /// <param name="multipleOfTowardsZero">Outputs the multiple of that is closer to zero.</param>
      /// <param name="multipleOfAwayFromZero">Outputs the multiple of that is farther from zero.</param>
      /// <returns>The nearest two multiples to value as out parameters.</returns>
      public TNumber MultipleOfNearest(TNumber multiple, bool unequal, HalfRounding mode, out TNumber multipleOfTowardsZero, out TNumber multipleOfAwayFromZero)
      {
        multipleOfTowardsZero = value.MultipleOfTowardZero(multiple, unequal);
        multipleOfAwayFromZero = value.MultipleOfAwayFromZero(multiple, unequal);

        return value.RoundToNearest(mode, multipleOfTowardsZero, multipleOfAwayFromZero);
      }

      /// <summary>
      /// <para>Round a <paramref name="value"/> to the nearest <paramref name="multiple"/> toward-zero and whether to ensure it is <paramref name="unequal"/>.</para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="value"></param>
      /// <param name="multiple"></param>
      /// <param name="unequal"></param>
      /// <returns></returns>
      public TNumber MultipleOfTowardZero(TNumber multiple, bool unequal = false)
        //=> MultipleOfTowardZero(value, multiple, unequal && TNumber.IsInteger(value)); // value - (value % multiple) is var motz && unequal && motz == value ? motz - TSelf.CopySign(multiple, value) : motz;
        => value - (value % multiple) is var motz && (unequal && TNumber.IsInteger(value)) && motz == value ? motz - TNumber.CopySign(multiple, value) : motz;

      #endregion

      #region Unit/Sign

      /// <summary>
      /// <para>The sign step function.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Sign_function"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Step_function"/></para>
      /// </summary>
      /// <remarks>LT zero = -1, EQ zero = 0, GT zero = +1.</remarks>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="value"></param>
      /// <returns></returns>
      public TNumber Sign()
        => TNumber.IsZero(value) ? value : UnitSign(value);

      /// <summary>
      /// <para>The unit sign step function, i.e. zero is treated as a positive unit value of one.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Step_function"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Sign_function"/></para>
      /// </summary>
      /// <remarks>LT 0 (negative) = -1, GTE 0 (not negative) = +1.</remarks>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="value"></param>
      /// <returns></returns>
      public TNumber UnitSign()
        => TNumber.CopySign(TNumber.One, value);

      #endregion

      #region Spread

      /// <summary>
      /// <para>Spreads an in-interval <paramref name="value"/> around the outside edges of the closed interval [<paramref name="lowValue"/>, <paramref name="highValue"/>].</para>
      /// <para>This is the opposite of a <see cref="System.Numerics.INumber{TSelf}.Clamp(TSelf, TSelf, TSelf)"/> ( [<paramref name="minValue"/>, <paramref name="maxValue"/>] ) which is inclusive, making the Spread( either [NegativeInfinity, <paramref name="lowValue"/>) or (<paramref name="highValue"/>, PositiveInfinity] ), i.e. exclusive of <paramref name="lowValue"/> and <paramref name="highValue"/>.</para>
      /// </summary>
      public TNumber Spread(TNumber lowValue, TNumber highValue, HalfRounding mode)
        => (value < lowValue || value > highValue)
        ? value // If number is already spread, nothing to do but return it.
        : value.RoundToNearest(mode, lowValue, highValue) is var nearestValue && (nearestValue == lowValue)
        ? lowValue.GetInfimum()
        : (nearestValue == highValue)
        ? highValue.GetSupremum()
        : nearestValue;

      #endregion
    }

    #region LogOf

    /// <summary>
    /// <para>Round a <paramref name="value"/> to the nearest log-of-<paramref name="radix"/> away-from-zero.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    public static TNumber LogOfAwayFromZero<TNumber, TRadix>(this TNumber value, TRadix radix)
      where TNumber : System.Numerics.INumber<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => value <= TNumber.Zero || radix <= TRadix.One
      ? TNumber.Zero
      : TNumber.CreateChecked(double.Log(double.CreateChecked(value), double.CreateChecked(radix)).Envelop());

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <param name="mode"></param>
    /// <param name="logTowardsZero"></param>
    /// <param name="logAwayFromZero"></param>
    /// <returns></returns>
    public static TNumber LogOfNearest<TNumber, TRadix>(this TNumber value, TRadix radix, HalfRounding mode, out TNumber logTowardsZero, out TNumber logAwayFromZero)
      where TNumber : System.Numerics.INumber<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      logAwayFromZero = value.LogOfAwayFromZero(radix);
      logTowardsZero = value.LogOfTowardZero(radix);

      return value.RoundToNearest(mode, logTowardsZero, logAwayFromZero);
    }

    /// <summary>
    /// <para>Round a <paramref name="value"/> to the nearest log-of-<paramref name="radix"/> toward-zero.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TNumber LogOfTowardZero<TNumber, TRadix>(this TNumber value, TRadix radix)
      where TNumber : System.Numerics.INumber<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => value <= TNumber.Zero || radix <= TRadix.One
      ? TNumber.Zero
      : TNumber.CreateChecked(double.Truncate(double.Log(double.CreateChecked(TNumber.Abs(value)), double.CreateChecked(radix))));

    #endregion

    #region MeanMedianAbsoluteDeviation

    /// <summary>
    /// <para>The mean absolute deviation (MAD), of a data set is the average of the absolute deviations from a central point.</para>
    /// <para>In this function, both M(AD)ean, M(AD)edian and the regular mode are computed.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Average_absolute_deviation"/></para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="values"></param>
    /// <returns></returns>
    public static (double MeanAbsoluteDeviation, double MedianAbsoluteDeviation) MeanMedianAbsoluteDeviation<TNumber>(this System.Collections.Generic.IList<TNumber> values)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      var mmo = new Statistics.OnlineMeanMedianMode<TNumber>(values);

      var madMean = 0d;
      var madMedian = 0d;

      foreach (var value in values.Select(v => double.CreateChecked(v)))
      {
        madMean += double.Abs(value - mmo.Mean);
        madMedian += double.Abs(value - mmo.Median);
      }

      madMean /= mmo.Count;
      madMedian /= mmo.Count;

      return (madMean, madMedian);
    }

    #endregion

    #region PowOf

    /// <summary>
    /// <para>Round a <paramref name="value"/> to the nearest power-of-<paramref name="radix"/> away-from-zero and whether to ensure it is <paramref name="unequal"/>.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <param name="unequal"></param>
    /// <returns></returns>
    public static TNumber PowOfAwayFromZero<TNumber, TRadix>(this TNumber value, TRadix radix, bool unequal = false)
      where TNumber : System.Numerics.INumber<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => TNumber.CreateChecked(Units.Radix.AssertMember(radix)) is var r && TNumber.IsZero(value)
      ? value
      : TNumber.CopySign(TNumber.Abs(value) is var v && r.FastIntegerPow(v.FastIntegerLog(radix, out var _).IlogTz, out var _).IpowTz is var p && (p == v ? p : p * r) is var afz && (unequal || !TNumber.IsInteger(value)) && afz == v ? afz * r : afz, value);

    /// <summary>
    /// <para>Rounds the <paramref name="value"/> to the closest power-of-<paramref name="radix"/> (i.e. of <paramref name="powOfTowardsZero"/> and <paramref name="powOfAwayFromZero"/> which are computed and returned as out parameters) according to <paramref name="unequal"/> and the strategy <paramref name="mode"/>. Negative <paramref name="value"/> resilient.</para>
    /// </summary>
    /// <param name="value">The value for which the nearest power-of-<paramref name="radix"/> of will be found.</param>
    /// <param name="radix">The radix (base) of the power-of-<paramref name="radix"/>.</param>
    /// <param name="unequal">Proper means nearest but <paramref name="unequal"/> to <paramref name="value"/> if it's a power-of-<paramref name="radix"/>, i.e. the two power-of-radix will be properly "nearest" (but not the same), or LT/GT rather than LTE/GTE.</param>
    /// <param name="powOfTowardsZero">Outputs the power-of-<paramref name="radix"/> of that is closer to zero.</param>
    /// <param name="powOfAwayFromZero">Outputs the power-of-<paramref name="radix"/> of that is farther from zero.</param>
    /// <returns>The nearest two power-of-<paramref name="radix"/> as out parameters and the the nearest of those two is returned.</returns>
    public static TNumber PowOf<TNumber, TRadix>(this TNumber value, TRadix radix, bool unequal, HalfRounding mode, out TNumber powOfTowardsZero, out TNumber powOfAwayFromZero)
      where TNumber : System.Numerics.INumber<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      powOfTowardsZero = value.PowOfTowardZero(radix, unequal);
      powOfAwayFromZero = value.PowOfAwayFromZero(radix, unequal);

      return value.RoundToNearest(mode, powOfTowardsZero, powOfAwayFromZero);
    }

    /// <summary>
    /// <para>Round a <paramref name="value"/> to the nearest power-of-<paramref name="radix"/> toward-zero and whether to ensure it is <paramref name="unequal"/>.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <param name="unequal"></param>
    /// <returns></returns>
    public static TNumber PowOfTowardZero<TNumber, TRadix>(this TNumber value, TRadix radix, bool unequal = false)
      where TNumber : System.Numerics.INumber<TNumber>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      => TNumber.CreateChecked(Units.Radix.AssertMember(radix)) is var r && TNumber.IsZero(value)
      ? value
      : TNumber.CopySign(TNumber.Abs(value) is var v && r.FastIntegerPow(v.FastIntegerLog(radix, out var _).IlogTz, out var _).IpowTz is var p && (unequal && TNumber.IsInteger(value)) && p == v ? p / r : p, value);

    #endregion

    #region Remainders

    /// <summary>
    /// <para>Remainder is the standard remainder.</para>
    /// <para>RemainderNoZero is the same as standard except no zero, and instead returns the divisor with the sign of the dividend.</para>
    /// <para>ReverseRemainder is the reverse order of the standard remainder, except for 0, which is still in same.</para>
    /// <para>ReverseRemainderNoZero is the same as ReverseRemainder except no zero, and instead returns the divisor with the sign of dividend.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="dividend"></param>
    /// <param name="divisor"></param>
    /// <returns></returns>
    public static (TNumber Remainder, TNumber RemainderNoZero, TNumber ReverseRemainder, TNumber ReverseRemainderNoZero) Remainders<TNumber>(this TNumber dividend, TNumber divisor)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      var remainder = dividend % divisor;

      var copySign = TNumber.CopySign(divisor, dividend);

      if (TNumber.IsZero(remainder))
      {
        return (remainder, copySign, remainder, copySign);
      }
      else
      {
        var minusRemainder = copySign - remainder;

        return (remainder, remainder, minusRemainder, minusRemainder);
      }
    }

    //extension<TNumber>(TNumber dividend)
    //  where TNumber : System.Numerics.INumber<TNumber>
    //{
    //  /// <summary>
    //  /// <para>Computes a non-zero remainder of (<paramref name="dividend"/> % <paramref name="divisor"/>), i.e. it returns [divisor, 1 .. (divisor - 1)], instead of the remainder [0, 1 .. (divisor - 1)].</para>
    //  /// <para>Zero is replaced by the <paramref name="divisor"/> (with the sign of <paramref name="dividend"/>) and all other remainder (%) values returned as defined by <typeparamref name="TNumber"/>.</para>
    //  /// </summary>
    //  /// <typeparam name="TNumber"></typeparam>
    //  /// <param name="dividend"></param>
    //  /// <param name="divisor"></param>
    //  /// <returns>
    //  /// <para>The reverse remainder of (<paramref name="dividend"/> % <paramref name="divisor"/>).</para>
    //  /// </returns>
    //  public TNumber RemainderNoZero(TNumber divisor, out TNumber remainder)
    //  {
    //    remainder = dividend % divisor;

    //    return TNumber.IsZero(remainder) ? TNumber.CopySign(divisor, dividend) : remainder;
    //  }

    //  /// <summary>
    //  /// <para>Computes the reverse <paramref name="remainder"/> of (<paramref name="dividend"/> % <paramref name="divisor"/>), i.e. it returns [divisor, 1], instead of the remainder [0, (divisor - 1)].</para>
    //  /// <para>Zero is replaced by the <paramref name="divisor"/> with the sign of <paramref name="dividend"/>, and all other remainder (%) values are returned in reverse order from that defined by <typeparamref name="TNumber"/>.</para>
    //  /// </summary>
    //  /// <typeparam name="TNumber"></typeparam>
    //  /// <param name="dividend"></param>
    //  /// <param name="divisor"></param>
    //  /// <param name="remainder"></param>
    //  /// <returns>
    //  /// <para>The reverse <paramref name="remainder"/> of (<paramref name="dividend"/> % <paramref name="divisor"/>).</para>
    //  /// <para>Also returns the normal <paramref name="remainder"/> as an out parameter.</para>
    //  /// </returns>
    //  public TNumber ReverseRemainderNoZero(TNumber divisor, out TNumber remainder)
    //  {
    //    var rev = ReverseRemainder(dividend, divisor, out remainder);

    //    return TNumber.IsZero(rev) ? TNumber.CopySign(divisor, dividend) : rev;
    //  }

    //  /// <summary>
    //  /// <para>Computes the reverse <paramref name="remainder"/> of (<paramref name="dividend"/> % <paramref name="divisor"/>), i.e. it returns [0, (divisor - 1) .. 1] instead of the remainder [0, 1 .. (divisor - 1)].</para>
    //  /// <para>All remainder values greater-than zero are in reverse order from that defined by <typeparamref name="TNumber"/>.</para>
    //  /// </summary>
    //  /// <typeparam name="TNumber"></typeparam>
    //  /// <param name="dividend"></param>
    //  /// <param name="divisor"></param>
    //  /// <param name="remainder"></param>
    //  /// <returns>
    //  /// <para>The reverse <paramref name="remainder"/> of (<paramref name="dividend"/> % <paramref name="divisor"/>).</para>
    //  /// <para>Also returns the normal <paramref name="remainder"/> as an out parameter.</para>
    //  /// </returns>
    //  public TNumber ReverseRemainder(TNumber divisor, out TNumber remainder)
    //  {
    //    remainder = dividend % divisor;

    //    return TNumber.IsZero(remainder) ? remainder : TNumber.CopySign(divisor, dividend) - remainder;
    //  }
    //}

    #endregion

    #region TruncRem

    /// <summary>
    /// <para>Computes the integer (i.e. the truncated or floor) quotient and remainder of (<paramref name="dividend"/> / <paramref name="divisor"/>).</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="dividend"></param>
    /// <param name="divisor"></param>
    /// <returns>Returns the integer (i.e. the truncated or floor) quotient and remainder.</returns>
    public static (TNumber TruncatedQuotient, TNumber Remainder) TruncRem<TNumber>(this TNumber dividend, TNumber divisor)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      var remainder = dividend % divisor;

      return ((dividend - remainder) / divisor, remainder);
    }

    #endregion

    #region RoundToNearest

    private static bool m_roundNearestAlternatingState; // This is a field used for the method below.

    extension<TNumber>(TNumber value)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      /// <summary>
      /// <para>Rounds the <paramref name="value"/> to the nearest boundary according to the strategy <paramref name="mode"/>.</para>
      /// <remark>Use .NET built-in functionality when possible.</remark>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <typeparam name="TNearest"></typeparam>
      /// <param name="value"></param>
      /// <param name="mode"></param>
      /// <param name="nearestTowardZero"></param>
      /// <param name="nearestAwayFromZero"></param>
      /// <returns></returns>
      public TNearest RoundToNearest<TNearest>(UniversalRounding mode, TNearest nearestTowardZero, TNearest nearestAwayFromZero)
      where TNearest : System.Numerics.INumber<TNearest>
      => nearestTowardZero == nearestAwayFromZero ? nearestTowardZero // First, if the two boundaries are equal, it's the one.
      : mode switch
      {
        // Second, we take care of the integral rounding cases.
        UniversalRounding.IntegralAwayFromZero => nearestAwayFromZero,
        UniversalRounding.IntegralTowardZero => nearestTowardZero,
        UniversalRounding.IntegralToNegativeInfinity => TNearest.Min(nearestTowardZero, nearestAwayFromZero),
        UniversalRounding.IntegralToPositiveInfinity => TNearest.Max(nearestTowardZero, nearestAwayFromZero),
        UniversalRounding.IntegralToRandom => value.RoundToNearestRandom(nearestTowardZero, nearestAwayFromZero),
        UniversalRounding.IntegralToEven => TNearest.IsEvenInteger(nearestTowardZero) ? nearestTowardZero : nearestAwayFromZero,
        //UniversalRounding.WholeAlternating => value.RoundToNearestAlternating(nearestTowardZero, nearestAwayFromZero),
        UniversalRounding.IntegralToOdd => TNearest.IsOddInteger(nearestTowardZero) ? nearestTowardZero : nearestAwayFromZero,
        // And third, we delegate to halfway-rounding strategies.
        _ => TNumber.Abs(value - TNumber.CreateChecked(nearestTowardZero)) is var distanceTowardsZero
          && TNumber.Abs(TNumber.CreateChecked(nearestAwayFromZero) - value) is var distanceAwayFromZero
          && (distanceTowardsZero < distanceAwayFromZero) ? nearestTowardZero // A clear win for towards-zero, no halfway-rounding needed.
          : (distanceAwayFromZero < distanceTowardsZero) ? nearestAwayFromZero // A clear win for away-from-zero, no halfway-rounding needed.
          : mode switch // If the distances are equal, i.e. exactly halfway, we use the appropriate rounding strategy to resolve a winner.
          {
            UniversalRounding.HalfToEven => TNearest.IsEvenInteger(nearestTowardZero) ? nearestTowardZero : nearestAwayFromZero,
            UniversalRounding.HalfAwayFromZero => nearestAwayFromZero,
            UniversalRounding.HalfTowardZero => nearestTowardZero,
            UniversalRounding.HalfToNegativeInfinity => TNearest.Min(nearestTowardZero, nearestAwayFromZero),
            UniversalRounding.HalfToPositiveInfinity => TNearest.Max(nearestTowardZero, nearestAwayFromZero),
            UniversalRounding.HalfToRandom => value.RoundToNearestRandom(nearestTowardZero, nearestAwayFromZero),
            UniversalRounding.HalfToAlternating => value.RoundToNearestAlternating(nearestTowardZero, nearestAwayFromZero),
            UniversalRounding.HalfToOdd => TNearest.IsOddInteger(nearestAwayFromZero) ? nearestAwayFromZero : nearestTowardZero,
            _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
          }
      };

      public TNearest RoundToNearest<TNearest>(HalfRounding mode, TNearest nearestTowardZero, TNearest nearestAwayFromZero)
        where TNearest : System.Numerics.INumber<TNearest>
        => TNumber.Abs(value - TNumber.CreateChecked(nearestTowardZero)) is var distanceTowardsZero
        && TNumber.Abs(TNumber.CreateChecked(nearestAwayFromZero) - value) is var distanceAwayFromZero
        && (distanceTowardsZero < distanceAwayFromZero) ? nearestTowardZero // A clear win for towards-zero, no halfway-rounding needed.
        : (distanceTowardsZero > distanceAwayFromZero) ? nearestAwayFromZero // A clear win for away-from-zero, no halfway-rounding needed.
        : mode switch // If the distances are equal, i.e. exactly halfway, we use the appropriate rounding strategy to resolve a winner.
        {
          HalfRounding.ToEven => TNearest.IsEvenInteger(nearestTowardZero) ? nearestTowardZero : nearestAwayFromZero,
          HalfRounding.AwayFromZero => nearestAwayFromZero,
          HalfRounding.TowardZero => nearestTowardZero,
          HalfRounding.ToNegativeInfinity => TNearest.Min(nearestTowardZero, nearestAwayFromZero),
          HalfRounding.ToPositiveInfinity => TNearest.Max(nearestTowardZero, nearestAwayFromZero),
          HalfRounding.ToRandom => value.RoundToNearestRandom(nearestTowardZero, nearestAwayFromZero),
          HalfRounding.ToAlternating => value.RoundToNearestAlternating(nearestTowardZero, nearestAwayFromZero),
          HalfRounding.ToOdd => TNearest.IsOddInteger(nearestAwayFromZero) ? nearestAwayFromZero : nearestTowardZero,
          _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
        };

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="value"></param>
      /// <param name="customState"></param>
      /// <returns></returns>
      public TNearest RoundToNearestAlternating<TNearest>(TNearest nearestTowardZero, TNearest nearestAwayFromZero, ref bool customState)
        where TNearest : System.Numerics.INumber<TNearest>
        => (customState = !customState)
        ? nearestTowardZero
        : nearestAwayFromZero;

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="value"></param>
      /// <returns></returns>
      public TNearest RoundToNearestAlternating<TNearest>(TNearest nearestTowardZero, TNearest nearestAwayFromZero)
        where TNearest : System.Numerics.INumber<TNearest>
        => value.RoundToNearestAlternating(nearestTowardZero, nearestAwayFromZero, ref m_roundNearestAlternatingState);

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <remarks>
      /// <para><see cref="HalfRounding.HalfToRandom"/></para>
      /// </remarks>
      /// <typeparam name="TNumber"></typeparam>
      /// <param name="value"></param>
      /// <param name="rng"></param>
      /// <returns></returns>
      public TNearest RoundToNearestRandom<TNearest>(TNearest nearestTowardZero, TNearest nearestAwayFromZero, System.Random? rng = null)
        where TNearest : System.Numerics.INumber<TNearest>
        => (rng ?? System.Random.Shared).NextDouble() < 0.5
        ? nearestTowardZero
        : nearestAwayFromZero;
    }

    #endregion
  }
}
