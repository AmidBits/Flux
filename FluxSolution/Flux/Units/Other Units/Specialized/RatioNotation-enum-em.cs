namespace Flux
{
  public static partial class Em
  {
    /// <summary>Creates a new string formatted as a ratio string, optionally reducing the ratio, if possible.</summary>
    public static string ToRatioNotationString<TSelf>(this Units.RatioNotation display, TSelf numerator, TSelf denominator, string? format, System.IFormatProvider? formatProvider = null)
      where TSelf : System.Numerics.INumber<TSelf>
      => display switch
      {
        Units.RatioNotation.AcolonB => $"{numerator.ToString(format, formatProvider)}\u2236{denominator.ToString(format, formatProvider)}", // As a ratio (colon).
        Units.RatioNotation.AslashB => $"{numerator.ToString(format, formatProvider)}\u2044{denominator.ToString(format, formatProvider)}", // With a ratio slash.
        Units.RatioNotation.AtoB => $"{numerator.ToString(format, formatProvider)} to {denominator.ToString(format, formatProvider)}", // As textual "A to B".
        _ => throw new System.ArgumentOutOfRangeException(nameof(display))
      };
  }
}
