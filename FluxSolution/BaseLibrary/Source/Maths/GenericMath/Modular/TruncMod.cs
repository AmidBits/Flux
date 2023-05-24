namespace Flux
{
  public static partial class GenericMath
  {
#if NET7_0_OR_GREATER

    /// <summary>Computes the integer (i.e. truncated/floor) quotient and also returns the <paramref name="remainder"/> (<paramref name="dividend"/> modulo <paramref name="divisor"/>) as an output parameter.</summary>
    public static TSelf TruncMod<TSelf>(this TSelf dividend, TSelf divisor, out TSelf remainder)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      remainder = dividend % divisor;

      return (dividend - remainder) / divisor;
    }

#else

    /// <summary>Computes the integer (i.e. truncated/floor) quotient and also returns the <paramref name="remainder"/> (<paramref name="dividend"/> modulo <paramref name="divisor"/>) as an output parameter.</summary>
    public static System.Numerics.BigInteger TruncMod(this System.Numerics.BigInteger dividend, System.Numerics.BigInteger divisor, out System.Numerics.BigInteger remainder)
    {
      remainder = dividend % divisor;

      return (dividend - remainder) / divisor;
    }

    /// <summary>Computes the integer (i.e. truncated/floor) quotient and also returns the <paramref name="remainder"/> (<paramref name="dividend"/> modulo <paramref name="divisor"/>) as an output parameter.</summary>
    public static double TruncMod(this double dividend, double divisor, out double remainder)
    {
      remainder = dividend % divisor;

      return (dividend - remainder) / divisor;
    }

#endif
  }
}
