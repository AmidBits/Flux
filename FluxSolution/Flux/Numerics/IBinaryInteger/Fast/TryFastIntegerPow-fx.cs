namespace Flux
{
  public static partial class BinaryIntegerFast
  {
    /// <summary>
    /// <para>Attempts to compute the <paramref name="value"/> raised to the given <paramref name="power"/> (exponent) and then rounded using the rounding <paramref name="mode"/>. The resulting <paramref name="pow"/> (double) and <paramref name="integerPow"/> are returned as out parameters.</para>
    /// <para>Computes <paramref name="value"/> raised to the power of <paramref name="power"/>, using the .NET built-in functionality.</para>
    /// <para>This is a faster method, but is limited to integer output less-than-or-equal to 53 bits in size.</para>
    /// </summary>
    /// <typeparam name="TINumber"></typeparam>
    /// <typeparam name="TIPower"></typeparam>
    /// <typeparam name="TIPow"></typeparam>
    /// <param name="value">The radix (base) to be raised to the power-of-<paramref name="power"/>.</param>
    /// <param name="power">The exponent with which to raise the <paramref name="value"/>.</param>
    /// <param name="mode"></param>
    /// <param name="integerPow"></param>
    /// <param name="pow">The result as an out parameter, if successful. Undefined if unsuccessful.</param>
    /// <returns></returns>
    public static bool TryFastIntegerPow<TINumber, TIPower, TIPow>(this TINumber value, TIPower power, UniversalRounding mode, out TIPow integerPow, out double pow)
      where TINumber : System.Numerics.IBinaryInteger<TINumber>
      where TIPower : System.Numerics.IBinaryInteger<TIPower>
      where TIPow : System.Numerics.IBinaryInteger<TIPow>
    {
      try
      {
        integerPow = TIPow.CreateChecked(value.FastIntegerPow(power, mode, out pow));

        if (integerPow.GetBitLength() <= 53)
          return true;
      }
      catch { }

      integerPow = TIPow.Zero;
      pow = 0.0;

      return false;
    }
  }
}
