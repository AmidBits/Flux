namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    /// <summary>
    /// <para>Computes <paramref name="value"/> raised to the power of <paramref name="exponent"/>, using exponentiation by squaring.</para>
    /// <see cref="https://en.wikipedia.org/wiki/Exponentiation"/>
    /// <see cref="https://en.wikipedia.org/wiki/Exponentiation_by_squaring"/>
    /// </summary>
    /// <remarks>If <paramref name="value"/> and/or <paramref name="exponent"/> are zero, 1 is returned. I.e. 0&#x2070;, x&#x2070; and 0&#x02E3; all return 1 in this version.</remarks>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TSelf IntegerPow<TSelf>(this TSelf value, TSelf exponent)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (TSelf.IsZero(value) || TSelf.IsZero(exponent))
        return TSelf.One; // If either value or exponent is zero, one is customary.

      AssertNonNegative(exponent, nameof(exponent));

      if (value == (TSelf.One + TSelf.One))
        return TSelf.One << int.CreateChecked(exponent);

      var result = TSelf.One;

      while (exponent != TSelf.One)
      {
        if (TSelf.IsOddInteger(exponent)) // Only act on set bits in exponent.
          result *= value; // Multiply by the current corresponding power-of-radix (adjusts value/exponent below for each iteration).

        exponent >>= 1; // Half the exponent for the next iteration.
        value *= value; // Compute power-of-radix for the next iteration.
      }

      return result * value;
    }

    /// <summary>
    /// <para>Computes <paramref name="value"/> raised to the power of absolute(<paramref name="exponent"/>), using exponentiation by squaring, and also returns the <paramref name="reciprocal"/> of the result (i.e. 1.0 / result) as an out parameter. The reciprocal is the same as specifying a negative exponent to <see cref="System.Math.Pow"/>.</para>
    /// </summary>
    /// <remarks>If <paramref name="value"/> and/or <paramref name="exponent"/> are zero, 1 is returned. I.e. 0&#x2070;, x&#x2070; and 0&#x02E3; all return 1 in this version.</remarks>
    public static TSelf IntegerPowRec<TSelf, TReciprocal>(this TSelf value, TSelf exponent, out TReciprocal reciprocal)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TReciprocal : System.Numerics.IFloatingPoint<TReciprocal>
    {
      var ipow = IntegerPow(value, TSelf.Abs(exponent));

      reciprocal = TReciprocal.One / TReciprocal.CreateChecked(ipow);

      return ipow;
    }

#else

    /// <summary>Returns <paramref name="radix"/> raised to the power of <paramref name="exponent"/>, using exponentiation by squaring.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Exponentiation"/>
    /// <see cref="https://en.wikipedia.org/wiki/Exponentiation_by_squaring"/>
    public static System.Numerics.BigInteger IntegerPow(this System.Numerics.BigInteger radix, System.Numerics.BigInteger exponent)
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
    public static System.Numerics.BigInteger IntegerPowRec(this System.Numerics.BigInteger radix, System.Numerics.BigInteger exponent, out double reciprocal)
    {
      var ipow = IntegerPow(radix, System.Numerics.BigInteger.Abs(exponent));

      reciprocal = 1d / (double)ipow;

      return ipow;
    }

#endif
  }
}
