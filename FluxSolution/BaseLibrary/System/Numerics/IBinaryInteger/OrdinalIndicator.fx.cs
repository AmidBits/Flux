namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Gets the ordinal indicator suffix for <paramref name="value"/>. E.g. "st" for 1 and "nd" for 122.</para>
    /// </summary>
    /// <remarks>The suffixes "st", "nd" and "rd" are consistent for all numbers ending in 1, 2 and 3, resp., except for 11, 12 and 13, which, with all other numbers, ends with the suffix "th".</remarks>
    public static string GetOrdinalIndicatorSuffix<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
    {
      var twoDigit = int.CreateChecked(TNumber.Abs(value) % TNumber.CreateChecked(100));

      var oneDigit = twoDigit is >= 11 and <= 13 ? 0 : twoDigit % 10;

      if (oneDigit == 1)
        return "st";
      else if (oneDigit == 2)
        return "nd";
      else if (oneDigit == 3)
        return "rd";
      else
        return "th";
    }

    /// <summary>
    /// <para>Creates a new string with <paramref name="value"/> and its ordinal indicator. E.g. "1st" for 1 and "122nd" for 122.</para>
    /// </summary>
    public static string ToOrdinalIndicatorString<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => value.ToString() + value.GetOrdinalIndicatorSuffix();
  }
}
