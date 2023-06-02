using System.Linq;

namespace Flux
{
  public static partial class StringFormattingExtensionMethods
  {
#if NET7_0_OR_GREATER
    public static string ToCardinalNumeralCompoundString<TSelf>(this TSelf number)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => string.Join(' ', NumeralComposition.GetCardinalNumerals(NumeralComposition.GetCompoundNumbers(System.Numerics.BigInteger.CreateChecked(number))));
#else
    public static string ToCardinalNumeralCompoundString(this System.Numerics.BigInteger number)
      => string.Join(' ', NumeralComposition.GetCardinalNumerals(NumeralComposition.GetCompoundNumbers(number)));
#endif
  }

  /// <summary>Supporting class for breaking down and translating numbers to strings.</summary>
  public static partial class NumeralComposition
  {
    /// <summary>This is step two. It translates <paramref name="compoundNumbers"/> into cardinal numerals.</summary>
    public static System.Collections.Generic.List<string> GetCardinalNumerals(System.Collections.Generic.List<System.Numerics.BigInteger> compoundNumbers)
    {
      var list = new System.Collections.Generic.List<string>();

      var isNegative = !compoundNumbers.First().IsNonNegative();

      if (isNegative)
        list.Add("Negative");

      list.AddRange(compoundNumbers.Skip(System.Math.Abs((int)compoundNumbers.First())).Select(on => NumeralComposition.ShortScaleDictionary[on]));

      return list;
    }

    /// <summary>This is step one. It breaks a <paramref name="number"/> down into parts (compound numbers) that can be translated into cardinal numerals. The first compound number is the sign of <paramref name="number"/>.</summary>
    /// <remarks>The first number in the list is the sign, i.e. -1, 0 or 1. If the sign is 0, there are no more numbers in the list.</remarks>
    public static System.Collections.Generic.List<System.Numerics.BigInteger> GetCompoundNumbers(System.Numerics.BigInteger number)
    {
      var list = new System.Collections.Generic.List<System.Numerics.BigInteger>
      {
        number.Sign
      };

      list.AddRange(RecursiveProcess(System.Numerics.BigInteger.Abs(number))); // Ensure we pass an absolute number.

      return list;

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

    /// <summary>Contains the short scale table of number to word translations.</summary>
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
        { System.Numerics.BigInteger.Parse("1e33", System.Globalization.NumberStyles.AllowExponent, null), "Decillion" },
        { System.Numerics.BigInteger.Parse("1e30", System.Globalization.NumberStyles.AllowExponent, null), "Nonillion" },
        { System.Numerics.BigInteger.Parse("1e27", System.Globalization.NumberStyles.AllowExponent, null), "Octillion" },
        { System.Numerics.BigInteger.Parse("1e24", System.Globalization.NumberStyles.AllowExponent, null), "Septillion" },
        { System.Numerics.BigInteger.Parse("1e21", System.Globalization.NumberStyles.AllowExponent, null), "Sextillion" },
        { 1000000000000000000L, "Quintillion" },
        { 1000000000000000L, "Quadrillion" },
        { 1000000000000L, "Trillion" },
        { 1000000000, "Billion" },
        { 1000000, "Million" },
        { 1000, "Thousand" },
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
