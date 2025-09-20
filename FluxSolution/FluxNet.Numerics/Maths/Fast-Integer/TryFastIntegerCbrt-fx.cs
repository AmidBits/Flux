namespace FluxNet.Numerics
{
  public static partial class Maths
  {
    /// <summary>
    /// <para>Attempts to compute the cube-root of a <paramref name="value"/> and then rounded using the rounding <paramref name="mode"/>. The resulting <paramref name="cbrt"/> (double) and <paramref name="icbrt"/> are returned as out parameters.</para>
    /// <para>This is a faster method, but is limited to integer input less-than-or-equal to 53 bits in size.</para>
    /// </summary>
    /// <typeparam name="TINumber"></typeparam>
    /// <typeparam name="TICbrt"></typeparam>
    /// <param name="value">The cubed number to find the root of.</param>
    /// <param name="mode"></param>
    /// <param name="icbrt">The integer cube-root of <paramref name="value"/>.</param>
    /// <param name="cbrt">The floating-point cube-root of <paramref name="value"/>.</param>
    /// <returns>Whether the operation was successful.</returns>
    public static bool TryFastIntegerCbrt<TINumber, TICbrt>(this TINumber value, System.MidpointRounding mode, out TICbrt icbrt, out double cbrt)
      where TINumber : System.Numerics.IBinaryInteger<TINumber>
      where TICbrt : System.Numerics.IBinaryInteger<TICbrt>
    {
      try
      {
        if (value.GetBitLength() <= 53)
        {
          icbrt = TICbrt.CreateChecked(value.FastIntegerCbrt(mode, out cbrt));

          return true;
        }
      }
      catch { }

      icbrt = TICbrt.Zero;
      cbrt = 0.0;

      return false;
    }
  }
}
