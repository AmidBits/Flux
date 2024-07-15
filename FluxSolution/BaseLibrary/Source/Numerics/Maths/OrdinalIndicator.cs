namespace Flux
{
  public static partial class OrdinalIndicator
  {
    /// <summary>Gets the ordinal indicator suffix for <paramref name="value"/>. E.g. "st" for 1 and "nd" for 122.</summary>
    public static string GetOrdinalIndicatorSuffix<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      return (int.CreateChecked(TSelf.Abs(value) % TSelf.CreateChecked(100)) is var twoDigits && twoDigits >= 11 && twoDigits <= 13 ? 0 : twoDigits % 10) switch // If two digits is 11, 12 or 13 then force switch on 0, otherwise reduce to one digit.
      {
        1 => "st",
        2 => "nd",
        3 => "rd",
        _ => "th", // Any single digit other than 1, 2 or 3.
      };
    }

    /// <summary>Gets the ordinal indicator suffix with two available single digits, <paramref name="onesDigit"/> and a <paramref name="tensDigit"/>.</summary>
    public static string GetOrdinalIndicatorSuffix<TSelf>(this TSelf onesDigit, TSelf tensDigit)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => GetOrdinalIndicatorSuffix(tensDigit * TSelf.CreateChecked(10) + onesDigit);

    /// <summary>Creates a new string with <paramref name="value"/> and its ordinal indicator. E.g. "1st" for 1 and "122nd" for 122.</summary>
    public static string ToOrdinalIndicatorString<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => $"{value}{GetOrdinalIndicatorSuffix(value)}";
  }
}
