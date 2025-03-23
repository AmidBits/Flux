namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Attempts to compute the <paramref name="radix"/> raised to the given <paramref name="exponent"/> (power) and then rounded using the rounding <paramref name="mode"/>. The resulting <paramref name="pow"/> (double) and <paramref name="integerPow"/> are returned as out parameters.</para>
    /// <para>Computes <paramref name="radix"/> raised to the power of <paramref name="exponent"/>, using the .NET built-in functionality.</para>
    /// <para>This is a faster method, but is limited to integer output less-than-or-equal to 53 bits in size.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TExponent"></typeparam>
    /// <typeparam name="TPow"></typeparam>
    /// <param name="radix">The radix (base) to be raised to the power-of-<paramref name="exponent"/>.</param>
    /// <param name="exponent">The exponent with which to raise the <paramref name="radix"/>.</param>
    /// <param name="mode"></param>
    /// <param name="integerPow"></param>
    /// <param name="pow">The result as an out parameter, if successful. Undefined in unsuccessful.</param>
    /// <returns></returns>
    public static bool TryFastIntegerPow<TValue, TExponent, TPow>(this TValue radix, TExponent exponent, UniversalRounding mode, out TPow integerPow, out double pow)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      where TExponent : System.Numerics.IBinaryInteger<TExponent>
      where TPow : System.Numerics.IBinaryInteger<TPow>
    {
      try
      {
        integerPow = TPow.CreateChecked(radix.FastIntegerPow(exponent, mode, out pow));

        if (integerPow.GetBitLength() <= 53)
          return true;
      }
      catch { }

      integerPow = TPow.Zero;
      pow = 0.0;

      return false;
    }
  }
}
