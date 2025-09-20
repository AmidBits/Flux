namespace FluxNet.Numerics
{
  public static partial class Maths
  {
    /// <summary>
    /// <para>Attempts to compute the square-root of a <paramref name="value"/> and then rounded using the rounding <paramref name="mode"/>. The resulting <paramref name="sqrt"/> (double) and <paramref name="isqrt"/> are returned as out parameters.</para>
    /// <para>This is a faster method, but is limited to integer input less-than-or-equal to 53 bits in size.</para>
    /// </summary>
    /// <typeparam name="TINumber"></typeparam>
    /// <typeparam name="TISqrt"></typeparam>
    /// <param name="value">The squared number to find the root of.</param>
    /// <param name="mode"></param>
    /// <param name="isqrt">The integer square-root of <paramref name="value"/>.</param>
    /// <param name="sqrt">The actual floating-point square-root of <paramref name="value"/>.</param>
    /// <returns>Whether the operation was successful.</returns>
    public static bool TryFastIntegerSqrt<TINumber, TISqrt>(this TINumber value, System.MidpointRounding mode, out TISqrt isqrt, out double sqrt)
      where TINumber : System.Numerics.IBinaryInteger<TINumber>
      where TISqrt : System.Numerics.IBinaryInteger<TISqrt>
    {
      try
      {
        if (value.GetBitLength() <= 53)
        {
          isqrt = TISqrt.CreateChecked(value.FastIntegerSqrt(mode, out sqrt));

          return true;
        }
      }
      catch { }

      isqrt = TISqrt.Zero;
      sqrt = 0.0;

      return false;
    }
  }
}
