namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Computes <paramref name="radix"/> raised to the power of <paramref name="exponent"/>, using exponentiation by squaring.</para>
    /// <see href="https://en.wikipedia.org/wiki/Exponentiation"/>
    /// <see href="https://en.wikipedia.org/wiki/Exponentiation_by_squaring"/>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="radix">The radix (base) to be raised to the power-of-<paramref name="exponent"/>.</param>
    /// <param name="exponent">The exponent with which to raise the <paramref name="radix"/>.</param>
    /// <returns>The <paramref name="radix"/> raised to the power-of-<paramref name="exponent"/>.</returns>
    /// <remarks>If <paramref name="radix"/> and/or <paramref name="exponent"/> are zero, 1 is returned. I.e. 0&#x2070;, x&#x2070; and 0&#x02E3; all return 1 in this version.</remarks>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TValue IntegerPow<TValue, TPower>(this TValue radix, TPower exponent)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      where TPower : System.Numerics.IBinaryInteger<TPower>
    {
      if (TryFastIntegerPow(radix, exponent, out var ipow)) // Testing!
        return ipow;

      if (TValue.IsZero(radix) || TPower.IsZero(exponent))
        return TValue.One; // If either value or exponent is zero, one is customary.

      exponent.AssertNonNegativeRealNumber(nameof(exponent));

      if (radix == (TValue.One + TValue.One))
        return TValue.One << int.CreateChecked(exponent);

      var result = TValue.One;

      while (exponent != TPower.One)
      {
        if (TPower.IsOddInteger(exponent)) // Only act on set bits in exponent.
          result *= radix; // Multiply by the current corresponding power-of-radix (adjusts value/exponent below for each iteration).

        exponent >>= 1; // Half the exponent for the next iteration.
        radix *= radix; // Compute power-of-radix for the next iteration.
      }

      return result * radix;
    }

    /// <summary>
    /// <para>Computes <paramref name="radix"/> raised to the power of absolute(<paramref name="exponent"/>), using exponentiation by squaring, and also returns the <paramref name="reciprocal"/> of the result (i.e. 1.0 / result) as an out parameter. The reciprocal is the same as specifying a negative exponent to <see cref="System.Math.Pow"/>.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="radix">The radix (base) to be raised to the power-of-<paramref name="exponent"/>.</param>
    /// <param name="exponent">The exponent with which to raise the <paramref name="radix"/>.</param>
    /// <param name="reciprocal">The reciprocal of <paramref name="radix"/> raised to the power-of-<paramref name="exponent"/>, i.e. 1 divided by the resulting value.</param>
    /// <returns>The <paramref name="radix"/> raised to the power-of-<paramref name="exponent"/>.</returns>
    /// <remarks>If <paramref name="radix"/> and/or <paramref name="exponent"/> are zero, 1 is returned.</remarks>
    public static TValue IntegerPowRec<TValue, TPower, TReciprocal>(this TValue radix, TPower exponent, out TReciprocal reciprocal)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      where TPower : System.Numerics.IBinaryInteger<TPower>
      where TReciprocal : System.Numerics.IFloatingPoint<TReciprocal>
    {
      var ipow = IntegerPow(radix, TPower.Abs(exponent));

      reciprocal = TReciprocal.One / TReciprocal.CreateChecked(ipow);

      return ipow;
    }

    /// <summary>
    /// <para>Computes <paramref name="radix"/> raised to the power of <paramref name="exponent"/>, using the .NET built-in functionality. This is a faster but limited version.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="radix">The radix (base) to be raised to the power-of-<paramref name="exponent"/>.</param>
    /// <param name="exponent">The exponent with which to raise the <paramref name="radix"/>.</param>
    /// <param name="fipow">The result as an out parameter, if successful. Undefined in unsuccessful.</param>
    /// <returns>Whether the operation was successful.</returns>
    public static bool TryFastIntegerPow<TValue, TPower>(this TValue radix, TPower exponent, out TValue fipow)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      where TPower : System.Numerics.IBinaryInteger<TPower>
    {
      try
      {
        fipow = radix.FastPow(exponent, out var _);

        if (fipow.GetBitLengthEx() <= 53)
          return true;
      }
      catch { }

      fipow = TValue.Zero;
      return false;
    }
  }
}
