namespace Flux
{
  public static partial class Em
  {
    /// <summary>Creates a new string formatted as a ratio string, optionally reducing the ratio, if possible.</summary>
    public static string ToRatioString<TSelf>(this Quantities.RatioDisplay display, TSelf numerator, TSelf denominator, string? format, System.IFormatProvider? formatProvider = null)
      where TSelf : System.Numerics.INumber<TSelf>
      => display switch
      {
        Quantities.RatioDisplay.AcolonB => $"{numerator.ToString(format, formatProvider)}\u2236{denominator.ToString(format, formatProvider)}", // As a ratio (colon).
        Quantities.RatioDisplay.AslashB => $"{numerator.ToString(format, formatProvider)}\u2044{denominator.ToString(format, formatProvider)}", // With a ratio slash.
        Quantities.RatioDisplay.AtoB => $"{numerator.ToString(format, formatProvider)} to {denominator.ToString(format, formatProvider)}", // As textual "A to B".
        _ => throw new System.ArgumentOutOfRangeException(nameof(display))
      };
  }

  namespace Quantities
  {
    public enum RatioDisplay
    {
      /// <summary>As a ratio (colon), e.g. "1:2".</summary>
      AcolonB,
      /// <summary>With a ratio slash, e.g. "1/2".</summary>
      AslashB,
      /// <summary>As textual "A to B", e.g. "1 to 2".</summary>
      AtoB,
    }
  }
}
