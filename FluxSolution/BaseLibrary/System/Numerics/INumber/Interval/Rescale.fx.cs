namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Proportionally rescale the <paramref name="value"/> from the closed interval [<paramref name="minSource"/>, <paramref name="maxSource"/>] to the closed interval [<paramref name="minTarget"/>, <paramref name="maxTarget"/>]. The <paramref name="value"/> retains its proportional interval ratio, e.g. a 5 from the closed interval [0, 10] becomes 50 when rescaled to the closed interval [0, 100].</summary>
    public static TSelf Rescale<TSelf>(this TSelf value, TSelf minSource, TSelf maxSource, TSelf minTarget, TSelf maxTarget)
      where TSelf : System.Numerics.INumber<TSelf>
      => minTarget + (maxTarget - minTarget) * (value - minSource) / (maxSource - minSource);
  }
}
