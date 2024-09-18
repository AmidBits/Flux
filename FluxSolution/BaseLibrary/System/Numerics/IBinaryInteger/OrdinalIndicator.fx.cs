namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Gets the ordinal indicator suffix for <paramref name="value"/>. E.g. "st" for 1 and "nd" for 122.</para>
    /// </summary>
    /// <remarks>The suffixes "st", "nd" and "rd" are consistent for all numbers ending in 1, 2 and 3, resp., except for 11, 12 and 13, which, with all other numbers, ends with the suffix "th".</remarks>
    public static string GetOrdinalIndicatorSuffix<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => (int.CreateChecked(TValue.Abs(value) % TValue.CreateChecked(100)) is var d2 && d2 >= 11 && d2 <= 13 ? 0 : d2 % 10) is var d1 // If two digits is 11, 12 or 13 then force switch on 0, otherwise reduce to one digit.
      && d1 == 1 ? "st" : d1 == 2 ? "nd" : d1 == 3 ? "rd"
      : "th"; // Any number ending in digits other than 1, 2 or 3 (EXCEPT for 11, 12 and 13 obviously, as per above).

    ///// <summary>Gets the ordinal indicator suffix with two available single digits, <paramref name="ones"/> and a <paramref name="tens"/>.</summary>
    //public static string GetOrdinalIndicatorSuffix<TValue>(this TValue ones, TValue tens)
    //  where TValue : System.Numerics.IBinaryInteger<TValue>
    //  => (tens * TValue.CreateChecked(10) + ones).GetOrdinalIndicatorSuffix();

    /// <summary>Creates a new string with <paramref name="value"/> and its ordinal indicator. E.g. "1st" for 1 and "122nd" for 122.</summary>
    public static string ToOrdinalIndicatorString<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => value.ToString() + value.GetOrdinalIndicatorSuffix();
  }
}
