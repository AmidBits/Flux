namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    /// <summary>Returns the full quotient and also returns the <paramref name="remainder"/> (<paramref name="dividend"/> modulo <paramref name="divisor"/>) and integer (i.e. truncated/floor) quotient <paramref name="truncatedQuotient"/> as output parameters.</summary>
    public static TSelf DivModTrunc<TSelf>(this TSelf dividend, TSelf divisor, out TSelf remainder, out TSelf truncatedQuotient)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      truncatedQuotient = TruncMod(dividend, divisor, out remainder);

      return dividend / divisor;
    }

#else

    /// <summary>Returns the full quotient and also returns the <paramref name="remainder"/> (<paramref name="dividend"/> modulo <paramref name="divisor"/>) and integer (i.e. truncated/floor) quotient <paramref name="truncatedQuotient"/> as output parameters.</summary>
    public static System.Numerics.BigInteger DivModTrunc(this System.Numerics.BigInteger dividend, System.Numerics.BigInteger divisor, out System.Numerics.BigInteger remainder, out System.Numerics.BigInteger truncatedQuotient)
    {
      truncatedQuotient = TruncMod(dividend, divisor, out remainder);

      return dividend / divisor;
    }

    /// <summary>Returns the full quotient and also returns the <paramref name="remainder"/> (<paramref name="dividend"/> modulo <paramref name="divisor"/>) and integer (i.e. truncated/floor) quotient <paramref name="truncatedQuotient"/> as output parameters.</summary>
    public static double DivModTrunc(this double dividend, double divisor, out double remainder, out double truncatedQuotient)
    {
      truncatedQuotient = TruncMod(dividend, divisor, out remainder);

      return dividend / divisor;
    }

#endif
  }
}
