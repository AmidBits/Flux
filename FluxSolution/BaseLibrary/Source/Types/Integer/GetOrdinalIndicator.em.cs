namespace Flux
{
  public static partial class IntEm
  {
    public static string GetOrdinalIndicator(System.Numerics.BigInteger source)
      => (int)(source % 10 is var d && d < 4 && source % 100 is var dd && (dd < 11 || dd > 13) ? d : 0) switch
      {
        0 => "th",
        1 => "st",
        2 => "nd",
        3 => "rd",
        _ => throw new System.IndexOutOfRangeException()
      };

    public static string GetOrdinalIndicator(int source)
      => GetOrdinalIndicator(source.ToBigInteger());

    public static string GetOrdinalIndicator(long source)
      => GetOrdinalIndicator(source.ToBigInteger());
  }
}
