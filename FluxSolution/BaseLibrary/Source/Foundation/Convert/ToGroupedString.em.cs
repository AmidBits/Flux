namespace Flux
{
  public static partial class Convert
  {
    /// <summary>Differs from the built-in "N" format in that the number does not render decimal places if none exist./summary>
    public static string ToGroupedString(System.Numerics.BigInteger source)
      => source.ToString("#,###0", System.Globalization.CultureInfo.InvariantCulture);

    /// <summary>Differs from the built-in "N" format in that the number does not render decimal places if none exist./summary>
    public static string ToGroupedString(int source)
      => source.ToString("#,###0", System.Globalization.CultureInfo.InvariantCulture);
    /// <summary>Differs from the built-in "N" format in that the number does not render decimal places if none exist./summary>
    public static string ToGroupedString(long source)
      => source.ToString("#,###0", System.Globalization.CultureInfo.InvariantCulture);
  }
}
