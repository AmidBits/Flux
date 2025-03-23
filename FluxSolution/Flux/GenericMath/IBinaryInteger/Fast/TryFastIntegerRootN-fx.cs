namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Attempts to compute the nth-root of a <paramref name="value"/> and then rounded using the rounding <paramref name="mode"/>. The resulting <paramref name="root"/> (double) and <paramref name="integerRoot"/> are returned as out parameters.</para>
    /// <para>This is a faster method, but is limited to integer input less-than-or-equal to 53 bits in size.</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TNth"></typeparam>
    /// <typeparam name="TRoot"></typeparam>
    /// <param name="value">The value to find the root of.</param>
    /// <param name="nth">Essentially the radix.</param>
    /// <param name="root">The integer <paramref name="nth"/> root of <paramref name="value"/>.</param>
    /// <returns>Whether the operation was successful.</returns>
    public static bool TryFastIntegerRootN<TValue, TNth, TRoot>(this TValue value, TNth nth, UniversalRounding mode, out TRoot integerRoot, out double root)
      where TValue : System.Numerics.IBinaryInteger<TValue>
      where TNth : System.Numerics.IBinaryInteger<TNth>
      where TRoot : System.Numerics.IBinaryInteger<TRoot>
    {
      try
      {
        if (value.GetBitLength() <= 53)
        {
          integerRoot = TRoot.CreateChecked(value.FastIntegerRootN(nth, mode, out root));

          return true;
        }
      }
      catch { }

      integerRoot = TRoot.Zero;
      root = 0.0;

      return false;
    }
  }
}
