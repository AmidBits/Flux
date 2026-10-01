namespace Flux
{
  public static partial class BinaryInteger
  {
    extension<TInteger>(TInteger)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      #region RoundToPower

      /// <summary>
      /// <para>Rounds value to a power-of-radix 2-tuple.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="radix"></param>
      /// <param name="unequal"></param>
      /// <param name="rule"></param>
      /// <returns>
      /// <para>(<typeparamref name="TInteger"/> PowerTowardZero, <typeparamref name="TInteger"/> PowerAwayFromZero, <see langword="bool"/> IsExactPower, <typeparamref name="TInteger"/> NearestPower)</para>
      /// <list type="bullet">
      /// <item>PowerTowardZero - a power-of-<paramref name="radix"/> less-than-(or-equal) to <paramref name="value"/> <i>(depends on <paramref name="unequal"/>)</i></item>
      /// <item>PowerAwayFromZero - a power-of-<paramref name="radix"/> greater-than-(or-equal) to <paramref name="value"/> <i>(depends on <paramref name="unequal"/>)</i></item>
      /// <item>IsExactPower - indicates whether <paramref name="value"/> is an exact power-of-<paramref name="radix"/></item>
      /// <item>NearestPower - the power-of-<paramref name="radix"/> nearest to <paramref name="value"/></item>
      /// </list>
      /// </returns>
      public static (TInteger PowerTowardZero, TInteger PowerAwayFromZero, bool IsExactPower, TInteger NearestPower) RoundToPower(TInteger value, TInteger radix, bool unequal = false, NearestRoundingRule rule = NearestRoundingRule.ToEven)
      {
        TInteger powerTowardZero; TInteger powerAwayFromZero; bool isExactPower; TInteger nearestPower;

        System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radix, TInteger.One);

        if (TInteger.IsZero(value))
          return (TInteger.Zero, TInteger.Zero, false, TInteger.Zero); // Zero is the result.
        else if (TInteger.IsNegative(value))
        {
          (powerTowardZero, powerAwayFromZero, isExactPower, nearestPower) = RoundToPower(TInteger.Abs(value), radix, unequal, rule); // Recursive call with abs(value).

          return (-powerTowardZero, -powerAwayFromZero, isExactPower, -nearestPower); // Negate all values.
        }

        powerAwayFromZero = TInteger.One;
        while (powerAwayFromZero < value) // Find the smallest power-of-radix that is greater than value.
          powerAwayFromZero *= radix;

        isExactPower = powerAwayFromZero == value; // Whether the original value is a power-of-radix.

        powerTowardZero = isExactPower
          ? powerAwayFromZero // If value is a power-of-radix, then powerTowardZero equals powerAwayFromZero.
          : powerAwayFromZero / radix; // Otherwise powerTowardZero is the next lower power-of-radix.

        if (isExactPower && unequal) // This is the only place where the unequal is handled.
        {
          powerTowardZero /= radix;
          powerAwayFromZero *= radix;
        }

        nearestPower = isExactPower
          ? powerTowardZero
          : Number.RoundToNearestValue(value, rule, false, powerTowardZero, powerAwayFromZero); // Find the nearest of the two.

        return (powerTowardZero, powerAwayFromZero, isExactPower, nearestPower);
      }

      #endregion
    }
  }

  public static partial class FloatingPoint
  {
    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      #region RoundToInteger

      /// <summary>
      /// <para>Rounds value to an integer 2-tuple.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="unequal"></param>
      /// <param name="rule"></param>
      /// <returns>
      /// <para>(<typeparamref name="TFloat"/> IntegerTowardZero, <typeparamref name="TFloat"/> IntegerAwayFromZero, <see langword="bool"/> IsExactInteger, <typeparamref name="TFloat"/> NearestInteger)</para>
      /// <list type="bullet">
      /// <item>IntegerTowardZero - an integer less-than-(or-equal) to <paramref name="value"/> <i>(depends on <paramref name="unequal"/>)</i></item>
      /// <item>IntegerAwayFromZero - an integer greater-than-(or-equal) to <paramref name="value"/> <i>(depends on <paramref name="unequal"/>)</i></item>
      /// <item>IsExactInteger - indicates whether <paramref name="value"/> is an exact integer</item>
      /// <item>NearestInteger - the integer nearest to <paramref name="value"/></item>
      /// </list>
      /// </returns>
      public static (TFloat IntegerTowardZero, TFloat IntegerAwayFromZero, bool IsExactInteger, TFloat NearestInteger) RoundToInteger(TFloat value, bool unequal, NearestRoundingRule rule = NearestRoundingRule.ToEven)
      {
        TFloat integerTowardZero; TFloat integerAwayFromZero; bool isExactInteger; TFloat nearestInteger;

        // We don't want immediate return on TFloat.IsInteger(value) because that disables the feature of unequal.

        if (TFloat.IsNegative(value))
        {
          (integerTowardZero, integerAwayFromZero, isExactInteger, nearestInteger) = RoundToInteger(TFloat.Abs(value), unequal, rule); // Recursive call with abs(value).

          return (-integerTowardZero, -integerAwayFromZero, isExactInteger, -nearestInteger); // Negate all values.
        }

        integerTowardZero = TFloat.Truncate(value);

        isExactInteger = integerTowardZero == value;

        integerAwayFromZero = isExactInteger ? integerTowardZero : integerTowardZero + TFloat.One;

        if (isExactInteger && unequal) // This is the only place where the unequal is handled.
        {
          integerTowardZero -= TFloat.One;
          integerAwayFromZero += TFloat.One;
        }

        nearestInteger = Number.RoundToNearestValue(value, rule, false, integerTowardZero, integerAwayFromZero);

        return (integerTowardZero, integerAwayFromZero, isExactInteger, nearestInteger);
      }

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

      /// <summary>
      /// <para>Rounds value to a multiple 2-tuple.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="multiple"></param>
      /// <param name="unequal"></param>
      /// <param name="rule"></param>
      /// <returns>
      /// <para>(<typeparamref name="TNumber"/> MultipleTowardZero, <typeparamref name="TNumber"/> MultipleAwayFromZero, <see langword="bool"/> IsExactMultiple, <typeparamref name="TNumber"/> NearestMultiple)</para>
      /// <list type="bullet">
      /// <item>MultipleTowardZero - a <paramref name="multiple"/> less-than-(or-equal) to <paramref name="value"/> <i>(depends on <paramref name="unequal"/>)</i></item>
      /// <item>MultipleAwayFromZero - a <paramref name="multiple"/> greater-than-(or-equal) to <paramref name="value"/> <i>(depends on <paramref name="unequal"/>)</i></item>
      /// <item>IsExactMultiple - indicates whether <paramref name="value"/> is an exact <paramref name="multiple"/></item>
      /// <item>NearestMultiple - the <paramref name="multiple"/> nearest to <paramref name="value"/></item>
      /// </list>
      /// </returns>
      public static (TNumber MultipleTowardZero, TNumber MultipleAwayFromZero, bool IsExactMultiple, TNumber NearestMultiple) RoundToMultiple(TNumber value, TNumber multiple, bool unequal = false, NearestRoundingRule rule = NearestRoundingRule.ToEven)
      {
        TNumber multipleTowardZero; TNumber multipleAwayFromZero; bool isExactMultiple; TNumber nearestMultiple;

        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(multiple);

        if (TNumber.IsZero(value))
          return (TNumber.Zero, TNumber.Zero, false, TNumber.Zero); // Zero is the result.
        else if (TNumber.IsNegative(value))
        {
          (multipleTowardZero, multipleAwayFromZero, isExactMultiple, nearestMultiple) = RoundToMultiple(TNumber.Abs(value), multiple, unequal, rule); // Recursive call with abs(value).

          return (-multipleTowardZero, -multipleAwayFromZero, isExactMultiple, -nearestMultiple); // Negate all values.
        }

        var (quotient, remainder) = IntegerDivRemTruncated(value, multiple); // Truncated division works on negative numbers for quotient which is used for multipleTowardZero.

        multipleTowardZero = quotient * multiple;

        isExactMultiple = TNumber.IsZero(remainder); // Whether the original value is a multiple.

        multipleAwayFromZero = isExactMultiple ? multipleTowardZero : multipleTowardZero + multiple;

        if (isExactMultiple && unequal) // This is the only place where the unequal is handled.
        {
          multipleTowardZero -= multiple;
          multipleAwayFromZero += multiple;
        }

        nearestMultiple = RoundToNearestValue(value, rule, false, multipleTowardZero, multipleAwayFromZero); // Find the nearest of the two.

        return (multipleTowardZero, multipleAwayFromZero, isExactMultiple, nearestMultiple);
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
