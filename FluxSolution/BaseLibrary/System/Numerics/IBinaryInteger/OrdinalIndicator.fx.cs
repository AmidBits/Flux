namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Gets the ordinal indicator suffix for <paramref name="value"/>. E.g. "st" for 1 and "nd" for 122.</summary>
    public static string GetOrdinalIndicatorSuffix<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
    {
      return (int.CreateChecked(TValue.Abs(value) % TValue.CreateChecked(100)) is var twoDigits && twoDigits >= 11 && twoDigits <= 13 ? 0 : twoDigits % 10) switch // If two digits is 11, 12 or 13 then force switch on 0, otherwise reduce to one digit.
      {
        1 => "st",
        2 => "nd",
        3 => "rd",
        _ => "th", // Any single digit other than 1, 2 or 3.
      };
    }

    /// <summary>Gets the ordinal indicator suffix with two available single digits, <paramref name="onesDigit"/> and a <paramref name="tensDigit"/>.</summary>
    public static string GetOrdinalIndicatorSuffix<TValue>(this TValue onesDigit, TValue tensDigit)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => GetOrdinalIndicatorSuffix(tensDigit * TValue.CreateChecked(10) + onesDigit);

    /// <summary>Creates a new string with <paramref name="value"/> and its ordinal indicator. E.g. "1st" for 1 and "122nd" for 122.</summary>
    public static string ToOrdinalIndicatorString<TValue>(this TValue value)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      => $"{value}{GetOrdinalIndicatorSuffix(value)}";
  }
}
