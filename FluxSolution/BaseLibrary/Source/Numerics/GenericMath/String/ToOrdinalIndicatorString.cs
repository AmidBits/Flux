#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Gets the ordinal indicator for <paramref name="x"/>. E.g. "st" for 1 and "nd" for 122.</summary>
    public static System.ReadOnlySpan<char> GetOrdinalIndicator<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var n = int.CreateSaturating(TSelf.Abs(x));

      return (n % 10 is var o && o < 4 && n % 100 is var t && (t < 11 || t > 13) ? o : 0) switch
      {
        1 => "st",
        2 => "nd",
        3 => "rd",
        _ => "th",
      };
    }

    /// <summary>PREVIEW! Creates a new string with <paramref name="x"/> and its ordinal indicator. E.g. "1st" for 1 and "122nd" for 122.</summary>
    public static System.ReadOnlySpan<char> ToOrdinalIndicatorString<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => $"{x}{GetOrdinalIndicator(x)}";
  }
}
#endif
