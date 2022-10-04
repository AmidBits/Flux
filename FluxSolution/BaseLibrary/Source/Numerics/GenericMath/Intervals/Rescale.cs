#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Proportionally re-scale the <paramref name="x"/> from within one closed interval [<paramref name="minSource"/>, <paramref name="maxSource"/>] to within another closed interval [<paramref name="minTarget"/>, <paramref name="maxTarget"/>]. The value retains its interval ratio. E.g. a 5 in the range [0, 10] becomes 50 when rescaled to the range [0, 100].</summary>
    public static TSelf Rescale<TSelf>(this TSelf x, TSelf minSource, TSelf maxSource, TSelf minTarget, TSelf maxTarget)
      where TSelf : System.Numerics.INumberBase<TSelf>
      => (maxTarget - minTarget) * (x - minSource) / (maxSource - minSource) + minTarget;
  }
}
#endif
