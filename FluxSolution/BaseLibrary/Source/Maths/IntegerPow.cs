namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    /// <summary>
    /// <para>Computes <paramref name="radix"/> raised to the power of <paramref name="exponent"/>, using exponentiation by squaring.</para>
    /// <see href="https://en.wikipedia.org/wiki/Exponentiation"/>
    /// <see href="https://en.wikipedia.org/wiki/Exponentiation_by_squaring"/>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="radix">The radix (base) to be raised to the power-of-<paramref name="exponent"/>.</param>
    /// <param name="exponent">The exponent with which to raise the <paramref name="radix"/>.</param>
    /// <returns>The <paramref name="radix"/> raised to the power-of-<paramref name="exponent"/>.</returns>
    /// <remarks>If <paramref name="radix"/> and/or <paramref name="exponent"/> are zero, 1 is returned. I.e. 0&#x2070;, x&#x2070; and 0&#x02E3; all return 1 in this version.</remarks>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TSelf IntegerPow<TSelf>(this TSelf radix, TSelf exponent)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (TryFastIntegerPow(radix, exponent, out var ipow)) // Testing!
        return ipow;

      if (TSelf.IsZero(radix) || TSelf.IsZero(exponent))
        return TSelf.One; // If either value or exponent is zero, one is customary.

      AssertNonNegative(exponent, nameof(exponent));

      if (radix == (TSelf.One + TSelf.One))
        return TSelf.One << int.CreateChecked(exponent);

      var result = TSelf.One;

      while (exponent != TSelf.One)
      {
        if (TSelf.IsOddInteger(exponent)) // Only act on set bits in exponent.
          result *= radix; // Multiply by the current corresponding power-of-radix (adjusts value/exponent below for each iteration).

        exponent >>= 1; // Half the exponent for the next iteration.
        radix *= radix; // Compute power-of-radix for the next iteration.
      }

      return result * radix;
    }

    /// <summary>
    /// <para>Computes <paramref name="radix"/> raised to the power of absolute(<paramref name="exponent"/>), using exponentiation by squaring, and also returns the <paramref name="reciprocal"/> of the result (i.e. 1.0 / result) as an out parameter. The reciprocal is the same as specifying a negative exponent to <see cref="System.Math.Pow"/>.</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="radix">The radix (base) to be raised to the power-of-<paramref name="exponent"/>.</param>
    /// <param name="exponent">The exponent with which to raise the <paramref name="radix"/>.</param>
    /// <param name="reciprocal">The reciprocal of <paramref name="radix"/> raised to the power-of-<paramref name="exponent"/>, i.e. 1 divided by the resulting value.</param>
    /// <returns>The <paramref name="radix"/> raised to the power-of-<paramref name="exponent"/>.</returns>
    /// <remarks>If <paramref name="radix"/> and/or <paramref name="exponent"/> are zero, 1 is returned. I.e. 0&#x2070;, x&#x2070; and 0&#x02E3; all return 1 in this version.</remarks>
    public static TSelf IntegerPowRec<TSelf, TReciprocal>(this TSelf radix, TSelf exponent, out TReciprocal reciprocal)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      where TReciprocal : System.Numerics.IFloatingPoint<TReciprocal>
    {
      var ipow = IntegerPow(radix, TSelf.Abs(exponent));

      reciprocal = TReciprocal.One / TReciprocal.CreateChecked(ipow);

      return ipow;
    }

    /// <summary>
    /// <para>Computes <paramref name="radix"/> raised to the power of <paramref name="exponent"/>, using the .NET built-in functionality. This is a faster but limited version.</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="radix">The radix (base) to be raised to the power-of-<paramref name="exponent"/>.</param>
    /// <param name="exponent">The exponent with which to raise the <paramref name="radix"/>.</param>
    /// <param name="fipow">The result as an out parameter, if successful. Undefined in unsuccessful.</param>
    /// <returns>Whether the operation was successful.</returns>
    public static bool TryFastIntegerPow<TSelf>(this TSelf radix, TSelf exponent, out TSelf fipow)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      try
      {
        fipow = TSelf.CreateChecked(System.Math.Round(System.Math.Pow(double.CreateChecked(radix), double.CreateChecked(exponent))));

        if (fipow.GetBitLengthEx() <= 53)
          return true;
      }
      catch { }

      fipow = TSelf.Zero;
      return false;
    }

#else

    /// <summary>Returns <paramref name="radix"/> raised to the power of <paramref name="exponent"/>, using exponentiation by squaring.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Exponentiation"/>
    /// <see href="https://en.wikipedia.org/wiki/Exponentiation_by_squaring"/>
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
