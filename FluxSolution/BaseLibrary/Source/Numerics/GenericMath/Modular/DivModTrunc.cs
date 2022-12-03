namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns the full quotient and also returns the <paramref name="remainder"/> (<paramref name="dividend"/> modulo <paramref name="divisor"/>) and integer (i.e. truncated/floor) quotient <paramref name="truncatedQuotient"/> as output parameters.</summary>
    public static TSelf DivModTrunc<TSelf>(this TSelf dividend, TSelf divisor, out TSelf remainder, out TSelf truncatedQuotient)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      truncatedQuotient = TruncMod(dividend, divisor, out remainder);

      return dividend / divisor;
    }
  }
}
