namespace Flux
{
  public static partial class OrdinalIndicator
  {
#if NET7_0_OR_GREATER

    /// <summary>Gets the ordinal indicator for <paramref name="value"/>. E.g. "st" for 1 and "nd" for 122.</summary>
    public static string GetOrdinalIndicatorSuffix<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      return (int.CreateChecked(TSelf.Abs(value) % TSelf.CreateChecked(100)) is var twoDigits && (twoDigits < 11 || twoDigits > 13) ? twoDigits % 10 : 0) switch // If two digits is 11, 12 or 13 then force switch on 0, otherwise reduce to one digit.
      {
        1 => "st",
        2 => "nd",
        3 => "rd",
        _ => "th", // Any single digit other than 1, 2 or 3.
      };
    }

    /// <summary>Creates a new string with <paramref name="value"/> and its ordinal indicator. E.g. "1st" for 1 and "122nd" for 122.</summary>
    public static string ToOrdinalIndicatorString<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => $"{value}{GetOrdinalIndicatorSuffix(value)}";

#else

    /// <summary>Gets the ordinal indicator for <paramref name="x"/>. E.g. "st" for 1 and "nd" for 122.</summary>
    public static string GetOrdinalIndicator(this System.Numerics.BigInteger x)
    {
      var nm100 = (int)(System.Numerics.BigInteger.Abs(x) % 100);
      var nm10 = nm100 % 10;

      return (nm10 is var o && o < 4 && nm100 is var t && (t < 11 || t > 13) ? o : 0) switch
      {
        1 => "st",
        2 => "nd",
        3 => "rd",
        _ => "th",
      };
    }

    /// <summary>Creates a new string with <paramref name="x"/> and its ordinal indicator. E.g. "1st" for 1 and "122nd" for 122.</summary>
    public static string ToOrdinalIndicatorString(this System.Numerics.BigInteger x)
      => $"{x}{GetOrdinalIndicator(x)}";

#endif
  }
}
