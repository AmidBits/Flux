namespace Flux
{
  public static partial class Convert
  {
    /// <summary></summary>
    public static string ToGroupedString(this System.Numerics.BigInteger source)
      => source.ToString("#,###0", System.Globalization.CultureInfo.InvariantCulture);

    /// <summary></summary>
    public static string ToGroupedString(this int source)
      => source.ToString("#,###0", System.Globalization.CultureInfo.InvariantCulture);
    /// <summary></summary>
    public static string ToGroupedString(this long source)
      => source.ToString("#,###0", System.Globalization.CultureInfo.InvariantCulture);
  }
}
