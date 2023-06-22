namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    /// <summary>Differs from the built-in "N" format in that the number does not render decimal places if none exist./summary>
    public static string ToGroupedString<TSelf>(this TSelf source)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => source.ToString("#,###0", System.Globalization.CultureInfo.InvariantCulture);

#else

    /// <summary>Differs from the built-in "N" format in that the number does not render decimal places if none exist./summary>
    public static string ToGroupedString(this System.Numerics.BigInteger source)
      => source.ToString("#,###0", System.Globalization.CultureInfo.InvariantCulture);

    /// <summary>Differs from the built-in "N" format in that the number does not render decimal places if none exist./summary>
    public static string ToGroupedString(this int source)
      => source.ToString("#,###0", System.Globalization.CultureInfo.InvariantCulture);

    /// <summary>Differs from the built-in "N" format in that the number does not render decimal places if none exist./summary>
    public static string ToGroupedString(this long source)
      => source.ToString("#,###0", System.Globalization.CultureInfo.InvariantCulture);

#endif
  }
}
