namespace Flux
{
  public static partial class Em
  {
    /// <summary>
    /// <para>Create a string of words from the <paramref name="number"/>. The <paramref name="decimalPointWord"/> is added as a decimal separator word if it's not a whole number, and </para>
    /// <para><see href="https://en.wikipedia.org/wiki/Cardinal_numeral"/></para>
    /// <example>(-123.456).ToCardinalNumeralCompoundString() // = "Negative One Hundred Twenty-Three Point Four Hundred Fifty-Six".</example>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="number"></param>
    /// <returns></returns>
    public static string ToEnglishCardinalNumeralCompoundString<TSelf>(this TSelf number, bool includeAnd = false, string decimalPointWord = "Point")
      where TSelf : System.Numerics.INumber<TSelf>
      => Globalization.En.NumeralComposition.GetCardinalNumeralString(number, decimalPointWord, includeAnd);
  }

  namespace Globalization.En
  {
    /// <summary>
    /// <para>Supporting class for breaking down and translating numbers to strings.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Cardinal_numeral"/></para>
    /// <para><see href="https://en.wikipedia.org/wiki/English_numerals"/></para>
    /// </summary>
    public static partial class NumeralComposition
    {
      /// <summary>
      /// <para>Creates a string with english compound numerals representing the number.</para>
      /// </summary>
      /// <typeparam name="TSelf"></typeparam>
      /// <param name="number"></param>
      /// <param name="decimalPointWord"></param>
      /// <param name="includeAnd"></param>
      /// <returns></returns>
      public static string GetCardinalNumeralString<TSelf>(TSelf number, string decimalSeparatorWord = "Point", bool includeAnd = false)
        where TSelf : System.Numerics.INumber<TSelf>
      {
        var sm = new SpanMaker<char>();

        var composition = Globalization.En.NumeralComposition.GetCompoundNumbersAndNumerals(System.Numerics.BigInteger.CreateChecked(number)); // The integral part.

        sm = sm.Append(composition.First().CardinalNumeral);

        AppendNumber(ref sm);

        if (!TSelf.IsInteger(number)) // If it's a decimal number...
        {
          if (sm.Length > 0)
            sm = sm.Append(' ');

          sm = sm.Append(decimalSeparatorWord);

          composition = Globalization.En.NumeralComposition.GetCompoundNumbersAndNumerals(System.Numerics.BigInteger.CreateChecked(decimal.CreateChecked(TSelf.Abs(number)).GetParts().FractionalPartAsWholeNumber)); // The fractional part.

          AppendNumber(ref sm);
        }

        return sm.ToString();

        void AppendNumber(ref SpanMaker<char> sm)
        {
          for (var index = 1; index < composition.Count; index++)
          {
            var currentComposition = composition.ElementAt(index);
            var previousComposition = composition.ElementAt(index - 1);

            if (sm.Length > 0)
              sm = sm.Append(currentComposition.CompoundNumber >= 1 && currentComposition.CompoundNumber <= 9 && previousComposition.CompoundNumber >= 20 && previousComposition.CompoundNumber <= 90 ? '-' : ' ');

            sm = sm.Append(currentComposition.CardinalNumeral);

            if (includeAnd && currentComposition.CompoundNumber == 100)
            {
              sm = sm.Append(' ');
              sm = sm.Append("And");
            }
          }
        }
      }

      /// <summary>
      /// <para>Creates a list by breaking down the <paramref name="number"/> into compound numbers and translates them into cardinal numerals. The first entry is the sign of <paramref name="number"/> and the word "Negative" if <paramref name="number"/> is negative.</para>
      /// </summary>
      /// <remarks>The first number in the list is the sign, i.e. -1, 0 or 1. If the sign is 0, there are no more numbers in the list.</remarks>
      public static System.Collections.Generic.List<(System.Numerics.BigInteger CompoundNumber, string CardinalNumeral)> GetCompoundNumbersAndNumerals(System.Numerics.BigInteger number)
      {
        var list = new System.Collections.Generic.List<(System.Numerics.BigInteger CompoundNumber, string CardinalNumeral)>
        {
          (number.Sign, number.Sign < 0 ? "Negative" : string.Empty)
        };

        foreach (var bi in RecursiveProcess(System.Numerics.BigInteger.Abs(number)))
          list.Add((bi, ShortScaleDictionary[bi]));

        return list;

        //var list = new System.Collections.Generic.List<System.Numerics.BigInteger>
        //{
        //  number.Sign
        //};

        //list.AddRange(RecursiveProcess(System.Numerics.BigInteger.Abs(number))); // Ensure we pass an absolute number.

        //return list;

        static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> RecursiveProcess(System.Numerics.BigInteger number)
        {
          var list = new System.Collections.Generic.List<System.Numerics.BigInteger>();

          if (number >= 100) // If the number is GTE 100, do it.
            foreach (var kvp in ShortScaleDictionary.Where(kvp => kvp.Key >= 100).OrderByDescending(kvp => kvp.Key)) // All GTE 100, in decending order.
              if (number >= kvp.Key) // If the number is greater or equal to the place value (e.g. 7,nnn,nnn compared to 1,000,000).
              {
                var quotient = number / kvp.Key; // Compute the cardinal number for the place value (e.g. 7,nnn,nnn / 1,000,000 = 7).

                list.AddRange(RecursiveProcess(quotient)); // Recursively process the cardinal number (e.g. 7).

                list.Add(kvp.Key); // Add the place value used above (e.g. 1,000,000).

                number -= quotient * kvp.Key; // Set number by subtracting the values just processed (e.g. 7 * 1,000,000 = 7,000,000) and continue loop.
              }

          if (number >= 20) // If the number is GTE 20 (it's less than 100 at this point), do it.
          {
            var remainder = number % 10; // Compute the cardinal number for the place value (e.g. 72 % 10 = 2).

            list.Add(number - remainder); // Add the place value by subtracting the remainder (e.g. 72 - 2 = 70).

            number = remainder; // Set number to remainder (e.g. 2) and continue down.
          }

          if (number > 0) // If the number is GT 0 (it's less than 20 at this point), simply add it.
            list.Add(number); // Add the number (e.g. 2).

          return list;
        }
      }

      /// <summary>
      /// <para>Contains the short scale table of number to word translations.</para>
      /// </summary>
      public static System.Collections.Generic.IReadOnlyDictionary<System.Numerics.BigInteger, string> ShortScaleDictionary
        => new System.Collections.Generic.Dictionary<System.Numerics.BigInteger, string>()
        {
          //{ System.Numerics.BigInteger.Parse("1e123", System.Globalization.NumberStyles.AllowExponent, null), "Quadragintillion" },
          //{ System.Numerics.BigInteger.Parse("1e120", System.Globalization.NumberStyles.AllowExponent, null), "Noventrigintillion" },
          //{ System.Numerics.BigInteger.Parse("1e117", System.Globalization.NumberStyles.AllowExponent, null), "Octotrigintillion" },
          //{ System.Numerics.BigInteger.Parse("1e114", System.Globalization.NumberStyles.AllowExponent, null), "Septentrigintillion" },
          //{ System.Numerics.BigInteger.Parse("1e111", System.Globalization.NumberStyles.AllowExponent, null), "Sestrigintillion" },
          //{ System.Numerics.BigInteger.Parse("1e108", System.Globalization.NumberStyles.AllowExponent, null), "Quintrigintillion" },
          //{ System.Numerics.BigInteger.Parse("1e105", System.Globalization.NumberStyles.AllowExponent, null), "Quattuor­trigint­illion" },
          //{ System.Numerics.BigInteger.Parse("1e102", System.Globalization.NumberStyles.AllowExponent, null), "Trestrigintillion" },
          //{ System.Numerics.BigInteger.Parse("1e99", System.Globalization.NumberStyles.AllowExponent, null), "Duotrigintillion" },
          //{ System.Numerics.BigInteger.Parse("1e96", System.Globalization.NumberStyles.AllowExponent, null), "Untrigintillion" },
          //{ System.Numerics.BigInteger.Parse("1e93", System.Globalization.NumberStyles.AllowExponent, null), "Trigintillion" },
          //{ System.Numerics.BigInteger.Parse("1e90", System.Globalization.NumberStyles.AllowExponent, null), "Novemvigintillion" },
          //{ System.Numerics.BigInteger.Parse("1e87", System.Globalization.NumberStyles.AllowExponent, null), "Octovigintillion" },
          //{ System.Numerics.BigInteger.Parse("1e84", System.Globalization.NumberStyles.AllowExponent, null), "Septenvigintillion" },
          //{ System.Numerics.BigInteger.Parse("1e81", System.Globalization.NumberStyles.AllowExponent, null), "Sesvigintillion" },
          //{ System.Numerics.BigInteger.Parse("1e78", System.Globalization.NumberStyles.AllowExponent, null), "Quinvigintillion" },
          //{ System.Numerics.BigInteger.Parse("1e75", System.Globalization.NumberStyles.AllowExponent, null), "Quattuorvigintillion" },
          //{ System.Numerics.BigInteger.Parse("1e72", System.Globalization.NumberStyles.AllowExponent, null), "Trevigintillion" },
          //{ System.Numerics.BigInteger.Parse("1e69", System.Globalization.NumberStyles.AllowExponent, null), "Duovigintillion" },
          //{ System.Numerics.BigInteger.Parse("1e66", System.Globalization.NumberStyles.AllowExponent, null), "Unvigintillion" },
          //{ System.Numerics.BigInteger.Parse("1e63", System.Globalization.NumberStyles.AllowExponent, null), "Vigintillion" },
          //{ System.Numerics.BigInteger.Parse("1e60", System.Globalization.NumberStyles.AllowExponent, null), "Novemdecillion" },
          //{ System.Numerics.BigInteger.Parse("1e57", System.Globalization.NumberStyles.AllowExponent, null), "Octodecillion" },
          //{ System.Numerics.BigInteger.Parse("1e54", System.Globalization.NumberStyles.AllowExponent, null), "Septendecillion" },
          //{ System.Numerics.BigInteger.Parse("1e51", System.Globalization.NumberStyles.AllowExponent, null), "Sedecillion" },
          //{ System.Numerics.BigInteger.Parse("1e48", System.Globalization.NumberStyles.AllowExponent, null), "Quindecillion" },
          //{ System.Numerics.BigInteger.Parse("1e45", System.Globalization.NumberStyles.AllowExponent, null), "Quattuordecillion" },
          //{ System.Numerics.BigInteger.Parse("1e42", System.Globalization.NumberStyles.AllowExponent, null), "Tredecillion" },
          //{ System.Numerics.BigInteger.Parse("1e39", System.Globalization.NumberStyles.AllowExponent, null), "Duodecillion" },
          //{ System.Numerics.BigInteger.Parse("1e36", System.Globalization.NumberStyles.AllowExponent, null), "Undecillion" },
          //{ System.Numerics.BigInteger.Parse("1e33", System.Globalization.NumberStyles.AllowExponent, null), "Decillion" },
          { System.Numerics.BigInteger.Parse("1e30", System.Globalization.NumberStyles.AllowExponent, null), "Nonillion" },
          { System.Numerics.BigInteger.Parse("1e27", System.Globalization.NumberStyles.AllowExponent, null), "Octillion" },
          { System.Numerics.BigInteger.Parse("1e24", System.Globalization.NumberStyles.AllowExponent, null), "Septillion" },
          { System.Numerics.BigInteger.Parse("1e21", System.Globalization.NumberStyles.AllowExponent, null), "Sextillion" },
          { 1000000000000000000L, "Quintillion" }, // 1e18
          { 1000000000000000L, "Quadrillion" }, // 1e15
          { 1000000000000L, "Trillion" }, // 1e12
          { 1000000000, "Billion" }, // 1e9
          { 1000000, "Million" }, // 1e6
          { 1000, "Thousand" }, // 1e3
          { 100, "Hundred" },
          { 90, "Ninety" },
          { 80, "Eighty" },
          { 70, "Seventy" },
          { 60, "Sixty" },
          { 50, "Fifty" },
          { 40, "Fourty" },
          { 30, "Thirty" },
          { 20, "Twenty" },
          { 19, "Nineteen" },
          { 18, "Eighteen" },
          { 17, "Seventeen" },
          { 16, "Sixteen" },
          { 15, "Fifteen" },
          { 14, "Fourteen" },
          { 13, "Thirteen" },
          { 12, "Twelve" },
          { 11, "Eleven" },
          { 10, "Ten" },
          { 9, "Nine" },
          { 8, "Eight" },
          { 7, "Seven" },
          { 6, "Six" },
          { 5, "Five" },
          { 4, "Four" },
          { 3, "Three" },
          { 2, "Two" },
          { 1, "One" },
          { 0, "Zero" },
        };
    }
  }
}