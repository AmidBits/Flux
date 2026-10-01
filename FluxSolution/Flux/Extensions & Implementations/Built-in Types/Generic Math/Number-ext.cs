namespace Flux
{
  public static partial class Number
  {
    extension<TNumber>(TNumber)
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
      public static TNumber DetentInterval(TNumber value, TNumber interval, TNumber proximity)
        => TNumber.CreateChecked(int.CreateChecked(value / interval)) * interval is var tzPosition && TNumber.Abs(tzPosition - value) <= proximity
        ? tzPosition
        : tzPosition + interval is var afzPosition && TNumber.Abs(afzPosition - value) <= proximity
        ? afzPosition
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
      public static TNumber DetentPosition(TNumber value, TNumber position, TNumber proximity)
        => TNumber.Abs(position - value) <= proximity // Inquire whether the difference is within to specified proximity.
        ? position // If so, detent to the position.
        : value; // Otherwise leave it where it is.

      #endregion

      #region EqualsWithin..

      /// <summary>
      /// <para>Perform an absolute and a relative equality test, for maximum coverage of small and large vales.</para>
      /// <para>Absolute equality checks if the absolute difference between <paramref name="value"/> and <paramref name="other"/> is smaller than a predefined <paramref name="tolerance"/>. This is useful when you want to ensure the numbers are "close enough" without considering their scale.</para>
      /// <para>Absolute equality is simpler and works well for small numbers or fixed tolerances.</para>
      /// <para>Relative equality considers the scale of the numbers by dividing the absolute difference by the magnitude of the numbers. This is useful when comparing numbers that may vary significantly in scale.</para>
      /// <para>Relative equality is better for larger numbers or numbers with varying scales, as it adjusts the tolerance dynamically.</para>
      /// </summary>
      /// <typeparam name="TTolerance"></typeparam>
      /// <param name="value">The value of interest.</param>
      /// <param name="other">The value to compare to.</param>
      /// <param name="tolerance">E.g. 1e-10.</param>
      /// <returns></returns>
      public static bool EqualsWithin<TTolerance>(TNumber value, TNumber other, TTolerance tolerance)
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

        return difference <= comparison; // The difference is LTE to the signed significant digits raised-to-the-power-of radix.
      }

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
      public static bool EqualsWithin<TSignificantDigits, TRadix>(TNumber value, TNumber other, TSignificantDigits significantDigits, TRadix radix)
        where TSignificantDigits : System.Numerics.IBinaryInteger<TSignificantDigits>
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radix, TRadix.One);

        if (value == other) // The value and other are equal.
          return true;

        if (TNumber.IsNaN(value) || TNumber.IsNaN(other))
          return false;

        if (TNumber.IsInfinity(value) || TNumber.IsInfinity(other))
          return value == other;

        var difference = double.CreateChecked(TNumber.Abs(value - other));
        var comparison = double.Pow(double.CreateChecked(radix), double.CreateChecked(significantDigits));

        return difference <= comparison; // The difference is LTE to the signed significant digits raised-to-the-power-of radix.
      }

      #endregion

      #region FoldAcross

      /// <summary>
      /// <para>Performs a triangle‑wave fold (i.e. back and forth) across the closed interval [<paramref name="minValue"/>, <paramref name="maxValue"/>], until the <paramref name="value"/> is within the closed interval.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="minValue"></param>
      /// <param name="maxValue"></param>
      /// <returns></returns>
      public static TNumber FoldAcross(TNumber value, TNumber minValue, TNumber maxValue)
      {
        if (value >= minValue && value <= maxValue)
          return value;

        var above = value > maxValue; // Determine overshoot direction.

        var overshoot = above ? value - maxValue : minValue - value;
        var range = maxValue - minValue;

        var (q, r) = IntegerDivRemTruncated(overshoot, range); // Truncated division gives quotient parity + remainder.

        return TNumber.IsEvenInteger(q)
            ? (above ? maxValue - r : minValue + r) // Even q → reflect toward boundary crossed.
            : (above ? minValue + r : maxValue - r); // Odd q → reflect toward opposite boundary.
      }

      //public static TNumber FoldAcross(TNumber value, TNumber minValue, TNumber maxValue)
      //  => (value > maxValue)
      //  ? (TruncDivRem(value - maxValue, maxValue - minValue) is var (tqHi, remHi) && TNumber.IsEvenInteger(tqHi) ? maxValue - remHi : minValue + remHi)
      //  : (value < minValue)
      //  ? (TruncDivRem(minValue - value, maxValue - minValue) is var (tqLo, remLo) && TNumber.IsEvenInteger(tqLo) ? minValue + remLo : maxValue - remLo)
      //  : value;

      #endregion

      /// <summary>
      /// <para>Indicates whether <typeparamref name="TNumber"/> is a type built-in to <see cref="System.Runtime"/> (i.e. .NET).</para>
      /// </summary>
      /// <returns></returns>
      public static bool IsBuiltIn()
        => typeof(TNumber).Assembly == typeof(byte).Assembly; // System.Private.CoreLib

      #region IsConsideredPlural

      /// <summary>
      /// <para>Determines whether the number is considered plural in terms of writing.</para>
      /// <para></para>
      /// </summary>
      /// <remarks>This function consider all numbers (e.g. 1.0, 2, etc.), except <c>integer</c> types equal to 1, to be plural.</remarks>
      /// <returns></returns>
      public static bool IsConsideredPlural(TNumber value)
        => !(value == TNumber.One && value.GetType().IsNumericsIBinaryInteger()); // Only an integer 1 (not 1.0) is singular, otherwise a number is considered plural.

      #endregion

      //#region ..MultipleOf

      ///// <summary>
      ///// 
      ///// </summary>
      ///// <param name="value"></param>
      ///// <param name="multiple">The multiple to which <paramref name="value"/> is measured.</param>
      ///// <param name="unequal"></param>
      ///// <param name="rule"></param>
      ///// <returns></returns>
      //public static (TNumber MultipleTowardZero, TNumber NearestMultiple, TNumber MultipleAwayFromZero) MultipleOf(TNumber value, TNumber multiple, bool unequal = false, NearestRoundingRule rule = NearestRoundingRule.ToEven)
      //{
      //  var csmv = TNumber.CopySign(multiple, value);

      //  var motz = value - (value % multiple);
      //  var moafz = motz;

      //  if (unequal && motz == value)
      //    motz -= csmv;

      //  if (unequal || moafz != value)
      //    moafz += csmv;

      //  var nv = RoundToNearestValue(value, rule, false, motz, moafz);

      //  return (motz, nv, moafz);
      //}

      //#endregion

      public static TNumber Wrap(TNumber value, TNumber min, TNumber max, IntervalNotation notation, TNumber epsilon)
      {
        if (max <= min)
          throw new ArgumentException("max must be greater than min.");

        if (epsilon <= TNumber.Zero)
          throw new ArgumentException("epsilon must be positive.");

        var range = max - min;

        if (notation == IntervalNotation.Closed)
          range += epsilon;
        else if (notation == IntervalNotation.Open)
          range -= epsilon;

        var shift = min;

        if (notation == IntervalNotation.HalfOpenLeft)
          shift = max;
        else if (notation == IntervalNotation.Open)
          shift = min + epsilon;

        return shift + EuclideanModulo(value - shift, range);
      }

      //public static TNumber WrapHalfOpenRight(TNumber value, TNumber min, TNumber max)
      //  => min + ModuloHalfOpenRight(value - min, max - min);

      //public static TNumber WrapHalfOpenLeft(TNumber value, TNumber min, TNumber max)
      //  => min + ModuloHalfOpenLeft(value - min, max - min);

      //public static TNumber WrapClosed(TNumber value, TNumber min, TNumber max)
      //  => min + ModuloClosed(value - min, max - min);

      //public static TNumber WrapOpen(TNumber value, TNumber min, TNumber max)
      //  => min + ModuloOpen(value - min, max - min);

      #region WrapAround

      /// <summary>
      /// <para>Wraps a value around, to the opposite side, if the value exceeds either boundary.</para>
      /// </summary>
      /// <param name="minValue"></param>
      /// <param name="maxValue"></param>
      /// <returns></returns>
      public static TNumber WrapAround(TNumber value, TNumber minValue, TNumber maxValue)
        => (value > maxValue)
        ? minValue + ((value - maxValue - TNumber.One) % (maxValue - minValue + TNumber.One))
        : (value < minValue)
        ? maxValue - ((minValue - value - TNumber.One) % (maxValue - minValue + TNumber.One))
        : value;

      //public static TNumber UlpWrap(TNumber value, TNumber minValue, TNumber maxValue)
      //{
      //  var ulp = GetUlp(value);

      //  if (value > maxValue)
      //  {
      //    return minValue + ((value - maxValue - ulp) % (maxValue - minValue + ulp));
      //  }
      //  else if (value < minValue)
      //  {
      //    return maxValue - ((minValue - value - ulp) % (maxValue - minValue + ulp));
      //  }
      //  else
      //  {
      //    return value;
      //  }
      //}

      #endregion

      public static TNumber WrapToInterval(TNumber value, TNumber minValue, TNumber maxValue/*, WrapMode wrapMode = WrapMode.Normalized*/, IntervalNotation intervalNotation, TNumber epsilon)
      {
        System.ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(minValue, maxValue);
        System.ArgumentOutOfRangeException.ThrowIfNegative(epsilon);

        //if (wrapMode == WrapMode.Strict)
        //  System.ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(minValue, maxValue);
        //else if (wrapMode == WrapMode.Normalized && maxValue < minValue)
        //  (minValue, maxValue) = (maxValue, minValue);
        //else if (wrapMode == WrapMode.Centered)
        //{
        //  var mid = (minValue + maxValue) * TNumber.CreateChecked(0.5);
        //  var half = (maxValue - minValue) * TNumber.CreateChecked(0.5);

        //  return mid + WrapToInterval(value - mid, -half, half, WrapMode.Normalized, intervalNotation, epsilon);
        //}

        var range = maxValue - minValue;
        if (TNumber.IsZero(range))
          return minValue;

        var wrapped = (value - minValue) % range;
        if (TNumber.IsNegative(wrapped))
          wrapped += range;

        var result = minValue + wrapped;

        var ulp = Ulp(result);

        var eps = TNumber.IsZero(epsilon) ? ulp : epsilon;

        return intervalNotation switch
        {
          IntervalNotation.Closed => result,
          IntervalNotation.HalfOpenRight => result >= maxValue - eps ? minValue : result,
          IntervalNotation.HalfOpenLeft => result <= minValue + eps ? maxValue : result,
          IntervalNotation.Open => (result <= minValue + eps) ? minValue + eps : (result >= maxValue - eps) ? maxValue - eps : result,
          _ => result,
        };
      }


      public static TNumber Wrap(TNumber value, TNumber minValue, TNumber maxValue, IntervalNotation notation)
      {
        return notation switch
        {
          IntervalNotation.Closed => WrapClosed(value, minValue, maxValue),
          IntervalNotation.HalfOpenLeft => WrapHalfOpenLeft(value, minValue, maxValue),
          IntervalNotation.HalfOpenRight => WrapHalfOpenRight(value, minValue, maxValue),
          IntervalNotation.Open => WrapOpen(value, minValue, maxValue),
          _ => throw new System.ArgumentOutOfRangeException(nameof(notation)),
        };
      }

      public static TNumber WrapClosed(TNumber value, TNumber minValue, TNumber maxValue)
      {
        var rem = ModulusOperators.EuclideanModulo(value - minValue, maxValue - minValue);

        if (TNumber.IsZero(rem))
          return maxValue;

        return minValue + rem;

        //var range = (maxValue - minValue) + Number.GetUlp(minValue);

        //return minValue + (value - minValue - TFloat.Floor((value - minValue) / range) * range);
      }

      public static TNumber WrapHalfOpenLeft(TNumber value, TNumber minValue, TNumber maxValue)
      {
        var rem = ModulusOperators.EuclideanModulo(value - minValue, maxValue - minValue);

        if (TNumber.IsZero(rem))
          return maxValue;

        return minValue + rem;

        //var shift = minValue + Number.GetUlp(minValue);
        //var range = maxValue - shift;

        //return shift + (value - shift - TFloat.Floor((value - shift) / range) * range);
      }

      public static TNumber WrapHalfOpenRight(TNumber value, TNumber minValue, TNumber maxValue)
      {
        return minValue + ModulusOperators.EuclideanModulo(value - minValue, maxValue - minValue);

        //var range = maxValue - minValue;

        //return minValue + (value - minValue - TFloat.Floor((value - minValue) / range) * range);
      }

      public static TNumber WrapOpen(TNumber value, TNumber minValue, TNumber maxValue)
      {
        var rem = ModulusOperators.EuclideanModulo(value - minValue, maxValue - minValue);

        if (TNumber.IsZero(rem))
          return minValue + Number.Ulp(minValue);

        return minValue + rem;

        //var range = maxValue - minValue;

        //var w = minValue + (value - minValue - TFloat.Floor((value - minValue) / range) * range);

        //if (w == minValue)
        //  return minValue + Number.GetUlp(minValue);

        //return w;
      }
    }

    //#region PowOf

    ///// <summary>
    ///// <para>Round a <paramref name="value"/> to the nearest power-of-<paramref name="radix"/> away-from-zero and whether to ensure it is <paramref name="unequal"/>.</para>
    ///// </summary>
    ///// <typeparam name="TNumber"></typeparam>
    ///// <typeparam name="TRadix"></typeparam>
    ///// <param name="value"></param>
    ///// <param name="radix"></param>
    ///// <param name="unequal"></param>
    ///// <returns></returns>
    //public static TNumber PowOfAwayFromZero<TNumber, TRadix>(this TNumber value, TRadix radix, bool unequal = false)
    //  where TNumber : System.Numerics.INumber<TNumber>
    //  where TRadix : System.Numerics.IBinaryInteger<TRadix>
    //  => TNumber.CreateChecked(Units.Radix.AssertMember(radix)) is var r && TNumber.IsZero(value)
    //  ? value
    //  : TNumber.CopySign(TNumber.Abs(value) is var v && r.FastIntegerPow(v.FastIntegerLog(radix, out var _).IlogTz, out var _).IpowTz is var p && (p == v ? p : p * r) is var afz && (unequal || !TNumber.IsInteger(value)) && afz == v ? afz * r : afz, value);

    ///// <summary>
    ///// <para>Rounds the <paramref name="value"/> to the closest power-of-<paramref name="radix"/> (i.e. of <paramref name="powOfTowardsZero"/> and <paramref name="powOfAwayFromZero"/> which are computed and returned as out parameters) according to <paramref name="unequal"/> and the strategy <paramref name="mode"/>. Negative <paramref name="value"/> resilient.</para>
    ///// </summary>
    ///// <param name="value">The value for which the nearest power-of-<paramref name="radix"/> of will be found.</param>
    ///// <param name="radix">The radix (base) of the power-of-<paramref name="radix"/>.</param>
    ///// <param name="unequal">Proper means nearest but <paramref name="unequal"/> to <paramref name="value"/> if it's a power-of-<paramref name="radix"/>, i.e. the two power-of-radix will be properly "nearest" (but not the same), or LT/GT rather than LTE/GTE.</param>
    ///// <param name="powOfTowardsZero">Outputs the power-of-<paramref name="radix"/> of that is closer to zero.</param>
    ///// <param name="powOfAwayFromZero">Outputs the power-of-<paramref name="radix"/> of that is farther from zero.</param>
    ///// <returns>The nearest two power-of-<paramref name="radix"/> as out parameters and the the nearest of those two is returned.</returns>
    //public static TNumber PowOf<TNumber, TRadix>(this TNumber value, TRadix radix, bool unequal, HalfRounding mode, out TNumber powOfTowardsZero, out TNumber powOfAwayFromZero)
    //  where TNumber : System.Numerics.INumber<TNumber>
    //  where TRadix : System.Numerics.IBinaryInteger<TRadix>
    //{
    //  powOfTowardsZero = value.PowOfTowardZero(radix, unequal);
    //  powOfAwayFromZero = value.PowOfAwayFromZero(radix, unequal);

    //  return value.RoundToNearest(mode, powOfTowardsZero, powOfAwayFromZero);
    //}

    ///// <summary>
    ///// <para>Round a <paramref name="value"/> to the nearest power-of-<paramref name="radix"/> toward-zero and whether to ensure it is <paramref name="unequal"/>.</para>
    ///// </summary>
    ///// <typeparam name="TNumber"></typeparam>
    ///// <typeparam name="TRadix"></typeparam>
    ///// <param name="value"></param>
    ///// <param name="radix"></param>
    ///// <param name="unequal"></param>
    ///// <returns></returns>
    //public static TNumber PowOfTowardZero<TNumber, TRadix>(this TNumber value, TRadix radix, bool unequal = false)
    //  where TNumber : System.Numerics.INumber<TNumber>
    //  where TRadix : System.Numerics.IBinaryInteger<TRadix>
    //  => TNumber.CreateChecked(Units.Radix.AssertMember(radix)) is var r && TNumber.IsZero(value)
    //  ? value
    //  : TNumber.CopySign(TNumber.Abs(value) is var v && r.FastIntegerPow(v.FastIntegerLog(radix, out var _).IlogTz, out var _).IpowTz is var p && (unequal && TNumber.IsInteger(value)) && p == v ? p / r : p, value);

    //#endregion
  }
}
