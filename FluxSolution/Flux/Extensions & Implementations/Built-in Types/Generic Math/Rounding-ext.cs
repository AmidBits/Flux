namespace Flux
{
  public static partial class BinaryInteger
  {
    extension<TInteger>(TInteger)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      #region GetNearestPowers

      /// <summary>
      /// <para>Computes the nearest powers of <paramref name="radix"/> toward and away from zero, determines whether the value is an exact power, and selects the nearest power according to the specified rounding rule.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="radix"></param>
      /// <param name="strict">If <see langword="true"/> and <paramref name="value"/> is an exact power-of-radix, the returned toward-zero and away-from-zero powers are adjusted to be strictly different from <paramref name="value"/>.</param>
      /// <param name="rule"></param>
      /// <returns>
      /// <para>(<typeparamref name="TInteger"/> PowerTowardZero, <typeparamref name="TInteger"/> PowerAwayFromZero, <see langword="bool"/> IsExactPower, <typeparamref name="TInteger"/> NearestPower)</para>
      /// <list type="bullet">
      /// <item>NearestPowerTowardZero - the nearest power of <paramref name="radix"/> in the direction of zero.</item>
      /// <item>NearestPowerAwayFromZero - the nearest power of <paramref name="radix"/> in the direction away from zero.</item>
      /// <item>IsExactPower - indicates whether <paramref name="value"/> is an exact power of <paramref name="radix"/>.</item>
      /// <item>NearestPowerByRule - the nearest power of <paramref name="radix"/> according to <paramref name="rule"/>.</item>
      /// </list>
      /// </returns>
      public static (TInteger NearestPowerTowardZero, TInteger NearestPowerAwayFromZero, bool IsExactPower, TInteger NearestPowerByRule) GetNearestPowers(TInteger value, TInteger radix, bool strict = false, NearestRoundingRule rule = NearestRoundingRule.ToEven)
      {
        TInteger nearestPowerTowardZero; TInteger nearestPowerAwayFromZero; bool isExactPower; TInteger nearestPowerByRule;

        System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radix, TInteger.One);

        if (TInteger.IsZero(value))
          return (TInteger.Zero, TInteger.Zero, false, TInteger.Zero); // Zero is the result.
        else if (TInteger.IsNegative(value))
        {
          (nearestPowerTowardZero, nearestPowerAwayFromZero, isExactPower, nearestPowerByRule) = GetNearestPowers(TInteger.Abs(value), radix, strict, rule); // Recursive call with abs(value).

          return (-nearestPowerTowardZero, -nearestPowerAwayFromZero, isExactPower, -nearestPowerByRule); // Negate all values.
        }

        nearestPowerAwayFromZero = TInteger.One;
        while (nearestPowerAwayFromZero < value) // Find the smallest power-of-radix that is greater than value.
          nearestPowerAwayFromZero = checked(nearestPowerAwayFromZero * radix);

        isExactPower = nearestPowerAwayFromZero == value; // Whether the original value is a power-of-radix.

        nearestPowerTowardZero = isExactPower
          ? nearestPowerAwayFromZero // If value is a power-of-radix, then powerTowardZero equals powerAwayFromZero.
          : nearestPowerAwayFromZero / radix; // Otherwise powerTowardZero is the next lower power-of-radix.

        if (isExactPower && strict) // This is the only place where the unequal is handled.
        {
          nearestPowerTowardZero /= radix;
          nearestPowerAwayFromZero = checked(nearestPowerAwayFromZero * radix);
        }

        nearestPowerByRule = isExactPower
          ? nearestPowerTowardZero
          : Number.RoundToNearestValue(value, rule, false, nearestPowerTowardZero, nearestPowerAwayFromZero); // Find the nearest of the two.

        return (nearestPowerTowardZero, nearestPowerAwayFromZero, isExactPower, nearestPowerByRule);
      }

      #endregion
    }
  }

  public static partial class FloatingPoint
  {
    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      #region GetNearestIntegers

      /// <summary>
      /// <para>Computes the nearest integers in the directions toward and away from zero, determines whether the <paramref name="value"/> is an exact integer, and selects the nearest integer according to the specified rounding <paramref name="rule"/>.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="strict">If <see langword="true"/> and <paramref name="value"/> is an exact integer, the returned toward-zero and away-from-zero integers are adjusted to be strictly different from <paramref name="value"/>.</param>      
      /// <param name="rule"></param>
      /// <returns>
      /// <list type="bullet">
      /// <item>NearestIntegerTowardZero - the nearest integer to <paramref name="value"/> in the direction of zero (equivalent to truncation).</item>
      /// <item>NearestIntegerAwayFromZero - the nearest integer to <paramref name="value"/> in the direction away from zero.</item>
      /// <item>IsExactInteger - indicates whether <paramref name="value"/> is an exact integer.</item>
      /// <item>NearestIntegerByRule - the integer nearest to <paramref name="value"/> according to <paramref name="rule"/>.</item>
      /// </list>
      /// </returns>
      public static (TFloat NearestIntegerTowardZero, TFloat NearestIntegerAwayFromZero, bool IsExactInteger, TFloat NearestIntegerByRule) GetNearestIntegers(TFloat value, bool strict, NearestRoundingRule rule = NearestRoundingRule.ToEven)
      {
        TFloat nearestIntegerTowardZero; TFloat nearestIntegerAwayFromZero; bool isExactInteger; TFloat nearestIntegerByRule;

        // We don't want immediate return on TFloat.IsInteger(value) because that disables the feature of unequal.

        if (TFloat.IsNegative(value))
        {
          (nearestIntegerTowardZero, nearestIntegerAwayFromZero, isExactInteger, nearestIntegerByRule) = GetNearestIntegers(TFloat.Abs(value), strict, rule); // Recursive call with abs(value).

          return (-nearestIntegerTowardZero, -nearestIntegerAwayFromZero, isExactInteger, -nearestIntegerByRule); // Negate all values.
        }

        nearestIntegerTowardZero = TFloat.Truncate(value);

        isExactInteger = nearestIntegerTowardZero == value;

        nearestIntegerAwayFromZero = isExactInteger ? nearestIntegerTowardZero : nearestIntegerTowardZero + TFloat.One;

        if (isExactInteger && strict) // In strict mode, exact integers are adjusted to their neighboring integers.
        {
          nearestIntegerTowardZero -= TFloat.One;
          nearestIntegerAwayFromZero += TFloat.One;
        }

        nearestIntegerByRule = Number.RoundToNearestValue(value, rule, false, nearestIntegerTowardZero, nearestIntegerAwayFromZero);

        return (nearestIntegerTowardZero, nearestIntegerAwayFromZero, isExactInteger, nearestIntegerByRule);
      }

      #endregion

      #region RoundToInteger

      /// <summary>
      /// <para>Rounds a floating-point value to the nearest integer according to the specified directed rounding mode.</para>
      /// </summary>
      /// <param name="value">The value to round.</param>
      /// <param name="mode">The directed rounding mode to use.</param>
      /// <returns>The rounded integer value.</returns>
      /// <exception cref="System.NotImplementedException">Thrown when the specified rounding mode is not supported.</exception>
      public static TFloat RoundToInteger(TFloat value, DirectedRoundingMode mode)
        => mode switch
        {
          DirectedRoundingMode.TowardNegativeInfinity => TFloat.Floor(value),
          DirectedRoundingMode.TowardPositiveInfinity => TFloat.Ceiling(value),
          DirectedRoundingMode.TowardZero => TFloat.Truncate(value),
          DirectedRoundingMode.AwayFromZero => Envelop(value),
          _ => throw new System.NotImplementedException(nameof(mode)),
        };

      #endregion

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
            NearestRoundingRule.TowardZero => TFloat.Truncate(value), // TFloat.IsNegative(value) ? ceiling : floor,
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
      public static TFloat RoundByPrecision<TRadix>(TFloat value, NearestRoundingRule nearestRoundingTies, int significantDigits, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(significantDigits);
        System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radix, TRadix.One);

        var scalar = TFloat.Pow(TFloat.CreateChecked(radix), TFloat.CreateChecked(significantDigits));

        return RoundToNearestInteger(value * scalar, nearestRoundingTies) / scalar;
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
      /// <para>Equivalent to the opposite effect of the Truncate() functionality, i.e. instead of truncating the fraction and essentially executing a round-toward-zero, envelop the fraction and essentially execute a round-away-from-zero.</para>
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
      public TFloat Quantize(TFloat step, DirectedRoundingMode mode)
        => RoundToInteger(value / step, mode) * step;

      /// <summary>
      /// <para>Quantizes a value by snapping it to a multiple of <paramref name="step"/> according to the specified nearest rounding <paramref name="rule"/>.</para>
      /// </summary>
      /// <remarks>Equivalent to: <c>RoundToNearestInteger(<paramref name="value"/> / <paramref name="step"/>, <paramref name="rule"/>) * <paramref name="step"/></c></remarks>
      /// <param name="step"></param>
      /// <param name="rule"></param>
      /// <returns></returns>
      public TFloat Quantize(TFloat step, NearestRoundingRule rule)
        => RoundToNearestInteger(value / step, rule) * step;

      #endregion
    }
  }

  public static partial class Number
  {
    extension<TNumber>(TNumber)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      #region GetNearestMultiples

      /// <summary>
      /// <para>Computes the nearest multiples of <paramref name="multiple"/> toward and away from zero, determines whether the value is an exact multiple, and selects the nearest multiple according to the specified rounding rule.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="multiple"></param>
      /// <param name="strict">If <see langword="true"/> and <paramref name="value"/> is an exact multiple of <paramref name="multiple"/>, the returned toward-zero and away-from-zero multiples are adjusted to be strictly different from <paramref name="value"/>.</param>
      /// <param name="rule"></param>
      /// <returns>
      /// <para>(<typeparamref name="TNumber"/> MultipleTowardZero, <typeparamref name="TNumber"/> MultipleAwayFromZero, <see langword="bool"/> IsExactMultiple, <typeparamref name="TNumber"/> NearestMultiple)</para>
      /// <list type="bullet">
      /// <item>NearestMultipleTowardZero - the nearest multiple of <paramref name="multiple"/> in the direction of zero.</item>
      /// <item>NearestMultipleAwayFromZero - the nearest multiple of <paramref name="multiple"/> in the direction away from zero.</item>
      /// <item>IsExactMultiple - indicates whether <paramref name="value"/> is an exact multiple of <paramref name="multiple"/>.</item>
      /// <item>NearestMultipleByRule - the nearest multiple of <paramref name="multiple"/> according to <paramref name="rule"/>.</item>
      /// </list>
      /// </returns>
      public static (TNumber NearestMultipleTowardZero, TNumber NearestMultipleAwayFromZero, bool IsExactMultiple, TNumber NearestMultipleByRule) GetNearestMultiples(TNumber value, TNumber multiple, bool strict = false, NearestRoundingRule rule = NearestRoundingRule.ToEven)
      {
        TNumber nearestMultipleTowardZero; TNumber nearestMultipleAwayFromZero; bool isExactMultiple; TNumber nearestMultipleByRule;

        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(multiple);

        if (TNumber.IsZero(value))
          return (TNumber.Zero, TNumber.Zero, true, TNumber.Zero); // Zero is the result.
        else if (TNumber.IsNegative(value))
        {
          (nearestMultipleTowardZero, nearestMultipleAwayFromZero, isExactMultiple, nearestMultipleByRule) = GetNearestMultiples(TNumber.Abs(value), multiple, strict, rule); // Recursive call with abs(value).

          return (-nearestMultipleTowardZero, -nearestMultipleAwayFromZero, isExactMultiple, -nearestMultipleByRule); // Negate all values.
        }

        var (quotient, remainder) = IntegerDivRemTruncated(value, multiple); // Truncated division works on negative numbers for quotient which is used for multipleTowardZero.

        nearestMultipleTowardZero = checked(quotient * multiple);

        isExactMultiple = TNumber.IsZero(remainder); // Whether the original value is a multiple.

        nearestMultipleAwayFromZero = isExactMultiple ? nearestMultipleTowardZero : nearestMultipleTowardZero + multiple;

        if (isExactMultiple && strict) // This is the only place where strict mode is handled.
        {
          nearestMultipleTowardZero -= multiple;
          nearestMultipleAwayFromZero = checked(nearestMultipleAwayFromZero + multiple);
        }

        nearestMultipleByRule = RoundToNearestValue(value, rule, false, nearestMultipleTowardZero, nearestMultipleAwayFromZero); // Find the nearest of the two.

        return (nearestMultipleTowardZero, nearestMultipleAwayFromZero, isExactMultiple, nearestMultipleByRule);
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

      #region RoundToMultiple

      public static TNumber RoundToMultiple(TNumber value, TNumber multiple, DirectedRoundingMode mode)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(multiple);

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

      #region RoundToNearestValue

      public static TNumber RoundToNearestValue(TNumber value, NearestRoundingRule rule, bool proper, params System.ReadOnlySpan<TNumber> values)
      {
        System.ArgumentOutOfRangeException.ThrowIfZero(values.Length);

        var closestValues = new System.Collections.Generic.HashSet<TNumber>();
        var closestDistance = TNumber.Abs(value - values[0]);

        for (var i = 0; i < values.Length; i++)
        {
          var currentValue = values[i];
          var currentDistance = TNumber.Abs(value - currentValue);

          if ((!proper || currentValue != value) && (currentDistance <= closestDistance))
          {
            if (currentDistance < closestDistance)
            {
              closestValues.Clear();
              closestDistance = currentDistance;
            }

            closestValues.Add(currentValue);
          }
        }

        return (closestValues.Count > 1) // If multiple values were found, i.e. the distances are equal, or in other words the value is exactly halfway to all closestValues, we use the nearest rounding rule to resolve a winner.
          ? rule switch
          {
            NearestRoundingRule.TowardNegativeInfinity => closestValues.Min()!,
            NearestRoundingRule.TowardPositiveInfinity => closestValues.Max()!,
            NearestRoundingRule.TowardZero => closestValues.InfimumSupremum(value, v => v, false) is var (_, _, _, _, infIndex, infValue, _, supIndex, supValue, _, _, _) && value >= TNumber.Zero ? (infIndex > -1 ? infValue! : supValue!) : (supIndex > -1 ? supValue! : infValue!),
            NearestRoundingRule.AwayFromZero => closestValues.InfimumSupremum(value, v => v, false) is var (_, _, _, _, infIndex, infValue, _, supIndex, supValue, _, _, _) && value >= TNumber.Zero ? (supIndex > -1 ? supValue! : infValue!) : (infIndex > -1 ? infValue! : supValue!),
            NearestRoundingRule.ToEven => closestValues.FirstOrValue(closestValues.First(), TNumber.IsEvenInteger).Item,
            NearestRoundingRule.ToOdd => closestValues.FirstOrValue(closestValues.First(), TNumber.IsOddInteger).Item,
            NearestRoundingRule.Random => closestValues.GetRandomElement(),
            _ => throw new System.NotImplementedException(nameof(rule)),
          }
          : closestValues.First(); // Only one that is closest.
      }

      #endregion
    }
  }
}
