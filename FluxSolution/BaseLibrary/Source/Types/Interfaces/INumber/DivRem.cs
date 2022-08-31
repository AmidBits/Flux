#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>PREVIEW! Returns the integer (floor for floating point) quotient and also returns the remainder as an output parameter.</summary>
    public static TSelf DivRemMul <TSelf>(this TSelf numerator, TSelf denominator, out TSelf remainder, out TSelf multiple)
      where TSelf : System.Numerics.INumber<TSelf>
      => (multiple = numerator - (remainder = numerator % denominator)) / denominator;
  }
}
#endif
