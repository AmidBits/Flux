namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Gets the ordinal indicator for <paramref name="x"/>. E.g. "st" for 1 and "nd" for 122.</summary>
    public static string GetOrdinalIndicator<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var nm100 = int.CreateChecked(TSelf.Abs(x) % TSelf.CreateChecked(100));
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
    public static System.ReadOnlySpan<char> ToOrdinalIndicatorString<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => $"{x}{GetOrdinalIndicator(x)}";
  }
}
