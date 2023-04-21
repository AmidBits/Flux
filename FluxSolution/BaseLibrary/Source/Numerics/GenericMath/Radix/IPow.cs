namespace Flux
{
  public static partial class GenericMath
  {
#if NET7_0_OR_GREATER

    /// <summary>Returns <paramref name="radix"/> raised to the power of <paramref name="exponent"/>, using exponentiation by squaring.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Exponentiation"/>
    /// <see cref="https://en.wikipedia.org/wiki/Exponentiation_by_squaring"/>
    public static TSelf IPow<TSelf>(this TSelf radix, TSelf exponent)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertNonNegative(exponent, nameof(exponent));

      if (TSelf.IsZero(exponent))
        return TSelf.One;

      var result = TSelf.One;

      while (exponent != TSelf.One)
        checked
        {
          if (TSelf.IsOddInteger(exponent)) // Only act on set bits in exponent.
            result *= radix; // Multiply by the current corresponding power-of-radix (adjusted in radix below for each iteration).

          radix *= radix; // Compute power-of-radix for the next iteration.
          exponent >>= 1; // Half the exponent for the next iteration.
        }

      return result * radix;
    }

    /// <summary>Returns <paramref name="radix"/> raised to the power of Abs(<paramref name="exponent"/>), using exponentiation by squaring, and also returns the <paramref name="reciprocal"/> of the result (i.e. 1.0 / result) as an out parameter. The reciprocal is the same as specifying a negative exponent to <see cref="System.Math.Pow"/>.</summary>
    public static TSelf IPowRec<TSelf, TReciprocal>(this TSelf radix, TSelf exponent, out TReciprocal reciprocal)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TReciprocal : System.Numerics.IFloatingPoint<TReciprocal>
    {
      var ipow = IPow(radix, TSelf.Abs(exponent));

      reciprocal = TReciprocal.One / TReciprocal.CreateChecked(ipow);

      return ipow;
    }

#else

    /// <summary>Returns <paramref name="radix"/> raised to the power of <paramref name="exponent"/>, using exponentiation by squaring.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Exponentiation"/>
    /// <see cref="https://en.wikipedia.org/wiki/Exponentiation_by_squaring"/>
    public static System.Numerics.BigInteger IPow(this System.Numerics.BigInteger radix, System.Numerics.BigInteger exponent)
    {
      AssertNonNegative(exponent, nameof(exponent));

      if (exponent.IsZero)
        return System.Numerics.BigInteger.One;

      var result = System.Numerics.BigInteger.One;

      while (exponent != System.Numerics.BigInteger.One)
        checked
        {
          if (!exponent.IsEven) // Only act on set bits in exponent.
            result *= radix; // Multiply by the current corresponding power-of-radix (adjusted in radix below for each iteration).

          radix *= radix; // Compute power-of-radix for the next iteration.
          exponent >>= 1; // Half the exponent for the next iteration.
        }

      return result * radix;
    }

    /// <summary>Returns <paramref name="radix"/> raised to the power of Abs(<paramref name="exponent"/>), using exponentiation by squaring, and also returns the <paramref name="reciprocal"/> of the result (i.e. 1.0 / result) as an out parameter. The reciprocal is the same as specifying a negative exponent to <see cref="System.Math.Pow"/>.</summary>
    public static System.Numerics.BigInteger IPowRec(this System.Numerics.BigInteger radix, System.Numerics.BigInteger exponent, out double reciprocal)
    {
      var ipow = IPow(radix, System.Numerics.BigInteger.Abs(exponent));

      reciprocal = 1d / (double)ipow;

      return ipow;
    }

#endif
  }
}
