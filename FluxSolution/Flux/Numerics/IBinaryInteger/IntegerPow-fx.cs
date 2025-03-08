namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Computes <paramref name="radix"/> raised to the power of <paramref name="exponent"/>, using exponentiation by squaring.</para>
    /// <see href="https://en.wikipedia.org/wiki/Exponentiation"/>
    /// <see href="https://en.wikipedia.org/wiki/Exponentiation_by_squaring"/>
    /// </summary>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="radix">The radix (base) to be raised to the power-of-<paramref name="exponent"/>.</param>
    /// <param name="exponent">The exponent with which to raise the <paramref name="radix"/>.</param>
    /// <returns>The <paramref name="radix"/> raised to the power-of-<paramref name="exponent"/>.</returns>
    /// <remarks>If <paramref name="radix"/> and/or <paramref name="exponent"/> are zero, 1 is returned. I.e. 0&#x2070;, x&#x2070; and 0&#x02E3; all return 1 in this version.</remarks>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TRadix IntegerPow<TRadix, TExponent>(this TRadix radix, TExponent exponent)
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TExponent : System.Numerics.IBinaryInteger<TExponent>
    {
      if (TryFastIntegerPow(radix, exponent, UniversalRounding.WholeTowardZero, out TRadix ipow, out var _)) // Testing!
        return ipow;

      if (TRadix.IsZero(radix) || TExponent.IsZero(exponent))
        return TRadix.One; // If either value or exponent is zero, one is customary.

      exponent.AssertNonNegativeNumber(nameof(exponent));

      if (radix == TRadix.CreateChecked(2))
        return TRadix.One << int.CreateChecked(exponent);

      checked
      {
        var result = TRadix.One;

        while (exponent != TExponent.One)
        {
          if (TExponent.IsOddInteger(exponent))
            result *= radix;

          exponent >>= 1;
          radix *= radix;
        }

        return result * radix;
      }
    }

    /// <summary>
    /// <para>Computes <paramref name="radix"/> raised to the power of absolute(<paramref name="exponent"/>), using exponentiation by squaring, and also returns the <paramref name="reciprocal"/> of the result (i.e. 1.0 / result) as an out parameter. The reciprocal is the same as specifying a negative exponent to <see cref="double.Pow(double, double)"/>.</para>
    /// </summary>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="radix">The radix (base) to be raised to the power-of-<paramref name="exponent"/>.</param>
    /// <param name="exponent">The exponent with which to raise the <paramref name="radix"/>.</param>
    /// <param name="reciprocal">The reciprocal of <paramref name="radix"/> raised to the power-of-<paramref name="exponent"/>, i.e. 1 divided by the resulting value.</param>
    /// <returns>The <paramref name="radix"/> raised to the power-of-<paramref name="exponent"/>.</returns>
    /// <remarks>If <paramref name="radix"/> and/or <paramref name="exponent"/> are zero, 1 is returned.</remarks>
    public static TRadix IntegerPowRec<TRadix, TExponent, TReciprocal>(this TRadix radix, TExponent exponent, out TReciprocal reciprocal)
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TExponent : System.Numerics.IBinaryInteger<TExponent>
      where TReciprocal : System.Numerics.IFloatingPoint<TReciprocal>
    {
      var ipow = IntegerPow(radix, TExponent.Abs(exponent));

      reciprocal = TReciprocal.One / TReciprocal.CreateChecked(ipow);

      return ipow;
    }
  }
}
