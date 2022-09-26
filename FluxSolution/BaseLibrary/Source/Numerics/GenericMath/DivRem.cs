#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Returns the integer (i.e. floor/truncate for floating point) quotient and also returns the remainder as an output parameter.</summary>
    public static TSelf DivRem<TSelf>(this TSelf numerator, TSelf denominator, out TSelf remainder)
      where TSelf : System.Numerics.IDivisionOperators<TSelf, TSelf, TSelf>, System.Numerics.IModulusOperators<TSelf, TSelf, TSelf>
    {
      remainder = numerator % denominator;

      return numerator / denominator;
    }

    /// <summary>PREVIEW! Returns the full quotient and also returns the remainder and integer (i.e. truncated) quotient as output parameters.</summary>
    public static TSelf DivRemTrunc<TSelf>(this TSelf numerator, TSelf denominator, out TSelf remainder, out TSelf truncatedQuotient)
      where TSelf : System.Numerics.IDivisionOperators<TSelf, TSelf, TSelf>, System.Numerics.IModulusOperators<TSelf, TSelf, TSelf>, System.Numerics.ISubtractionOperators<TSelf, TSelf, TSelf>
    {
      truncatedQuotient = TruncDivRem(numerator, denominator, out remainder);

      return numerator / denominator;
    }

    /// <summary>PREVIEW! Returns the integer (i.e. truncated) quotient and also returns the remainder as an output parameter.</summary>
    public static TSelf TruncDivRem<TSelf>(this TSelf numerator, TSelf denominator, out TSelf remainder)
      where TSelf : System.Numerics.IDivisionOperators<TSelf, TSelf, TSelf>, System.Numerics.IModulusOperators<TSelf, TSelf, TSelf>, System.Numerics.ISubtractionOperators<TSelf, TSelf, TSelf>
    {
      remainder = numerator % denominator;

      return (numerator - remainder) / denominator;
    }
  }
}
#endif
