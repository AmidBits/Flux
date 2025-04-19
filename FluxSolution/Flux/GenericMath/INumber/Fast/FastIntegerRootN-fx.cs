namespace Flux
{
  public static partial class GenericMathFast
  {
    /// <summary>
    /// <para></para>
    /// <para>Uses the built-in <see cref="double.RootN(double, double)"/>.</para>
    /// <para>A negative <paramref name="number"/> results in a mirrored negative n-root.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TNth"></typeparam>
    /// <param name="number">The number whose nth root to compute.</param>
    /// <param name="nth">The degree of the root to compute.</param>
    /// <param name="mode">The integer rounding strategy to use.</param>
    /// <param name="rootn">The actual floating-point root-n as an out parameter.</param>
    /// <returns>The integer root-n of number.</returns>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static TNumber FastIntegerRootN<TNumber, TNth>(this TNumber number, TNth nth, UniversalRounding mode, out double rootn)
      where TNumber : System.Numerics.INumber<TNumber>
      where TNth : System.Numerics.IBinaryInteger<TNth>
      => checked(TNumber.CopySign(TNumber.CreateChecked((rootn = double.RootN(double.CreateChecked(TNumber.Abs(number)), int.CreateChecked(nth))).RoundUniversal(mode)), number));
  }
}
