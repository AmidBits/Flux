namespace Flux
{
  public static partial class Convert
  {
    /// <summary>Differs from the built-in "N" format in that the number does not render decimal places if none exist./summary>
    public static string ToGroupedString<TSelf>(TSelf source)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => source.ToString("#,###0", System.Globalization.CultureInfo.InvariantCulture);
  }
}
