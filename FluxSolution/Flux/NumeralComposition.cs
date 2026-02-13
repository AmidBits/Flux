namespace Flux
{
  /// <summary>
  /// <para>Supporting class for breaking down and translating numbers to strings.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Cardinal_numeral"/></para>
  /// <para><see href="https://en.wikipedia.org/wiki/English_numerals"/></para>
  /// </summary>
  public static partial class NumeralComposition
  {
    extension<TInteger>(TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      /// <summary>
      /// <para>Creates a new string (optionally specify a <paramref name="stringBuilder"/> to use) with english numerals representing an integer value.</para>
      /// <para>E.g. 670530 = "<c>Six Hundred Seventy Thousand Five Hundred Thirty</c>"</para>
      /// </summary>
      /// <param name="stringBuilder">Optional. If not provided an internal instance will be used.</param>
      /// <param name="numeralScale">The scale to use.</param>
      /// <returns></returns>
      public string ToCardinalNumeralString(System.Text.StringBuilder? stringBuilder = null, NumeralScale numeralScale = NumeralScale.ShortScale)
      {
        stringBuilder ??= new System.Text.StringBuilder();

        var cardinalNumerals = GetCardinalNumerals(value, out var compoundNumbers, numeralScale);

        if (compoundNumbers.Count > 0)
        {
          var previousNumber = compoundNumbers[0];

          if (previousNumber.IsZero)
            stringBuilder.Append(cardinalNumerals[0]);

          for (var i = 1; i < compoundNumbers.Count; i++)
          {
            var currentNumber = compoundNumbers[i];
            var currentNumeral = cardinalNumerals[i];

            if (stringBuilder.Length > 0)
            {
              if (currentNumber >= 1 && currentNumber <= 9 && previousNumber >= 20 && previousNumber <= 90)
                stringBuilder.Append('-'); // Add a hyphen between ANY tens and ones.
              else
                stringBuilder.Append(' '); // Otherwise add a space.
            }

            stringBuilder.Append(currentNumeral);

            previousNumber = currentNumber;
          }
        }

        return stringBuilder.ToString();
      }

      /// <summary>
      /// <para>Creates a new string (optionally specify a <paramref name="stringBuilder"/> to use) with english numerals representing an integer value.</para>
      /// <para>E.g. 2039 = "<c>Two Thousand Thirty-Ninth</c>"</para>
      /// </summary>
      /// <param name="stringBuilder">Optional. If not provided an internal instance will be used.</param>
      /// <param name="numeralScale">The scale to use.</param>
      /// <returns></returns>
      public string ToOrdinalNumeralString(System.Text.StringBuilder? stringBuilder = null, NumeralScale numeralScale = NumeralScale.ShortScale)
      {
        stringBuilder ??= new System.Text.StringBuilder();

        var ordinalNumerals = GetOrdinalNumerals(value, out var compoundNumbers, numeralScale);

        if (compoundNumbers.Count > 0)
        {
          var previousNumber = compoundNumbers[0];

          if (previousNumber.IsZero)
            stringBuilder.Append(ordinalNumerals[0]);

          for (var i = 1; i < compoundNumbers.Count; i++)
          {
            var currentNumber = compoundNumbers[i];
            var currentNumeral = ordinalNumerals[i];

            if (stringBuilder.Length > 0)
            {
              if (currentNumber >= 1 && currentNumber <= 9 && previousNumber >= 20 && previousNumber <= 90)
                stringBuilder.Append('-'); // Add a hyphen between ANY tens and ones.
              else
                stringBuilder.Append(' '); // Otherwise add a space.
            }

            stringBuilder.Append(currentNumeral);

            previousNumber = currentNumber;
          }
        }

        return stringBuilder.ToString();
      }
    }

    extension<TFloat>(TFloat value)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      /// <summary>
      /// <para>Creates a new string (optionally supply a <paramref name="stringBuilder"/> to use) with english numerals representing a floating-point value.</para>
      /// <para>20.39 = "<c>Twenty And Thirty-Nine Hundredths</c>"</para>
      /// </summary>
      /// <param name="stringBuilder"></param>
      /// <param name="numeralScale">The scale to use.</param>
      /// <param name="numeralDecimalSeparator">The numeral separating the integral and fractional part.</param>
      /// <param name="includeScaleFactor">Indicate whether to include a scale factor.</param>
      /// <returns></returns>
      public string ToCardinalNumeralString(System.Text.StringBuilder? stringBuilder = null, NumeralScale numeralScale = NumeralScale.ShortScale, string numeralDecimalSeparator = "And", bool includeScaleFactor = true)
      {
        stringBuilder ??= new System.Text.StringBuilder();

        if (value is decimal decimalValue)
        {
          var (integerPart, _, fractionalPartAsWholeNumber) = decimal.GetParts(decimalValue, out var _, out var _, out var scaleFactor, out var _);

          if (!integerPart.IsZero)
          {
            ToCardinalNumeralString(integerPart, stringBuilder, numeralScale);

            if (!fractionalPartAsWholeNumber.IsZero)
            {
              stringBuilder.Append(' ');

              stringBuilder.Append(numeralDecimalSeparator);
            }
          }

          if (!fractionalPartAsWholeNumber.IsZero)
          {
            ToCardinalNumeralString(fractionalPartAsWholeNumber, stringBuilder, numeralScale);

            if (includeScaleFactor)
            {
              var sb = new System.Text.StringBuilder();

              ToCardinalNumeralString(scaleFactor, sb, numeralScale);

              sb.Replace(' ', '-');

              sb.TrimCommonPrefix(0, "One-");

              sb.Append(BinaryInteger.GetOrdinalIndicatorSuffix(0));

              if (Flux.Number.IsConsideredPlural(fractionalPartAsWholeNumber))
                sb.Append('s');

              stringBuilder.Append(' ');
              stringBuilder.Append(sb);
            }
          }
        }
        else // It's another type (than decimal) of floating point, so let's convert it and call again.
          ToCardinalNumeralString(decimal.CreateChecked(value), stringBuilder, numeralScale, numeralDecimalSeparator);

        return stringBuilder.TrimCommonSuffix(0, char.IsWhiteSpace).ToString();
      }
    }

    #region CardinalNumerals

    public static System.Collections.Generic.List<string> GetCardinalNumerals<TInteger>(TInteger value, out System.Collections.Generic.List<System.Numerics.BigInteger> compoundNumbers, NumeralScale numeralScale = NumeralScale.ShortScale)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      compoundNumbers = GetCompoundNumbers(value, GetExtremum(numeralScale).MaxNumber);

      var cardinalNumerals = compoundNumbers.Skip(1).Select(cn => GetNumeral(cn, numeralScale)).Prepend(GetSignString(compoundNumbers[0])).ToList();

      return cardinalNumerals;
    }

    #endregion

    #region CompundNumbers

    public static string GetSignString(System.Numerics.BigInteger compoundNumberSign)
      => compoundNumberSign.IsZero ? CommonScaleDictionary.Zero.ToString() : compoundNumberSign.Sign < 0 ? "Negative" : string.Empty;

    /// <summary>
    /// <para>Creates a new sequence of compound numbers by breaking down the specified <paramref name="value"/>. The sequence will yield no larger value than <paramref name="maxCutoff"/>.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value"></param>
    /// <param name="maxCutoff">No values larger than this will appear. This is the maximum compound value.</param>
    /// <returns></returns>
    public static System.Collections.Generic.List<System.Numerics.BigInteger> GetCompoundNumbers<TInteger>(TInteger value, System.Numerics.BigInteger maxCutoff)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => [.. GetCompoundNumbers(System.Numerics.BigInteger.Abs(System.Numerics.BigInteger.CreateChecked(value)), maxCutoff).Prepend(System.Numerics.BigInteger.CreateChecked(Flux.Number.Sign(value)))];

    private static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetCompoundNumbers(System.Numerics.BigInteger absNumber, System.Numerics.BigInteger maxCutoff)
    {
      if (absNumber >= 100) // If the number is GTE 100, do it.
        for (var bi = maxCutoff; bi >= 100; bi /= (bi > 1000 ? 1000 : 10))
          if (absNumber >= bi) // If the number is greater or equal to the place value (e.g. 7,nnn,nnn compared to 1,000,000).
          {
            var quotient = absNumber / bi; // Compute the cardinal number for the place value (e.g. 7,nnn,nnn / 1,000,000 = 7).

            foreach (var qn in GetCompoundNumbers(quotient, maxCutoff)) // Recursively process the cardinal number (e.g. 7).
              yield return qn;

            yield return bi; // Add the place value used above (e.g. 1,000,000).

            absNumber -= quotient * bi; // Set number by subtracting the values just processed (e.g. 7 * 1,000,000 = 7,000,000) and continue.
          }

      if (absNumber >= 20) // If the number is GTE 20 (it's less than 100 at this point), do it.
      {
        var remainder = absNumber % 10; // Compute the cardinal number for the place value (e.g. 72 % 10 = 2).

        yield return absNumber - remainder; // Add the place value by subtracting the remainder (e.g. 72 - 2 = 70).

        absNumber = remainder; // Set number to remainder (e.g. 2) and continue down.
      }

      if (absNumber > 0) // If the number is GT 0 (it's less than 20 at this point), simply add it.
        yield return absNumber; // Add the number (e.g. 2).
    }

    #endregion

    #region OrdinalNumerals

    public static System.Collections.Generic.List<string> GetOrdinalNumerals<TInteger>(TInteger value, out System.Collections.Generic.List<System.Numerics.BigInteger> compoundNumbers, NumeralScale numeralScale = NumeralScale.ShortScale)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      var (minNumber, minNumeral, maxNumber, maxNumeral) = GetExtremum(numeralScale);

      compoundNumbers = GetCompoundNumbers(value, maxNumber);

      var ordinalNumerals = compoundNumbers.Skip(1).Select(cn => GetNumeral(cn, numeralScale)).Prepend(GetSignString(compoundNumbers[0])).ToList();

      var lastNumber = compoundNumbers[^1];

      if (lastNumber < minNumber)
        ordinalNumerals[^1] = ((CommonOrdinalNumerals)(int)lastNumber).ToString();
      else // lastNumber >= minNumber
      {
        ordinalNumerals[^1] = numeralScale switch
        {
          NumeralScale.LongScale => LongScaleDictionary[lastNumber] + BinaryInteger.GetOrdinalIndicatorSuffix(lastNumber),
          NumeralScale.ShortScale => ShortScaleDictionary[lastNumber] + BinaryInteger.GetOrdinalIndicatorSuffix(lastNumber),
          _ => throw new System.ArgumentOutOfRangeException(nameof(numeralScale)),
        };
      }

      return ordinalNumerals;
    }

    #endregion

    public static (System.Numerics.BigInteger MinNumber, string MinNumeral, System.Numerics.BigInteger MaxNumber, string MaxNumeral) GetExtremum(NumeralScale numeralScale)
    {
      switch (numeralScale)
      {
        case NumeralScale.CommonScale:
          {
            var (ExtremumMinElement, ExtremumMinIndex, ExtremumMinValue, ExtremumMaxElement, ExtremumMaxIndex, ExtremumMaxValue) = EnumExtensions.Extremum<CommonScaleDictionary>();

            return (ExtremumMinValue, ExtremumMinElement.ToString(), ExtremumMaxValue, ExtremumMaxElement.ToString());
          }
        case NumeralScale.LongScale:
          {
            var (MinElement, MinIndex, MinValue, MaxElement, MaxIndex, MaxValue) = LongScaleDictionary.Extremum(v => v.Key);

            return (MinValue, MinElement.Value, MaxValue, MaxElement.Value);
          }
        case NumeralScale.ShortScale:
          {
            var (MinElement, MinIndex, MinValue, MaxElement, MaxIndex, MaxValue) = ShortScaleDictionary.Extremum(v => v.Key);

            return (MinValue, MinElement.Value, MaxValue, MaxElement.Value);
          }
        default:
          throw new System.NotImplementedException();
      }
    }

    public static string GetNumeral<TInteger>(TInteger compoundNumber, NumeralScale namingScale)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      var abs = System.Numerics.BigInteger.Abs(System.Numerics.BigInteger.CreateChecked(compoundNumber));

      return namingScale switch
      {
        NumeralScale.CommonScale when abs <= GetExtremum(NumeralScale.CommonScale).MaxNumber => ((CommonScaleDictionary)(int)abs).ToString(),
        NumeralScale.LongScale when abs <= GetExtremum(NumeralScale.LongScale).MaxNumber => LongScaleDictionary.TryGetValue(abs, out var longScaleNumeral) ? longScaleNumeral : ((CommonScaleDictionary)(int)abs).ToString(),
        NumeralScale.ShortScale when abs <= GetExtremum(NumeralScale.ShortScale).MaxNumber => ShortScaleDictionary.TryGetValue(abs, out var shortScaleNumeral) ? shortScaleNumeral : ((CommonScaleDictionary)(int)abs).ToString(),
        _ => throw new System.NotImplementedException(),
      };
    }

    #region Data (long-scale & short-scale, common-scale and common-ordinal)

    public enum CommonOrdinalNumerals
    {
      Zeroth = 0,
      First,
      Second,
      Third,
      Fourth,
      Fifth,
      Sixth,
      Seventh,
      Eighth,
      Ninth,
      Tenth = 10,
      Eleventh,
      Twelveth,
      Thirteenth,
      Fourteenth,
      Fifteenth,
      Sixteenth,
      Seventeenth,
      Eighteenth,
      Nineteenth,
      Twentieth = 20,
      Thirtieth = 30,
      Fourtieth = 40,
      Fiftieth = 50,
      Sixtieth = 60,
      Seventieth = 70,
      Eightieth = 80,
      Ninetieth = 90,
      Hundredth = 100,
      Thousandth = 1000, // 1e3
      Millionth = 1000000, // 1e6
    }

    /// <summary>
    /// <para>Two naming scales for large numbers have been used in English and other European languages since the early modern era: the long and short scales. Most English variants use the short scale today, but the long scale remains dominant in many non-English-speaking areas, including continental Europe and Spanish-speaking countries in the Americas. These naming procedures are based on taking the number n occurring in 103n+3 (short scale) or 106n (long scale) and concatenating Latin roots for its units, tens, and hundreds place, together with the suffix -illion.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Names_of_large_numbers"/></para>
    /// </summary>
    public enum NumeralScale
    {
      /// <summary>
      /// <para>Only words up to a million are used.</para>
      /// </summary>
      CommonScale,

      /// <summary>
      /// <para>Usage: French Canada, older British, Western & Central Europe.</para>
      /// </summary>
      LongScale,

      /// <summary>
      /// <para>Usage: US, English Canada, modern British, Australia, and Eastern Europe.</para>
      /// </summary>
      ShortScale,
    }

    public enum CommonScaleDictionary
    {
      Zero = 0,
      One,
      Two,
      Three,
      Four,
      Five,
      Six,
      Seven,
      Eight,
      Nine,
      Ten = 10,
      Eleven,
      Twelve,
      Thirteen,
      Fourteen,
      Fifteen,
      Sixteen,
      Seventeen,
      Eighteen,
      Nineteen,
      Twenty = 20,
      Thirty = 30,
      Fourty = 40,
      Fifty = 50,
      Sixty = 60,
      Seventy = 70,
      Eighty = 80,
      Ninety = 90,
      Hundred = 100,
      Thousand = 1000, // 1e3
      Million = 1000000, // 1e6
    }

    public static System.Collections.Generic.IReadOnlyDictionary<System.Numerics.BigInteger, string> LongScaleDictionary
      => new System.Collections.Generic.Dictionary<System.Numerics.BigInteger, string>()
      {
        { System.Numerics.BigInteger.Parse("1e123", System.Globalization.NumberStyles.AllowExponent, null), "Vigintilliard" },
        { System.Numerics.BigInteger.Parse("1e120", System.Globalization.NumberStyles.AllowExponent, null), "Vigintillion" },
        { System.Numerics.BigInteger.Parse("1e117", System.Globalization.NumberStyles.AllowExponent, null), "Novendecilliard" },
        { System.Numerics.BigInteger.Parse("1e114", System.Globalization.NumberStyles.AllowExponent, null), "Novendecillion" },
        { System.Numerics.BigInteger.Parse("1e111", System.Globalization.NumberStyles.AllowExponent, null), "Octodecilliard" },
        { System.Numerics.BigInteger.Parse("1e108", System.Globalization.NumberStyles.AllowExponent, null), "Octodecillion" },
        { System.Numerics.BigInteger.Parse("1e105", System.Globalization.NumberStyles.AllowExponent, null), "Septendecilliard" },
        { System.Numerics.BigInteger.Parse("1e102", System.Globalization.NumberStyles.AllowExponent, null), "Septendecillion" },
        { System.Numerics.BigInteger.Parse("1e99", System.Globalization.NumberStyles.AllowExponent, null), "Sedecilliard" },
        { System.Numerics.BigInteger.Parse("1e96", System.Globalization.NumberStyles.AllowExponent, null), "Sedecillion" },
        { System.Numerics.BigInteger.Parse("1e93", System.Globalization.NumberStyles.AllowExponent, null), "Quindecilliard" },
        { System.Numerics.BigInteger.Parse("1e90", System.Globalization.NumberStyles.AllowExponent, null), "Quindecillion" },
        { System.Numerics.BigInteger.Parse("1e87", System.Globalization.NumberStyles.AllowExponent, null), "Quattuordecilliard" },
        { System.Numerics.BigInteger.Parse("1e84", System.Globalization.NumberStyles.AllowExponent, null), "Quattuordecillion" },
        { System.Numerics.BigInteger.Parse("1e81", System.Globalization.NumberStyles.AllowExponent, null), "Tredecilliard" },
        { System.Numerics.BigInteger.Parse("1e78", System.Globalization.NumberStyles.AllowExponent, null), "Tredecillion" },
        { System.Numerics.BigInteger.Parse("1e75", System.Globalization.NumberStyles.AllowExponent, null), "Duodecilliard" },
        { System.Numerics.BigInteger.Parse("1e72", System.Globalization.NumberStyles.AllowExponent, null), "Duodecillion" },
        { System.Numerics.BigInteger.Parse("1e69", System.Globalization.NumberStyles.AllowExponent, null), "Undecilliard" },
        { System.Numerics.BigInteger.Parse("1e66", System.Globalization.NumberStyles.AllowExponent, null), "Undecillion" },
        { System.Numerics.BigInteger.Parse("1e63", System.Globalization.NumberStyles.AllowExponent, null), "Decilliard" },
        { System.Numerics.BigInteger.Parse("1e60", System.Globalization.NumberStyles.AllowExponent, null), "Decillion" },
        { System.Numerics.BigInteger.Parse("1e57", System.Globalization.NumberStyles.AllowExponent, null), "Nonilliard" },
        { System.Numerics.BigInteger.Parse("1e54", System.Globalization.NumberStyles.AllowExponent, null), "Nonillion" },
        { System.Numerics.BigInteger.Parse("1e51", System.Globalization.NumberStyles.AllowExponent, null), "Octilliard" },
        { System.Numerics.BigInteger.Parse("1e48", System.Globalization.NumberStyles.AllowExponent, null), "Octillion" },
        { System.Numerics.BigInteger.Parse("1e45", System.Globalization.NumberStyles.AllowExponent, null), "Septilliard" },
        { System.Numerics.BigInteger.Parse("1e42", System.Globalization.NumberStyles.AllowExponent, null), "Septillion" },
        { System.Numerics.BigInteger.Parse("1e39", System.Globalization.NumberStyles.AllowExponent, null), "Sextilliard" },
        { System.Numerics.BigInteger.Parse("1e36", System.Globalization.NumberStyles.AllowExponent, null), "Sextillion" },
        { System.Numerics.BigInteger.Parse("1e33", System.Globalization.NumberStyles.AllowExponent, null), "Quintilliard" },
        { System.Numerics.BigInteger.Parse("1e30", System.Globalization.NumberStyles.AllowExponent, null), "Quintillion" },
        { System.Numerics.BigInteger.Parse("1e27", System.Globalization.NumberStyles.AllowExponent, null), "Quadrilliard" },
        { System.Numerics.BigInteger.Parse("1e24", System.Globalization.NumberStyles.AllowExponent, null), "Quadrillion" },
        { System.Numerics.BigInteger.Parse("1e21", System.Globalization.NumberStyles.AllowExponent, null), "Trilliard" },
        { 1000000000000000000L, "Trillion" }, // 1e18
        { 1000000000000000L, "Billiard" }, // 1e15
        { 1000000000000L, "Billion" }, // 1e12
        { 1000000000, "Milliard" }, // 1e9
      };

    /// <summary>
    /// <para>Contains the short scale table of number to word translations.</para>
    /// </summary>
    public static System.Collections.Generic.IReadOnlyDictionary<System.Numerics.BigInteger, string> ShortScaleDictionary
      => new System.Collections.Generic.Dictionary<System.Numerics.BigInteger, string>()
      {
        { System.Numerics.BigInteger.Parse("1e123", System.Globalization.NumberStyles.AllowExponent, null), "Quadragintillion" },
        { System.Numerics.BigInteger.Parse("1e120", System.Globalization.NumberStyles.AllowExponent, null), "Noventrigintillion" },
        { System.Numerics.BigInteger.Parse("1e117", System.Globalization.NumberStyles.AllowExponent, null), "Octotrigintillion" },
        { System.Numerics.BigInteger.Parse("1e114", System.Globalization.NumberStyles.AllowExponent, null), "Septentrigintillion" },
        { System.Numerics.BigInteger.Parse("1e111", System.Globalization.NumberStyles.AllowExponent, null), "Sestrigintillion" },
        { System.Numerics.BigInteger.Parse("1e108", System.Globalization.NumberStyles.AllowExponent, null), "Quintrigintillion" },
        { System.Numerics.BigInteger.Parse("1e105", System.Globalization.NumberStyles.AllowExponent, null), "Quattuor­trigint­illion" },
        { System.Numerics.BigInteger.Parse("1e102", System.Globalization.NumberStyles.AllowExponent, null), "Trestrigintillion" },
        { System.Numerics.BigInteger.Parse("1e99", System.Globalization.NumberStyles.AllowExponent, null), "Duotrigintillion" },
        { System.Numerics.BigInteger.Parse("1e96", System.Globalization.NumberStyles.AllowExponent, null), "Untrigintillion" },
        { System.Numerics.BigInteger.Parse("1e93", System.Globalization.NumberStyles.AllowExponent, null), "Trigintillion" },
        { System.Numerics.BigInteger.Parse("1e90", System.Globalization.NumberStyles.AllowExponent, null), "Novemvigintillion" },
        { System.Numerics.BigInteger.Parse("1e87", System.Globalization.NumberStyles.AllowExponent, null), "Octovigintillion" },
        { System.Numerics.BigInteger.Parse("1e84", System.Globalization.NumberStyles.AllowExponent, null), "Septenvigintillion" },
        { System.Numerics.BigInteger.Parse("1e81", System.Globalization.NumberStyles.AllowExponent, null), "Sesvigintillion" },
        { System.Numerics.BigInteger.Parse("1e78", System.Globalization.NumberStyles.AllowExponent, null), "Quinvigintillion" },
        { System.Numerics.BigInteger.Parse("1e75", System.Globalization.NumberStyles.AllowExponent, null), "Quattuorvigintillion" },
        { System.Numerics.BigInteger.Parse("1e72", System.Globalization.NumberStyles.AllowExponent, null), "Trevigintillion" },
        { System.Numerics.BigInteger.Parse("1e69", System.Globalization.NumberStyles.AllowExponent, null), "Duovigintillion" },
        { System.Numerics.BigInteger.Parse("1e66", System.Globalization.NumberStyles.AllowExponent, null), "Unvigintillion" },
        { System.Numerics.BigInteger.Parse("1e63", System.Globalization.NumberStyles.AllowExponent, null), "Vigintillion" },
        { System.Numerics.BigInteger.Parse("1e60", System.Globalization.NumberStyles.AllowExponent, null), "Novemdecillion" },
        { System.Numerics.BigInteger.Parse("1e57", System.Globalization.NumberStyles.AllowExponent, null), "Octodecillion" },
        { System.Numerics.BigInteger.Parse("1e54", System.Globalization.NumberStyles.AllowExponent, null), "Septendecillion" },
        { System.Numerics.BigInteger.Parse("1e51", System.Globalization.NumberStyles.AllowExponent, null), "Sedecillion" },
        { System.Numerics.BigInteger.Parse("1e48", System.Globalization.NumberStyles.AllowExponent, null), "Quindecillion" },
        { System.Numerics.BigInteger.Parse("1e45", System.Globalization.NumberStyles.AllowExponent, null), "Quattuordecillion" },
        { System.Numerics.BigInteger.Parse("1e42", System.Globalization.NumberStyles.AllowExponent, null), "Tredecillion" },
        { System.Numerics.BigInteger.Parse("1e39", System.Globalization.NumberStyles.AllowExponent, null), "Duodecillion" },
        { System.Numerics.BigInteger.Parse("1e36", System.Globalization.NumberStyles.AllowExponent, null), "Undecillion" },
        { System.Numerics.BigInteger.Parse("1e33", System.Globalization.NumberStyles.AllowExponent, null), "Decillion" },
        { System.Numerics.BigInteger.Parse("1e30", System.Globalization.NumberStyles.AllowExponent, null), "Nonillion" },
        { System.Numerics.BigInteger.Parse("1e27", System.Globalization.NumberStyles.AllowExponent, null), "Octillion" },
        { System.Numerics.BigInteger.Parse("1e24", System.Globalization.NumberStyles.AllowExponent, null), "Septillion" },
        { System.Numerics.BigInteger.Parse("1e21", System.Globalization.NumberStyles.AllowExponent, null), "Sextillion" },
        { 1000000000000000000L, "Quintillion" }, // 1e18
        { 1000000000000000L, "Quadrillion" }, // 1e15
        { 1000000000000L, "Trillion" }, // 1e12
        { 1000000000, "Billion" }, // 1e9
      };

    #endregion
  }
}
