namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetOrdinalIndicator(this System.Numerics.BigInteger source)
      => (int)(source % 10 is var d && d < 4 && source % 100 is var dd && (dd < 11 || dd > 13) ? d : 0) switch
      {
        0 => "th",
        1 => "st",
        2 => "nd",
        3 => "rd",
        _ => throw new System.IndexOutOfRangeException()
      };

    public static string GetOrdinalIndicator(this int source)
      => GetOrdinalIndicator(ToBigInteger(source));

    public static string GetOrdinalIndicator(this long source)
      => GetOrdinalIndicator(ToBigInteger(source));
  }
}
