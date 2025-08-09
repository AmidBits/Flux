namespace Flux
{
  public static partial class Number
  {
    /// <summary>
    /// <para>Proportionally rescale the <paramref name="value"/> from the closed interval [<paramref name="minSource"/>, <paramref name="maxSource"/>] to the closed interval [<paramref name="minTarget"/>, <paramref name="maxTarget"/>].</para>
    /// <para>The <paramref name="value"/> retains its proportional interval ratio, e.g. a 5 from the closed interval [0, 10] becomes 50 when rescaled to the closed interval [0, 100].</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value"></param>
    /// <param name="minSource"></param>
    /// <param name="maxSource"></param>
    /// <param name="minTarget"></param>
    /// <param name="maxTarget"></param>
    /// <returns></returns>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static TNumber Rescale<TNumber>(this TNumber value, TNumber minSource, TNumber maxSource, TNumber minTarget, TNumber maxTarget)
      where TNumber : System.Numerics.INumber<TNumber>
      => minTarget + (maxTarget - minTarget) * (value - minSource) / (maxSource - minSource);
  }
}
