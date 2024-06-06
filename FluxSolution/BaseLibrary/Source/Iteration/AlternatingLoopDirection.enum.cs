namespace Flux
{
  public static partial class Fx
  {
    public static System.Collections.Generic.IEnumerable<TNumber> ForAlternating<TNumber, TCount>(this AlternatingLoopDirection source, TNumber mean, TNumber step, TCount count)
      where TNumber : System.Numerics.INumber<TNumber>
      where TCount : System.Numerics.IBinaryInteger<TCount>
      => mean.LoopAlternating(step, count, source);
  }

  public enum AlternatingLoopDirection
  {
    /// <summary>Loop in an alternating direction further away from center (e.g. mean), i.e. outward.</summary>
    AwayFromCenter,
    /// <summary>Loop in an alternating direction closer towards center (e.g. mean), i.e. inward.</summary>
    TowardsCenter
  }
}
