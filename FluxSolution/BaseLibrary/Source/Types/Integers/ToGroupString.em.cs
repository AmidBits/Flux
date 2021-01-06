namespace Flux
{
  public static partial class IntegersEm
  {
    /// <summary></summary>
    public static string ToGroupString(this System.Numerics.BigInteger source)
      => source.ToString("#,###0", System.Globalization.CultureInfo.InvariantCulture);

    /// <summary></summary>
    public static string ToGroupString(this int source)
      => source.ToString("#,###0", System.Globalization.CultureInfo.InvariantCulture);
    /// <summary></summary>
    public static string ToGroupString(this long source)
      => source.ToString("#,###0", System.Globalization.CultureInfo.InvariantCulture);
  }
}
