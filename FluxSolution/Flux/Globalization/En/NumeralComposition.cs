namespace Flux.Globalization.En
{
  /// <summary>
  /// <para>Two naming scales for large numbers have been used in English and other European languages since the early modern era: the long and short scales. Most English variants use the short scale today, but the long scale remains dominant in many non-English-speaking areas, including continental Europe and Spanish-speaking countries in the Americas. These naming procedures are based on taking the number n occurring in 103n+3 (short scale) or 106n (long scale) and concatenating Latin roots for its units, tens, and hundreds place, together with the suffix -illion.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Names_of_large_numbers"/></para>
  /// </summary>
  public enum NamingScale
  {
    /// <summary>
    /// <para>Only words up to a million are used.</para>
    /// </summary>
    AnyScale,

    /// <summary>
    /// <para>Usage: French Canada, older British, Western & Central Europe.</para>
    /// </summary>
    LongScale,

    /// <summary>
    /// <para>Usage: US, English Canada, modern British, Australia, and Eastern Europe.</para>
    /// </summary>
    ShortScale,
  }

  /// <summary>
  /// <para>Supporting class for breaking down and translating numbers to strings.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Cardinal_numeral"/></para>
  /// <para><see href="https://en.wikipedia.org/wiki/English_numerals"/></para>
  /// </summary>
  public static partial class NumeralComposition
  {
    /// <summary>
    /// <para>Creates a new string (optionally supply a <paramref name="stringBuilder"/> to use) with words representing an integer <paramref name="value"/>.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value"></param>
    /// <param name="stringBuilder">Optional. If not provided an internal instance will be used.</param>
    /// <param name="defaultSeparator">The separator used for all cases except for when <paramref name="separatorForTensAndOnes"/> is used.</param>
    /// <param name="separatorForTensAndOnes">The separator used between tens and ones.</param>
    /// <returns></returns>
    public static string ToEnglishWordString<TInteger>(this TInteger value, System.Text.StringBuilder? stringBuilder = null, NamingScale namingScale = NamingScale.ShortScale)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      stringBuilder ??= new System.Text.StringBuilder();

      InterpretNumbersAndNumerals(GetCompoundNumerals(System.Numerics.BigInteger.CreateChecked(value), namingScale), stringBuilder);

      //var e = GetCompoundNumbersAndNumerals(System.Numerics.BigInteger.CreateChecked(value), namingScale);

      //InterpretNumerals(e, stringBuilder);

      //if (e.MoveNext())
      //{
      //  var (previousNumber, previousNumeral) = e.Current;

      //  if (previousNumber.IsZero)
      //    stringBuilder.Append(previousNumeral);

      //  while (e.MoveNext())
      //  {
      //    var (currentNumber, currentNumeral) = e.Current;

      //    if (stringBuilder.Length > 0)
      //    {
      //      if (currentNumber >= 1 && currentNumber <= 9 && previousNumber >= 20 && previousNumber <= 90)
      //        stringBuilder.Append('-'); // Add a hyphen between ANY tens and ones.
      //      else
      //        stringBuilder.Append(' '); // Otherwise add a space.
      //    }

      //    stringBuilder.Append(currentNumeral);

      //    (previousNumber, previousNumeral) = (currentNumber, currentNumeral);
      //  }
      //}

      return stringBuilder.ToString();
    }

    /// <summary>
    /// <para>Creates a new string (optionally supply a <paramref name="stringBuilder"/> to use) with words representing a floating-point <paramref name="value"/>.</para>
    /// </summary>
    /// <typeparam name="TFloat"></typeparam>
    /// <param name="value"></param>
    /// <param name="spacing"></param>
    /// <param name="stringBuilder"></param>
    /// <param name="numeralDecimalSeparator"></param>
    /// <returns></returns>
    public static string ToEnglishWordString<TFloat>(this TFloat value, System.Text.StringBuilder? stringBuilder = null, NamingScale namingScale = NamingScale.ShortScale, string numeralDecimalSeparator = "And")
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
    {
      stringBuilder ??= new System.Text.StringBuilder();

      if (value is decimal decimalValue)
      {
        var (integerPart, fractionalPart, fractionalPartAsWholeNumber) = decimalValue.GetParts(out var significand, out var scale, out var scaleFactor, out var sign);

        if (!integerPart.IsZero)
        {
          InterpretNumbersAndNumerals(GetCompoundNumerals(integerPart, namingScale), stringBuilder);
          //ToEnglishWordString(integerPart, stringBuilder, namingScale);

          if (!fractionalPartAsWholeNumber.IsZero)
          {
            stringBuilder.Append(' ');

            stringBuilder.Append(numeralDecimalSeparator);
          }
        }

        if (!fractionalPartAsWholeNumber.IsZero)
        {
          InterpretNumbersAndNumerals(GetCompoundNumerals(fractionalPartAsWholeNumber, namingScale), stringBuilder);
          //ToEnglishWordString(fractionalPartAsWholeNumber, stringBuilder, namingScale);

          stringBuilder.Append(' ');

          var sb = new System.Text.StringBuilder();

          InterpretNumbersAndNumerals(GetCompoundNumerals(scaleFactor, namingScale), sb);
          //ToEnglishWordString(scaleFactor, sb, namingScale);

          sb.Replace(' ', '-');

          sb.TrimCommonPrefix(0, "One-");

          sb.Append(0.GetOrdinalIndicatorSuffix());

          if (fractionalPartAsWholeNumber.IsConsideredPlural())
            sb.Append('s');

          stringBuilder.Append(sb);
        }
      }
      else // It's another type (than decimal) of floating point, so let's convert it and call again.
        ToEnglishWordString(decimal.CreateChecked(value), stringBuilder, namingScale, numeralDecimalSeparator);

      return stringBuilder.ToString();
    }

    public static string InterpretNumbersAndNumerals(System.Collections.Generic.IEnumerable<(System.Numerics.BigInteger CompoundNumber, string CardinalNumeral)> numerals, System.Text.StringBuilder? stringBuilder = null)
    {
      stringBuilder ??= new System.Text.StringBuilder();

      using var e = numerals.GetEnumerator();

      if (e.MoveNext())
      {
        var (previousNumber, previousNumeral) = e.Current;

        if (previousNumber.IsZero)
          stringBuilder.Append(previousNumeral);

        while (e.MoveNext())
        {
          var (currentNumber, currentNumeral) = e.Current;

          if (stringBuilder.Length > 0)
          {
            if (currentNumber >= 1 && currentNumber <= 9 && previousNumber >= 20 && previousNumber <= 90)
              stringBuilder.Append('-'); // Add a hyphen between ANY tens and ones.
            else
              stringBuilder.Append(' '); // Otherwise add a space.
          }

          stringBuilder.Append(currentNumeral);

          (previousNumber, previousNumeral) = (currentNumber, currentNumeral);
        }
      }

      return stringBuilder.ToString();
    }

    /// <summary>
    /// <para>Creates a string with english compound numerals representing the number.</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="number"></param>
    /// <param name="decimalPointWord"></param>
    /// <param name="includeAnd"></param>
    /// <returns></returns>
    //public static string GetCardinalNumeralString<TSelf>(TSelf number, string decimalSeparatorWord = "Point", bool includeAnd = false)
    //  where TSelf : System.Numerics.INumber<TSelf>
    //{
    //  var sm = new SpanMaker<char>();

    //  var composition = GetCompoundNumbersAndNumerals(System.Numerics.BigInteger.CreateChecked(number)).ToList(); // The integral part.

    //  sm = sm.Append(composition.First().CardinalNumeral);

    //  AppendNumber(ref sm);

    //  if (!TSelf.IsInteger(number)) // If it's a decimal number...
    //  {
    //    if (sm.Length > 0)
    //      sm = sm.Append(' ');

    //    sm = sm.Append(decimalSeparatorWord);

    //    composition = GetCompoundNumbersAndNumerals(System.Numerics.BigInteger.CreateChecked(decimal.CreateChecked(TSelf.Abs(number)).GetParts().FractionalPartAsWholeNumber)).ToList(); // The fractional part.

    //    AppendNumber(ref sm);
    //  }

    //  return sm.ToString();

    //  void AppendNumber(ref SpanMaker<char> sm)
    //  {
    //    for (var index = 1; index < composition.Count; index++)
    //    {
    //      var (currentCompoundNumber, currentCardinalNumeral) = composition.ElementAt(index);
    //      var (previousCompoundNumber, _) = composition.ElementAt(index - 1);

    //      if (sm.Length > 0)
    //        sm = sm.Append(currentCompoundNumber >= 1 && currentCompoundNumber <= 9 && previousCompoundNumber >= 20 && previousCompoundNumber <= 90 ? '-' : ' ');

    //      sm = sm.Append(currentCardinalNumeral);

    //      if (includeAnd && currentCompoundNumber == 100)
    //      {
    //        sm = sm.Append(' ');
    //        sm = sm.Append("And");
    //      }
    //    }
    //  }
    //}

    /// <summary>
    /// <para>Creates a new sequence of compound numbers. The first entry is the sign (as in the sign function) of the number and the word "Negative" if the number is negative.</para>
    /// </summary>
    /// <remarks>The first number in the list is the sign, i.e. -1, 0 or 1. If the sign is 0, there are no more numbers in the list.</remarks>
    //public static System.Collections.Generic.IEnumerable<(System.Numerics.BigInteger CompoundNumber, string CardinalNumeral)> GetCompoundNumbersAndNumerals(this System.Numerics.BigInteger number, NamingScale namingScale = NamingScale.LongScale)
    //{
    //  yield return (number.Sign, number.IsZero ? "Zero" : number.Sign < 0 ? "Negative" : string.Empty);

    //  foreach (var bi in GetCompoundNumbers(System.Numerics.BigInteger.Abs(number), System.Numerics.BigInteger.Parse("1e123", System.Globalization.NumberStyles.AllowExponent, null)))
    //    switch (namingScale)
    //    {
    //      case NamingScale.LongScale:
    //        yield return (bi, LongScaleDictionary[bi]);
    //        break;
    //      case NamingScale.ShortScale:
    //        yield return (bi, ShortScaleDictionary[bi]);
    //        break;
    //      default:
    //        break;
    //    }
    //}

    public static System.Collections.Generic.IEnumerable<(System.Numerics.BigInteger CompoundNumber, string CardinalNumeral)> GetCompoundNumerals<TInteger>(this TInteger value, NamingScale namingScale)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      var number = System.Numerics.BigInteger.CreateChecked(value);

      yield return (number.Sign, number.IsZero ? "Zero" : number.Sign < 0 ? "Negative" : string.Empty);

      number = System.Numerics.BigInteger.Abs(number);

      var maxCutoff1E6 = System.Numerics.BigInteger.Parse("1e6", System.Globalization.NumberStyles.AllowExponent, null);
      var maxCutoff1E123 = System.Numerics.BigInteger.Parse("1e123", System.Globalization.NumberStyles.AllowExponent, null);

      switch (namingScale)
      {
        case NamingScale.AnyScale:
          foreach (var bi in GetCompoundNumbers(number, maxCutoff1E6))
          {
            var word = ((AnyScaleZeroToMillion)(int)bi).ToString();

            yield return (bi, word);
          }
          yield break;
        case NamingScale.LongScale:
          foreach (var bi in GetCompoundNumbers(number, maxCutoff1E123))
          {
            if (!LongScaleDictionary.TryGetValue(bi, out var word))
              word = ((AnyScaleZeroToMillion)(int)bi).ToString();

            yield return (bi, word);
          }
          yield break;
        case NamingScale.ShortScale:
          foreach (var bi in GetCompoundNumbers(number, maxCutoff1E123))
          {
            if (!ShortScaleDictionary.TryGetValue(bi, out var word))
              word = ((AnyScaleZeroToMillion)(int)bi).ToString();

            yield return (bi, word);
          }
          yield break;
        default:
          throw new System.NotImplementedException();
      }
    }

    //public static System.Collections.Generic.IEnumerable<(System.Numerics.BigInteger CompoundNumber, string CardinalNumeral)> GetNumeralsAnyScale<TInteger>(this TInteger value)
    //  where TInteger : System.Numerics.IBinaryInteger<TInteger>
    //{
    //  var number = System.Numerics.BigInteger.CreateChecked(value);

    //  yield return (number.Sign, number.IsZero ? "Zero" : number.Sign < 0 ? "Negative" : string.Empty);

    //  var maxCutoff = System.Numerics.BigInteger.Parse("1e6", System.Globalization.NumberStyles.AllowExponent, null);

    //  foreach (var bi in GetCompoundNumbers(System.Numerics.BigInteger.Abs(number), maxCutoff))
    //    yield return (bi, ((AnyScaleZeroToMillion)(int)bi).ToString());
    //}

    //public static System.Collections.Generic.IEnumerable<(System.Numerics.BigInteger CompoundNumber, string CardinalNumeral)> GetNumeralsLongScale<TInteger>(this TInteger value)
    //  where TInteger : System.Numerics.IBinaryInteger<TInteger>
    //{
    //  var number = System.Numerics.BigInteger.CreateChecked(value);

    //  yield return (number.Sign, number.IsZero ? "Zero" : number.Sign < 0 ? "Negative" : string.Empty);

    //  var maxCutoff = System.Numerics.BigInteger.Parse("1e123", System.Globalization.NumberStyles.AllowExponent, null);

    //  foreach (var bi in GetCompoundNumbers(System.Numerics.BigInteger.Abs(number), maxCutoff))
    //  {
    //    if (!LongScaleDictionary.TryGetValue(bi, out var word))
    //      word = ((AnyScaleZeroToMillion)(int)bi).ToString();

    //    yield return (bi, word);
    //  }
    //}

    //public static System.Collections.Generic.IEnumerable<(System.Numerics.BigInteger CompoundNumber, string CardinalNumeral)> GetNumeralsShortScale<TInteger>(this TInteger value)
    //  where TInteger : System.Numerics.IBinaryInteger<TInteger>
    //{
    //  var number = System.Numerics.BigInteger.CreateChecked(value);

    //  yield return (number.Sign, number.IsZero ? "Zero" : number.Sign < 0 ? "Negative" : string.Empty);

    //  var maxCutoff = System.Numerics.BigInteger.Parse("1e123", System.Globalization.NumberStyles.AllowExponent, null);

    //  foreach (var bi in GetCompoundNumbers(System.Numerics.BigInteger.Abs(number), maxCutoff))
    //  {
    //    if (!ShortScaleDictionary.TryGetValue(bi, out var word))
    //      word = ((AnyScaleZeroToMillion)(int)bi).ToString();

    //    yield return (bi, word);
    //  }
    //}

    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetCompoundNumbers<TInteger>(this TInteger value, TInteger maxCompound)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      yield return TInteger.Sign(value);

      foreach (var compoundNumber in GetCompoundNumbers(System.Numerics.BigInteger.Abs(System.Numerics.BigInteger.CreateChecked(value)), System.Numerics.BigInteger.CreateChecked(maxCompound)))
        yield return compoundNumber;
    }

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

            absNumber -= quotient * bi; // Set number by subtracting the values just processed (e.g. 7 * 1,000,000 = 7,000,000) and continue loop.
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
        //{ 1000000, "Million" }, // 1e6
        //{ 1000, "Thousand" }, // 1e3
        //{ 100, "Hundred" },

        //{ 90, "Ninety" },
        //{ 80, "Eighty" },
        //{ 70, "Seventy" },
        //{ 60, "Sixty" },
        //{ 50, "Fifty" },
        //{ 40, "Fourty" },
        //{ 30, "Thirty" },
        //{ 20, "Twenty" },
        //{ 19, "Nineteen" },
        //{ 18, "Eighteen" },
        //{ 17, "Seventeen" },
        //{ 16, "Sixteen" },
        //{ 15, "Fifteen" },
        //{ 14, "Fourteen" },
        //{ 13, "Thirteen" },
        //{ 12, "Twelve" },
        //{ 11, "Eleven" },
        //{ 10, "Ten" },

        //{ 9, "Nine" },
        //{ 8, "Eight" },
        //{ 7, "Seven" },
        //{ 6, "Six" },
        //{ 5, "Five" },
        //{ 4, "Four" },
        //{ 3, "Three" },
        //{ 2, "Two" },
        //{ 1, "One" },
        //{ 0, "Zero" },
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
        //{ 1000000, "Million" }, // 1e6
        //{ 1000, "Thousand" }, // 1e3
        //{ 100, "Hundred" },

        //{ 90, "Ninety" },
        //{ 80, "Eighty" },
        //{ 70, "Seventy" },
        //{ 60, "Sixty" },
        //{ 50, "Fifty" },
        //{ 40, "Fourty" },
        //{ 30, "Thirty" },
        //{ 20, "Twenty" },
        //{ 19, "Nineteen" },
        //{ 18, "Eighteen" },
        //{ 17, "Seventeen" },
        //{ 16, "Sixteen" },
        //{ 15, "Fifteen" },
        //{ 14, "Fourteen" },
        //{ 13, "Thirteen" },
        //{ 12, "Twelve" },
        //{ 11, "Eleven" },
        //{ 10, "Ten" },

        //{ 9, "Nine" },
        //{ 8, "Eight" },
        //{ 7, "Seven" },
        //{ 6, "Six" },
        //{ 5, "Five" },
        //{ 4, "Four" },
        //{ 3, "Three" },
        //{ 2, "Two" },
        //{ 1, "One" },
        //{ 0, "Zero" },
      };

    public enum AnyScaleZeroToMillion
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
  }
}
