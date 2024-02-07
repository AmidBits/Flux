namespace Flux
{
  public static partial class Em
  {
    /// <summary>Creates a new string formatted as a ratio string, optionally reducing the ratio, if possible.</summary>
    public static string ToRatioString<TSelf>(this Units.RatioDisplay display, TSelf numerator, TSelf denominator, QuantifiableValueStringOptions options)
#if NET7_0_OR_GREATER
      where TSelf : System.Numerics.INumber<TSelf>
#endif
      => display switch
      {
        Units.RatioDisplay.AcolonB => $"{numerator.ToString(options.Format, options.FormatProvider)}\u2236{denominator.ToString(options.Format, options.FormatProvider)}", // As a ratio (colon).
        Units.RatioDisplay.AslashB => $"{numerator.ToString(options.Format, options.FormatProvider)}\u2044{denominator.ToString(options.Format, options.FormatProvider)}", // With a ratio slash.
        Units.RatioDisplay.AtoB => $"{numerator.ToString(options.Format, options.FormatProvider)} to {denominator.ToString(options.Format, options.FormatProvider)}", // As textual "A to B".
        _ => throw new System.ArgumentOutOfRangeException(nameof(display))
      };
  }

  namespace Units
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
