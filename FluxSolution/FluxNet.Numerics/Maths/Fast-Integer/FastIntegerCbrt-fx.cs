namespace FluxNet.Numerics
{
  public static partial class Maths
  {

    /// <summary>
    /// <para></para>
    /// <para>Uses the built-in <see cref="double.Sqrt(double, double)"/>.</para>
    /// <para>A negative <paramref name="number"/> results in a mirrored negative cbrt.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="number">The cubed number for which to find the root.</param>
    /// <param name="mode">The integer rounding strategy to use.</param>
    /// <param name="cbrt">The actual floating-point cube-root as an out parameter.</param>
    /// <returns>The integer cube-root.</returns>
    public static TNumber FastIntegerCbrt<TNumber>(this TNumber number, MidpointRounding mode, out double cbrt)
      where TNumber : System.Numerics.INumber<TNumber>
      => checked(TNumber.CopySign(TNumber.CreateChecked(double.Round(cbrt = double.Cbrt(double.CreateChecked(TNumber.Abs(number))), mode)), number));
  }
}
