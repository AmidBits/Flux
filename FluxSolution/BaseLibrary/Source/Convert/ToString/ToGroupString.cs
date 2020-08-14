namespace Flux
{
  public static partial class Convert
  {
    /// <summary></summary>
    public static string ToGroupString(this System.Numerics.BigInteger source)
      => source.ToString("#,###0", System.Globalization.CultureInfo.CurrentCulture);

    /// <summary></summary>
    public static string ToGroupString(this int source)
      => source.ToString("#,###0", System.Globalization.CultureInfo.CurrentCulture);
    /// <summary></summary>
    public static string ToGroupString(this long source)
      => source.ToString("#,###0", System.Globalization.CultureInfo.CurrentCulture);
  }
}
