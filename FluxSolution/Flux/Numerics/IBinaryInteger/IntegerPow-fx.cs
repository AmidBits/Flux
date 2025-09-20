namespace Flux
{
  public static partial class BinaryInteger
  {
    /// <summary>
    /// <para>Computes <paramref name="value"/> raised to the power of <paramref name="exponent"/>, using exponentiation by squaring.</para>
    /// <see href="https://en.wikipedia.org/wiki/Exponentiation"/>
    /// <see href="https://en.wikipedia.org/wiki/Exponentiation_by_squaring"/>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="value">The radix (base) to be raised to the <paramref name="exponent"/>-of.</param>
    /// <param name="exponent">The exponent with which to raise the <paramref name="value"/>.</param>
    /// <returns>The <paramref name="value"/> raised to the <paramref name="exponent"/>-of.</returns>
    /// <remarks>If <paramref name="value"/> and/or <paramref name="exponent"/> are zero, 1 is returned. I.e. 0&#x2070;, x&#x2070; and 0&#x02E3; all return 1 in this version.</remarks>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TInteger IntegerPow<TInteger, TExponent>(this TInteger value, TExponent exponent)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TExponent : System.Numerics.IBinaryInteger<TExponent>
      => value.TryFastIntegerPow(exponent, out var tz, out var afz, out var _)
      ? tz
      : TInteger.CreateChecked(System.Numerics.BigInteger.Pow(System.Numerics.BigInteger.CreateChecked(value), int.CreateChecked(exponent)));
    //{
    //  if (value.TryFastIntegerPow(power, out TInteger ipow, out TInteger _, out var _)) // Testing!
    //    return ipow;

    //  System.ArgumentOutOfRangeException.ThrowIfNegative(value);
    //  System.ArgumentOutOfRangeException.ThrowIfNegative(power);

    //  if (TInteger.IsZero(value) || TPower.IsZero(power))
    //    return TInteger.One; // If either value or exponent is zero, one is customary.

    //  if (value == TInteger.CreateChecked(2))
    //    return TInteger.One << int.CreateChecked(power);

    //  checked
    //  {
    //    var result = TInteger.One;

    //    while (power != TPower.One)
    //    {
    //      if (TPower.IsOddInteger(power))
    //        result *= value;

    //      power >>= 1;
    //      value *= value;
    //    }

    //    return result * value;
    //  }
    //}

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
      var ipow = radix.IntegerPow(TExponent.Abs(exponent));

      reciprocal = TReciprocal.One / TReciprocal.CreateChecked(ipow);

      return ipow;
    }
  }
}
