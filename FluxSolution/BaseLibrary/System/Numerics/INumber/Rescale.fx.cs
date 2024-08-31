namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Proportionally rescale the <paramref name="number"/> from the closed interval [<paramref name="minSource"/>, <paramref name="maxSource"/>] to the closed interval [<paramref name="minTarget"/>, <paramref name="maxTarget"/>]. The <paramref name="number"/> retains its proportional interval ratio, e.g. a 5 from the closed interval [0, 10] becomes 50 when rescaled to the closed interval [0, 100].</para>
    /// </summary>
    public static TNumber Rescale<TNumber>(this TNumber number, TNumber minSource, TNumber maxSource, TNumber minTarget, TNumber maxTarget)
      where TNumber : System.Numerics.INumber<TNumber>
      => minTarget + (maxTarget - minTarget) * (number - minSource) / (maxSource - minSource);
  }
}
