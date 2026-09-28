namespace Flux
{
  public static partial class BinaryInteger
  {
    extension<TInteger>(TInteger)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      public static (TInteger PowerTowardZero, TInteger PowerAwayFromZero, bool IsExactPower) RoundToPower<TRadix>(TInteger value, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        AssertRadix(radix, out TInteger rdx);

        var (logTowardZero, logAwayFromZero, isExactLog) = Log(value, rdx);

        return (Pow(rdx, logTowardZero), Pow(rdx, logAwayFromZero), isExactLog);
      }
    }
  }

  public static partial class FloatingPoint
  {
    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      #region RoundToNearestInteger

      /// <summary>
      /// <para>Gets the nearest integer from a <typeparamref name="TFloat"/> <paramref name="value"/>.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="mode"></param>
      /// <returns></returns>
      /// <exception cref="ArgumentOutOfRangeException"></exception>
      public static TFloat RoundToNearestInteger(TFloat value, NearestRoundingRule rule = NearestRoundingRule.ToEven)
      {
        if (TFloat.IsInteger(value))
          return value;

        var floor = TFloat.Floor(value);
        var fraction = value - floor;
        var half = TFloat.CreateChecked(0.5);

        var ceiling = floor + TFloat.One;

        if (fraction == half)
        {
          return rule switch
          {
            NearestRoundingRule.TowardNegativeInfinity => floor,
            NearestRoundingRule.TowardPositiveInfinity => ceiling,
            NearestRoundingRule.TowardZero => TFloat.Truncate(value),
            NearestRoundingRule.AwayFromZero => TFloat.IsNegative(value) ? floor : ceiling,
            NearestRoundingRule.ToEven => TFloat.IsEvenInteger(floor) ? floor : ceiling,
            NearestRoundingRule.ToOdd => TFloat.IsOddInteger(floor) ? floor : ceiling,
            NearestRoundingRule.Random => System.Random.Shared.Next(2) == 0 ? floor : ceiling,
            _ => throw new ArgumentOutOfRangeException(nameof(rule)),
          };
        }

        return fraction < half ? floor : ceiling; // Non-midpoint nearest-integer rounding.
      }

      #endregion

      #region RoundToNearestIntegerAlternating

      public static TFloat RoundToNearestIntegerAlternating(TFloat value, ref bool alternatingState)
      {
        var cmp = CompareFractionToMidpoint(value);

        var floor = TFloat.Floor(value);

        if (cmp < 0)
          return floor;

        var ceiling = TFloat.Ceiling(value);

        if (cmp > 0)
          return ceiling;

        return (alternatingState = !alternatingState) ? floor : ceiling;
      }

      #endregion
    }

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.IPowerFunctions<TFloat>
    {
      #region RoundByPrecision

      /// <summary>
      /// <para>Rounds the <paramref name="value"/> to the nearest <paramref name="significantDigits"/> in base <paramref name="radix"/>. The <paramref name="nearestRoundingTies"/> specifies the halfway rounding strategy to use.</para>
      /// <example>
      /// <code>var r = RoundByPrecision(99.96535789, 2, HalfwayRounding.ToEven); // = 99.97 (compare with the corresponding <see cref="RoundByTruncatedPrecision{TSelf}(TSelf, UniversalRounding, int, int)"/> method)</code>
      /// </example>
      /// </summary>
      /// <typeparam name="TValue"></typeparam>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="value"></param>
      /// <param name="nearestRoundingTies"></param>
      /// <param name="significantDigits"></param>
      /// <param name="radix"></param>
      /// <returns></returns>
      public static TFloat RoundByPrecision<TRadix>(TFloat x, NearestRoundingRule nearestRoundingTies, int significantDigits, TRadix radix)
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(significantDigits);
        System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radix, TRadix.One);

        var scalar = TFloat.Pow(TFloat.CreateChecked(radix), TFloat.CreateChecked(significantDigits));

        return RoundToNearestInteger(x * scalar, nearestRoundingTies) / scalar;
      }

      #endregion

      #region RoundByTruncatedPrecision

      /// <summary>
      /// <para>Rounds <paramref name="x"/> by truncating to the specified number of <paramref name="significantDigits"/> in base <paramref name="radix"/> and then round using the <paramref name="nearestRoundingTies"/>. The reason for doing this is because unless a value is EXACTLY between two numbers, to the decimal, it will be rounded based on the next least significant decimal digit and so on.</para>
      /// <para><seealso href="https://stackoverflow.com/questions/1423074/rounding-to-even-in-c-sharp"/></para>
      /// <example>
      /// <code>var r = RoundByTruncatedPrecision(99.96535789, 2, HalfwayRounding.ToEven); // = 99.96 (compare with the corresponding <see cref="RoundByPrecision{TValue}(TValue, UniversalRounding, int, int)"/> method)</code>
      /// </example>
      /// </summary>
      /// <typeparam name="TValue"></typeparam>
      /// <param name="x"></param>
      /// <param name="nearestRoundingTies"></param>
      /// <param name="significantDigits"></param>
      /// <param name="radix"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static TFloat RoundByTruncatedPrecision<TRadix>(TFloat x, NearestRoundingRule nearestRoundingTies, int significantDigits, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(significantDigits);
        System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radix, TRadix.One);

        var scalar = TFloat.Pow(TFloat.CreateChecked(radix), TFloat.CreateChecked(significantDigits + 1));

        return RoundByPrecision(TFloat.Truncate(x * scalar) / scalar, nearestRoundingTies, significantDigits, radix);
      }

      #endregion
    }
  }

  public static partial class Number
  {
    extension<TNumber>(TNumber)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      #region IsMultipleOf

      /// <summary>
      /// Determines whether <paramref name="value"/> is an exact multiple of <paramref name="multiple"/>.
      /// </summary>
      /// <remarks>Uses Euclidean modulo so the sign of <paramref name="value"/> does not affect the result.
      /// Throws <see cref="System.ArgumentOutOfRangeException"/> if <paramref name="multiple"/> is less than or equal
      /// to zero.</remarks>
      /// <param name="value">The value to test for being a multiple.</param>
      /// <param name="multiple">The positive divisor to test against. Must be greater than zero.</param>
      /// <returns>True if <paramref name="value"/> is an exact multiple of <paramref name="multiple"/>; otherwise, false.</returns>
      public static bool IsMultipleOf(TNumber value, TNumber multiple)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(multiple);

        return EuclideanModulo(value, multiple) == TNumber.Zero;
      }

      #endregion

      #region RoundToMultiple

      public static TNumber RoundToMultiple(TNumber value, TNumber multiple, DirectedRoundingMode mode)
      {
        var quotient = mode switch
        {
          DirectedRoundingMode.TowardNegativeInfinity => IntegerDivRemFloored(value, multiple).Quotient,
          DirectedRoundingMode.TowardPositiveInfinity => IntegerDivRemCeiling(value, multiple).Quotient,
          DirectedRoundingMode.TowardZero => IntegerDivRemTruncated(value, multiple).Quotient,
          DirectedRoundingMode.AwayFromZero => IntegerDivRemEnveloped(value, multiple).Quotient,
          _ => throw new System.NotImplementedException(),
        };

        return quotient * multiple;
      }

      #endregion

      #region RoundToNearestOf

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="rule"></param>
      /// <param name="proper"></param>
      /// <param name="values"></param>
      /// <returns></returns>
      /// <exception cref="System.NullReferenceException"></exception>
      /// <exception cref="NotImplementedException"></exception>
      public static TNumber RoundToNearestOf(TNumber value, NearestRoundingRule rule, bool proper, params System.ReadOnlySpan<TNumber> values)
      {
        System.ArgumentOutOfRangeException.ThrowIfZero(values.Length);

        var closestValues = new System.Collections.Generic.List<TNumber>() { values[0] };
        var closestDistance = TNumber.Abs(value - TNumber.CreateChecked(values[0]));

        for (var i = 1; i < values.Length; i++)
        {
          var currentValue = values[i];
          var currentDistance = TNumber.Abs(value - TNumber.CreateChecked(currentValue));

          if ((!proper || currentValue != value) && currentDistance <= closestDistance)
          {
            if (currentDistance < closestDistance)
            {
              closestValues.Clear();
              closestDistance = currentDistance;
            }

            if (!closestValues.Contains(currentValue))
              closestValues.Add(currentValue);
          }
        }

        return rule switch // If the distances are equal, i.e. the value is exactly halfway to all closestValues, we use the appropriate rounding strategy to resolve a winner.
        {
          NearestRoundingRule.TowardNegativeInfinity => closestValues.Min() ?? throw new System.NullReferenceException(),
          NearestRoundingRule.TowardPositiveInfinity => closestValues.Max() ?? throw new System.NullReferenceException(),
          NearestRoundingRule.TowardZero => closestValues.AsSpan().InfimumSupremum(value, v => v, false) is var (infimumItem, infimumIndex, infimumValue, supremumItem, supremumIndex, supremumValue) && value >= TNumber.Zero ? (infimumIndex > -1 ? infimumValue : supremumValue) : (supremumIndex > -1 ? supremumValue : infimumValue),
          NearestRoundingRule.AwayFromZero => closestValues.AsSpan().InfimumSupremum(value, v => v, false) is var (infimumItem, infimumIndex, infimumValue, supremumItem, supremumIndex, supremumValue) && value >= TNumber.Zero ? (supremumIndex > -1 ? supremumValue : infimumValue) : (infimumIndex > -1 ? infimumValue : supremumValue),
          NearestRoundingRule.ToEven => closestValues.FirstOrValue(closestValues[0], TNumber.IsEvenInteger).Item,
          NearestRoundingRule.ToOdd => closestValues.FirstOrValue(closestValues[0], TNumber.IsOddInteger).Item,
          NearestRoundingRule.Random => closestValues.AsSpan().GetRandomElement(),
          _ => throw new NotImplementedException(),
        };
      }

      #endregion

      #region RoundToNearestMultiple

      public static TNumber RoundToNearestMultiple(TNumber value, TNumber multiple, NearestRoundingRule rule = NearestRoundingRule.ToEven)
      {
        System.ArgumentOutOfRangeException.ThrowIfZero(multiple);

        var (quotient, remainder) = IntegerDivRemFloored(value, multiple);

        if (TNumber.IsZero(remainder))
          return value;

        var floor = quotient * multiple;
        var ceiling = floor + TNumber.Abs(multiple);

        var distanceToFloor = remainder;
        var distanceToCeiling = TNumber.Abs(multiple) - remainder;

        if (distanceToFloor == distanceToCeiling)
        {
          return rule switch
          {
            NearestRoundingRule.TowardNegativeInfinity => floor,
            NearestRoundingRule.TowardPositiveInfinity => ceiling,
            NearestRoundingRule.TowardZero => TNumber.IsNegative(value) ? ceiling : floor,
            NearestRoundingRule.AwayFromZero => TNumber.IsNegative(value) ? floor : ceiling,
            NearestRoundingRule.ToEven => TNumber.IsEvenInteger(floor) ? floor : ceiling,
            NearestRoundingRule.ToOdd => TNumber.IsOddInteger(floor) ? floor : ceiling,
            NearestRoundingRule.Random => Random.Shared.Next(2) == 0 ? floor : ceiling,
            _ => throw new ArgumentOutOfRangeException(nameof(rule)),
          };
        }

        return distanceToFloor < distanceToCeiling ? floor : ceiling;
      }

      #endregion
    }
  }
}
