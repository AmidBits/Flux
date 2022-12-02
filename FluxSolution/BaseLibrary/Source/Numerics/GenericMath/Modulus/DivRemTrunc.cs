namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns the full quotient and also returns the remainder and integer (i.e. truncated) quotient as output parameters.</summary>
    public static TSelf DivRemTrunc<TSelf>(this TSelf dividend, TSelf divisor, out TSelf remainder, out TSelf truncatedQuotient)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      truncatedQuotient = TruncRem(dividend, divisor, out remainder);

      return dividend / divisor;
    }
  }
}
