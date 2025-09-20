namespace FluxNet.Numerics
{
  public static partial class Maths
  {
    /// <summary>
    /// <para>Attempts to compute the nth-root of a <paramref name="value"/> and then rounded using the rounding <paramref name="mode"/>. The resulting <paramref name="rootn"/> (double) and <paramref name="integerRoot"/> are returned as out parameters.</para>
    /// <para>This is a faster method, but is limited to integer input less-than-or-equal to 53 bits in size.</para>
    /// </summary>
    /// <typeparam name="TINumber"></typeparam>
    /// <typeparam name="TINth"></typeparam>
    /// <typeparam name="TIRoot"></typeparam>
    /// <param name="value">The value to find the root of.</param>
    /// <param name="nth">The nth exponent for which to find the root.</param>
    /// <param name="rootn">The integer <paramref name="nth"/> root of <paramref name="value"/>.</param>
    /// <returns>Whether the operation was successful.</returns>
    public static bool TryFastIntegerRootN<TINumber, TINth, TIRoot>(this TINumber value, TINth nth, System.MidpointRounding mode, out TIRoot integerRoot, out double rootn)
      where TINumber : System.Numerics.IBinaryInteger<TINumber>
      where TINth : System.Numerics.IBinaryInteger<TINth>
      where TIRoot : System.Numerics.IBinaryInteger<TIRoot>
    {
      try
      {
        if (value.GetBitLength() <= 53)
        {
          integerRoot = TIRoot.CreateChecked(value.FastIntegerRootN(nth, mode, out rootn));

          return true;
        }
      }
      catch { }

      integerRoot = TIRoot.Zero;
      rootn = 0.0;

      return false;
    }
  }
}
