namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para></para>
    /// <para>Uses the built-in <see cref="double.RootN(double, double)"/>.</para>
    /// <para>A negative <paramref name="number"/> results in a mirrored negative n-root.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TNth"></typeparam>
    /// <param name="number"></param>
    /// <param name="nth"></param>
    /// <param name="mode"></param>
    /// <param name="rootn"></param>
    /// <returns>The resulting integer-nth-root.</returns>
    public static TNumber FastIntegerRootN<TNumber, TNth>(this TNumber number, TNth nth, UniversalRounding mode, out double rootn)
      where TNumber : System.Numerics.INumber<TNumber>
      where TNth : System.Numerics.IBinaryInteger<TNth>
    {
      checked
      {
        rootn = double.RootN(double.CreateChecked(TNumber.Abs(number)), int.CreateChecked(nth));

        return TNumber.CopySign(TNumber.CreateChecked(rootn.RoundUniversal(mode)), number);
      }
    }
  }
}
