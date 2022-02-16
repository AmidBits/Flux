namespace Flux
{
  public static partial class ExtensionMethods
  {
    private static string[] m_ordinalIndicator = new string[] { "th", "st", "nd", "rd" };

    public static string GetOrdinalIndicator(this System.Numerics.BigInteger source)
      => m_ordinalIndicator[(int)(source % 10 is var d && d < 4 && source % 100 is var dd && (dd < 11 || dd > 13) ? d : 0)];

    public static string GetOrdinalIndicator(this int source)
      => m_ordinalIndicator[source % 10 is var d && d < 4 && source % 100 is var dd && (dd < 11 || dd > 13) ? d : 0];

    public static string GetOrdinalIndicator(this long source)
        => m_ordinalIndicator[source % 10 is var d && d < 4 && source % 100 is var dd && (dd < 11 || dd > 13) ? d : 0];
  }
}
