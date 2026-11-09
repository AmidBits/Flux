namespace Flux
{
  public static partial class BinaryInteger
  {
    extension<TInteger>(TInteger)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      #region GetClosestPowers

      public static (TInteger PowerTowardZero, TInteger PowerAwayFromZero, bool IsExactPower) GetClosestPowers(TInteger value, TInteger radix, bool strict = false)
      {
        System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radix, TInteger.One);

        if (TInteger.IsZero(value))
          return (TInteger.Zero, TInteger.Zero, false);

        var magnitude = TInteger.Abs(value);

        var upperMagnitude = TInteger.One;

        while (upperMagnitude < magnitude)
          upperMagnitude = checked(upperMagnitude * radix);

        var isExact = upperMagnitude == magnitude;

        var lowerMagnitude = isExact ? upperMagnitude : upperMagnitude / radix;

        if (isExact && strict)
        {
          lowerMagnitude /= radix;
          upperMagnitude = checked(upperMagnitude * radix);
        }

        var (towardZero, awayFromZero) = TInteger.IsNegative(value) ? (-lowerMagnitude, -upperMagnitude) : (lowerMagnitude, upperMagnitude);

        return (towardZero, awayFromZero, isExact);
      }

      #endregion

      #region RoundToPower

      public static (TInteger PowerTowardZero, TInteger PowerAwayFromZero, bool IsExactPower, TInteger RoundedPower) RoundToPower(TInteger value, TInteger radix, DirectedRoundingMode mode, bool strict = false)
      {
        var (towardZero, awayFromZero, isExact) = GetClosestPowers(value, radix, strict);

        var roundedPower = Number.RoundToValue(value, mode, false, towardZero, awayFromZero);

        return (towardZero, awayFromZero, isExact, roundedPower);
      }

      public static (TInteger PowerTowardZero, TInteger PowerAwayFromZero, bool IsExactPower, TInteger RoundedPower) RoundToPower(TInteger value, TInteger radix, NearestRoundingRule rule = NearestRoundingRule.ToEven, bool strict = false)
      {
        var (towardZero, awayFromZero, isExact) = GetClosestPowers(value, radix, strict);

        var roundedPower = Number.RoundToValue(value, rule, false, towardZero, awayFromZero);

        return (towardZero, awayFromZero, isExact, roundedPower);
      }

      #endregion
    }
  }

  public static partial class FloatingPoint
  {
    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      #region GetClosestIntegers

      public static (TFloat IntegerTowardZero, TFloat IntegerAwayFromZero, bool IsExactInteger) GetClosestIntegers(TFloat value, bool strict = false)
      {
        var towardZero = TFloat.Truncate(value);

        var isExact = towardZero == value;

        var awayFromZero = isExact ? towardZero : TFloat.IsNegative(value) ? towardZero - TFloat.One : towardZero + TFloat.One;

        if (isExact && strict)
        {
          if (TFloat.IsNegative(value))
          {
            towardZero += TFloat.One;
            awayFromZero -= TFloat.One;
          }
          else
          {
            towardZero -= TFloat.One;
            awayFromZero += TFloat.One;
          }
        }

        return (towardZero, awayFromZero, isExact);
      }

      #endregion

      #region RoundToInteger

      public static (TFloat IntegerTowardZero, TFloat IntegerAwayFromZero, bool IsExactInteger, TFloat RoundedInteger) RoundToInteger(TFloat value, DirectedRoundingMode mode, bool strict = false)
      {
        var (towardZero, awayFromZero, isExact) = GetClosestIntegers(value, strict);

        var roundedInteger = Number.RoundToValue(value, mode, false, towardZero, awayFromZero);

        return (towardZero, awayFromZero, isExact, roundedInteger);
      }

      public static (TFloat IntegerTowardZero, TFloat IntegerAwayFromZero, bool IsExactInteger, TFloat RoundedInteger) RoundToInteger(TFloat value, NearestRoundingRule rule = NearestRoundingRule.ToEven, bool strict = false)
      {
        var (towardZero, awayFromZero, isExact) = GetClosestIntegers(value, strict);

        var roundedInteger = Number.RoundToValue(value, rule, false, towardZero, awayFromZero);

        return (towardZero, awayFromZero, isExact, roundedInteger);
      }

      #endregion

      #region Truncate

      /// <summary>
      /// <para>Truncates a value at the specified number of <paramref name="significantDigits"/> (decimal places).</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="significantDigits"></param>
      /// <returns></returns>
      public static TFloat Truncate(TFloat value, int significantDigits)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(significantDigits);

        var m = TFloat.CreateChecked(System.Numerics.BigInteger.Pow(10, significantDigits));

        return TFloat.Truncate(value * m) / m;
      }

      #endregion
    }

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.IPowerFunctions<TFloat>
    {
      #region RoundByPrecision

      /// <summary>
      /// <para>Rounds the <paramref name="value"/> to the nearest <paramref name="significantDigits"/> in base <paramref name="radix"/>. The <paramref name="rule"/> specifies the halfway rounding strategy to use.</para>
      /// <example>
      /// <code>var r = RoundByPrecision(99.96535789, 2, HalfwayRounding.ToEven); // = 99.97 (compare with the corresponding <see cref="RoundByTruncatedPrecision{TSelf}(TSelf, UniversalRounding, int, int)"/> method)</code>
      /// </example>
      /// </summary>
      /// <typeparam name="TValue"></typeparam>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="value"></param>
      /// <param name="rule"></param>
      /// <param name="significantDigits"></param>
      /// <param name="radix"></param>
      /// <returns></returns>
      public static TFloat RoundByPrecision<TRadix>(TFloat value, NearestRoundingRule rule, int significantDigits, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(significantDigits);
        System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radix, TRadix.One);

        var scalar = TFloat.Pow(TFloat.CreateChecked(radix), TFloat.CreateChecked(significantDigits));

        return RoundToInteger(value * scalar, rule).RoundedInteger / scalar;
      }

      #endregion

      #region RoundByTruncatedPrecision

      /// <summary>
      /// <para>Rounds <paramref name="value"/> by truncating to the specified number of <paramref name="significantDigits"/> in base <paramref name="radix"/> and then round using the <paramref name="nearestRoundingTies"/>. The reason for doing this is because unless a value is EXACTLY between two numbers, to the decimal, it will be rounded based on the next least significant decimal digit and so on.</para>
      /// <para><seealso href="https://stackoverflow.com/questions/1423074/rounding-to-even-in-c-sharp"/></para>
      /// <example>
      /// <code>var r = RoundByTruncatedPrecision(99.96535789, 2, HalfwayRounding.ToEven); // = 99.96 (compare with the corresponding <see cref="RoundByPrecision{TValue}(TValue, UniversalRounding, int, int)"/> method)</code>
      /// </example>
      /// </summary>
      /// <typeparam name="TValue"></typeparam>
      /// <param name="value"></param>
      /// <param name="nearestRoundingTies"></param>
      /// <param name="significantDigits"></param>
      /// <param name="radix"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static TFloat RoundByTruncatedPrecision<TRadix>(TFloat value, NearestRoundingRule nearestRoundingTies, int significantDigits, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(significantDigits);
        System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radix, TRadix.One);

        var scalar = TFloat.Pow(TFloat.CreateChecked(radix), TFloat.CreateChecked(significantDigits + 1));

        return RoundByPrecision(TFloat.Truncate(value * scalar) / scalar, nearestRoundingTies, significantDigits, radix);
      }

      #endregion
    }

    extension<TFloat>(TFloat value)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      #region Envelop

      /// <summary>
      /// <para>Envelops a value.</para>
      /// <para>Equivalent to the opposite effect of the Truncate() functionality, i.e. instead of truncating the fraction and essentially executing a round-toward-zero, envelop the fraction and execute a round-away-from-zero.</para>
      /// <para>It can also be seen as a companion function to truncate(). Unlike truncate() which calls floor() for positive numbers and ceiling() for negative; envelope() calls ceiling() for positive numbers and floor() for negative.</para>
      /// </summary>
      /// <remarks>Like truncate, envelop is a symmetric biased around 0 type rounding.</remarks>
      /// <typeparam name="TFloat"></typeparam>
      /// <param name="value"></param>
      /// <returns></returns>
      public TFloat Envelop()
        => TFloat.IsNegative(value) ? TFloat.Floor(value) : TFloat.Ceiling(value);

      /// <summary>
      /// <para>Envelops a value at the specified number of <paramref name="significantDigits"/> (decimal places).</para>
      /// <para>Equivalent to the opposite effect of the Truncate() functionality, i.e. instead of truncating the fraction and essentially executing a round-toward-zero, envelop the fraction and essentially execute a round-away-from-zero.</para>
      /// <para>It can also be seen as a companion function to truncate(). Unlike truncate() which calls floor() for positive numbers and ceiling() for negative; envelope() calls ceiling() for positive numbers and floor() for negative.</para>
      /// </summary>
      /// <remarks>Like truncate, envelop is a symmetric biased around 0 type rounding.</remarks>
      /// <typeparam name="TFloat"></typeparam>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value"></param>
      /// <param name="significantDigits"></param>
      /// <returns></returns>
      public TFloat Envelop(int significantDigits)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(significantDigits);

        var factor = TFloat.CreateChecked(System.Numerics.BigInteger.Pow(10, significantDigits));

        return Envelop(value * factor) / factor;
      }

      #endregion

      #region Normalize

      /// <summary>
      /// <para>Maps a <paramref name="value"/> from the interval [<paramref name="minValue"/>, <paramref name="maxValue"/>] onto the unit interval [0, 1], preserving its relative position.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="minValue"></param>
      /// <param name="maxValue"></param>
      /// <returns></returns>
      public TFloat Normalize(TFloat minValue, TFloat maxValue)
        => (value - minValue) / (maxValue - minValue);

      #endregion

      #region Quantize

      /// <summary>
      /// <para>Quantizes a value by snapping it to a multiple of <paramref name="step"/> according to the specified directed rounding <paramref name="mode"/>.</para>
      /// </summary>
      /// <remarks>Equivalent to: <c>RoundToInteger(<paramref name="value"/> / <paramref name="step"/>, <paramref name="mode"/>) * <paramref name="step"/></c></remarks>
      /// <param name="step"></param>
      /// <param name="mode"></param>
      /// <returns></returns>
      public TFloat Quantize(TFloat step, DirectedRoundingMode mode) => RoundToInteger(value / step, mode).RoundedInteger * step;

      /// <summary>
      /// <para>Quantizes a value by snapping it to a multiple of <paramref name="step"/> according to the specified nearest rounding <paramref name="rule"/>.</para>
      /// </summary>
      /// <remarks>Equivalent to: <c>RoundToNearestInteger(<paramref name="value"/> / <paramref name="step"/>, <paramref name="rule"/>) * <paramref name="step"/></c></remarks>
      /// <param name="step"></param>
      /// <param name="rule"></param>
      /// <returns></returns>
      public TFloat Quantize(TFloat step, NearestRoundingRule rule) => RoundToInteger(value / step, rule).RoundedInteger * step;

      #endregion
    }
  }

  public static partial class Number
  {
    extension<TNumber>(TNumber)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      #region GetClosestMultiples

      public static (TNumber MultipleTowardZero, TNumber MultipleAwayFromZero, bool IsExactMultiple) GetClosestMultiples(TNumber value, TNumber multiple, bool strict = false)
      {
        System.ArgumentOutOfRangeException.ThrowIfZero(multiple);

        var (towardQuotient, towardRemainder) = IntegerDivRemDirected(value, multiple, DirectedRoundingMode.TowardZero);

        var isExact = towardRemainder == TNumber.Zero;

        TNumber awayQuotient;

        if (strict || !isExact)
        {
          awayQuotient = IntegerDivRemDirected(value, multiple, DirectedRoundingMode.AwayFromZero).Quotient;

          if (strict && isExact)
          {
            var step = TNumber.IsNegative(value) ? -TNumber.One : TNumber.One;

            towardQuotient -= step;
            awayQuotient += step;
          }
        }
        else
        {
          awayQuotient = towardQuotient;
        }

        return (towardQuotient * multiple, awayQuotient * multiple, isExact);
      }

      #endregion

      #region RoundToMultiple

      public static (TNumber MultipleTowardZero, TNumber MultipleAwayFromZero, bool IsExactMultiple, TNumber RoundedMultiple) RoundToMultiple(TNumber value, TNumber multiple, DirectedRoundingMode mode, bool strict = false)
      {
        var (towardZero, awayFromZero, isExact) = GetClosestMultiples(value, multiple, strict);

        var roundedMultiple = RoundToValue(value, mode, false, towardZero, awayFromZero);

        return (towardZero, awayFromZero, isExact, roundedMultiple);
      }

      public static (TNumber MultipleTowardZero, TNumber MultipleAwayFromZero, bool IsExactMultiple, TNumber RoundedMultiple) RoundToMultiple(TNumber value, TNumber multiple, NearestRoundingRule rule = NearestRoundingRule.ToEven, bool strict = false)
      {
        var (towardZero, awayFromZero, isExact) = GetClosestMultiples(value, multiple, strict);

        var roundedMultiple = RoundToValue(value, rule, false, towardZero, awayFromZero);

        return (towardZero, awayFromZero, isExact, roundedMultiple);
      }

      #endregion

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

      #region RoundToValue

      public static TNumber RoundToValue(TNumber value, DirectedRoundingMode mode, bool strict, TNumber value1, TNumber value2)
      {
        var minValue = TNumber.Min(value1, value2);
        var maxValue = TNumber.Max(value1, value2);

        if (strict)
        {
          if (minValue == value)
            return maxValue;

          if (maxValue == value)
            return minValue;
        }

        var minDistance = TNumber.Abs(value - minValue);
        var maxDistance = TNumber.Abs(value - maxValue);

        return mode switch
        {
          DirectedRoundingMode.TowardNegativeInfinity => minValue,
          DirectedRoundingMode.TowardPositiveInfinity => maxValue,
          DirectedRoundingMode.TowardZero => TNumber.MinMagnitude(minValue, maxValue),
          DirectedRoundingMode.AwayFromZero => TNumber.MaxMagnitude(minValue, maxValue),
          DirectedRoundingMode.Random => System.Random.Shared.Next(2) == 0 ? minValue : maxValue,
          DirectedRoundingMode.Stochastic => (minDistance + maxDistance) is var distance && TNumber.IsZero(distance) ? value : System.Random.Shared.NextDouble() < double.CreateChecked(minDistance) / double.CreateChecked(distance) ? maxValue : minValue,
          _ => throw new NotImplementedException(nameof(mode)),
        };
      }

      public static TNumber RoundToValue(TNumber value, NearestRoundingRule rule, bool strict, TNumber value1, TNumber value2)
      {
        var minValue = TNumber.Min(value1, value2);
        var maxValue = TNumber.Max(value1, value2);

        if (strict)
        {
          if (minValue == value)
            return maxValue;

          if (maxValue == value)
            return minValue;
        }

        var minDistance = TNumber.Abs(value - minValue);
        var maxDistance = TNumber.Abs(value - maxValue);

        if (minDistance < maxDistance)
          return minValue;

        if (maxDistance < minDistance)
          return maxValue;

        return rule switch // Tie-breaking rules for equidistant candidates.
        {
          NearestRoundingRule.TowardNegativeInfinity => minValue,
          NearestRoundingRule.TowardPositiveInfinity => maxValue,
          NearestRoundingRule.TowardZero => TNumber.MinMagnitude(minValue, maxValue),
          NearestRoundingRule.AwayFromZero => TNumber.MaxMagnitude(minValue, maxValue),
          NearestRoundingRule.ToEven => TNumber.IsEvenInteger(minValue) ? minValue : TNumber.IsEvenInteger(maxValue) ? maxValue : throw new System.ArgumentException("No value is even."),
          NearestRoundingRule.ToOdd => TNumber.IsOddInteger(minValue) ? minValue : TNumber.IsOddInteger(maxValue) ? maxValue : throw new System.ArgumentException("No value is odd."),
          NearestRoundingRule.Random => System.Random.Shared.Next(2) == 0 ? minValue : maxValue,
          _ => throw new NotImplementedException(nameof(rule))
        };
      }

      public static TNumber RoundToValue(TNumber value, NearestRoundingRule rule, TNumber[] values)
      {
        values.ThrowOnNullOrEmpty(nameof(values));

        var minDistance = values.Min(c => TNumber.Abs(value - c));

        var ties = values.Where(c => TNumber.Abs(value - c) == minDistance).ToArray();

        if (ties.Length == 1)
          return ties[0];

        return rule switch
        {
          NearestRoundingRule.TowardNegativeInfinity => ties.Min()!,
          NearestRoundingRule.TowardPositiveInfinity => ties.Max()!,
          NearestRoundingRule.TowardZero => ties.MinBy(t => TNumber.Abs(t))!,
          NearestRoundingRule.AwayFromZero => ties.MaxBy(t => TNumber.Abs(t))!,
          NearestRoundingRule.ToEven => ties.Single(TNumber.IsEvenInteger),
          NearestRoundingRule.ToOdd => ties.Single(TNumber.IsOddInteger),
          NearestRoundingRule.Random => ties.GetRandomElement(),
          _ => throw new System.NotImplementedException(nameof(rule))
        };
      }

      #endregion
    }
  }
}
