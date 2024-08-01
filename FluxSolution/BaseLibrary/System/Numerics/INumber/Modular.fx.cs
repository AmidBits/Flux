namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns the integer (i.e. floor/truncate for floating point) quotient and also returns the <paramref name="remainder"/> (<paramref name="dividend"/> modulo <paramref name="divisor"/>) as an output parameter.</summary>
    /// <remarks>This function is equivalent to the DivRem() function, only provided for here for all INumber<> types.</remarks>
    public static TSelf DivMod<TSelf>(this TSelf dividend, TSelf divisor, out TSelf remainder)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      remainder = dividend % divisor;

      return dividend / divisor;
    }

    /// <summary>Returns the full quotient and also returns the <paramref name="remainder"/> (<paramref name="dividend"/> modulo <paramref name="divisor"/>) and integer (i.e. truncated/floor) quotient <paramref name="truncatedQuotient"/> as output parameters.</summary>
    public static TSelf DivModTrunc<TSelf>(this TSelf dividend, TSelf divisor, out TSelf remainder, out TSelf truncatedQuotient)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      truncatedQuotient = TruncMod(dividend, divisor, out remainder);

      return dividend / divisor;
    }

    ///// <summary>Computes the integer (i.e. truncated/floor) quotient.</summary>
    //public static TSelf TruncMod<TSelf>(this TSelf dividend, TSelf divisor)
    //  where TSelf : System.Numerics.INumber<TSelf>
    //  => (dividend - (dividend % divisor)) / divisor;

    /// <summary>Computes the integer (i.e. truncated/floor) quotient and also returns the <paramref name="remainder"/> (<paramref name="dividend"/> modulo <paramref name="divisor"/>) as an output parameter.</summary>
    public static TSelf TruncMod<TSelf>(this TSelf dividend, TSelf divisor, out TSelf remainder)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      remainder = dividend % divisor;

      return (dividend - remainder) / divisor;
    }
  }
}
