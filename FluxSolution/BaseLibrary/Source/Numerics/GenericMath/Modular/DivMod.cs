namespace Flux
{
  public static partial class GenericMath
  {
#if NET7_0_OR_GREATER

    /// <summary>Returns the integer (i.e. floor/truncate for floating point) quotient and also returns the <paramref name="remainder"/> (<paramref name="dividend"/> modulo <paramref name="divisor"/>) as an output parameter.</summary>
    /// <remarks>This function is equivalent to the DivRem() function, only provided for here for all INumber<> types.</remarks>
    public static TSelf DivMod<TSelf>(this TSelf dividend, TSelf divisor, out TSelf remainder)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      remainder = dividend % divisor;

      return dividend / divisor;
    }

#else

    /// <summary>Returns the integer (i.e. floor/truncate for floating point) quotient and also returns the <paramref name="remainder"/> (<paramref name="dividend"/> modulo <paramref name="divisor"/>) as an output parameter.</summary>
    /// <remarks>This function is equivalent to the DivRem() function, only provided for here for all INumber<> types.</remarks>
    public static System.Numerics.BigInteger DivMod(this System.Numerics.BigInteger dividend, System.Numerics.BigInteger divisor, out System.Numerics.BigInteger remainder)
    {
      remainder = dividend % divisor;

      return dividend / divisor;
    }

    /// <summary>Returns the integer (i.e. floor/truncate for floating point) quotient and also returns the <paramref name="remainder"/> (<paramref name="dividend"/> modulo <paramref name="divisor"/>) as an output parameter.</summary>
    /// <remarks>This function is equivalent to the DivRem() function, only provided for here for all INumber<> types.</remarks>
    public static double DivMod(this double dividend, double divisor, out double remainder)
    {
      remainder = dividend % divisor;

      return dividend / divisor;
    }

#endif
  }
}
