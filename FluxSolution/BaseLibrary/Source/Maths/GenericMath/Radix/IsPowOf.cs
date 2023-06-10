namespace Flux
{
  public static partial class GenericMath
  {
#if NET7_0_OR_GREATER

    /// <summary>Determines if <paramref name="value"/> is a power of <paramref name="radix"/>.</summary>
    public static bool IsPowOf<TSelf>(this TSelf value, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertNonNegative(value);
      AssertRadix(radix);

      if (value == radix) // If the value is equal to the radix, then it's a power of the radix.
        return true;

      if (radix == (TSelf.One + TSelf.One)) // Special case for binary numbers, we can use dedicated IsPow2().
        return TSelf.IsPow2(value);

      if (value > TSelf.One)
        while (TSelf.IsZero(value % radix))
          value /= radix;

      return value == TSelf.One;
    }

#else

    /// <summary>Determines if <paramref name="value"/> is a power of <paramref name="radix"/>.</summary>
    public static bool IsPowOf(this System.Numerics.BigInteger value, System.Numerics.BigInteger radix)
    {
      AssertNonNegative(value);
      AssertRadix(radix);

      if (value == radix) // If the value is equal to the radix, then it's a power of the radix.
        return true;

      if (radix == 2) // Special case for binary numbers, we can use dedicated IsPow2().
        return value.IsPow2();

      if (value > System.Numerics.BigInteger.One)
        while ((value % radix).IsZero)
          value /= radix;

      return value == System.Numerics.BigInteger.One;
    }

#endif
  }
}
