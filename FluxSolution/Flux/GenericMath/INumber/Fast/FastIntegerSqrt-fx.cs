namespace Flux
{
  public static partial class GenericMathFast
  {

    /// <summary>
    /// <para></para>
    /// <para>Uses the built-in <see cref="double.Sqrt(double, double)"/>.</para>
    /// <para>A negative <paramref name="number"/> results in a mirrored negative sqrt.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="number">The squared number for which to find the root.</param>
    /// <param name="mode">The integer rounding strategy to use.</param>
    /// <param name="sqrt">The actual floating-point square-root as an out parameter.</param>
    /// <returns>The integer square-root.</returns>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static TNumber FastIntegerSqrt<TNumber>(this TNumber number, UniversalRounding mode, out double sqrt)
      where TNumber : System.Numerics.INumber<TNumber>
      => checked(TNumber.CopySign(TNumber.CreateChecked((sqrt = double.Sqrt(double.CreateChecked(TNumber.Abs(number)))).RoundUniversal(mode)), number));
  }
}
