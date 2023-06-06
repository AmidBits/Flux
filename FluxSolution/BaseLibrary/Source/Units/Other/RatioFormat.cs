namespace Flux
{
  public static partial class UnitsExtensionMethods
  {
    /// <summary>Creates a new string formatted as a ratio string, optionally reducing the ratio, if possible.</summary>
    public static string ToRatioString<TSelf>(this Units.RatioFormat format, TSelf numerator, TSelf denominator)
#if NET7_0_OR_GREATER
      where TSelf : System.Numerics.INumber<TSelf>
#endif
      => format switch
      {
        Units.RatioFormat.AcolonB => $"{numerator}\u2236{denominator}", // As a ratio (colon).
        Units.RatioFormat.AslashB => $"{numerator}\u2044{denominator}", // With a ratio slash.
        Units.RatioFormat.AtoB => $"{numerator} to {denominator}", // As textual "A to B".
        _ => throw new System.ArgumentOutOfRangeException(nameof(format))
      };
  }

  namespace Units
  {
    public enum RatioFormat
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
