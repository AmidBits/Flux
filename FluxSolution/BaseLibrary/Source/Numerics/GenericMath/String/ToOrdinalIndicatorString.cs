#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Gets the ordinal indicator for the number. E.g. "st" for 1 and "nd" for 122.</summary>
    public static string GetOrdinalIndicatorString<TSelf>(this TSelf source)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var n = int.CreateSaturating(TSelf.Abs(source));

      return (n % 10 is var o && o < 4 && n % 100 is var t && (t < 11 || t > 13) ? o : 0) switch
      {
        1 => "st",
        2 => "nd",
        3 => "rd",
        _ => "th",
      };
    }

    /// <summary>PREVIEW! Creates a new string with the number and the ordinal indicator. E.g. "1st" for 1 and "122nd" for 122.</summary>
    public static string ToOrdinalIndicatorString<TSelf>(this TSelf source)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => $"{source}{GetOrdinalIndicatorString(source)}";
  }
}
#endif
