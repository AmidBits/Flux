namespace Flux
{
  public static partial class Em
  {
    public static System.Collections.Generic.IEnumerable<TNumber> ForAlternating<TNumber, TCount>(this AlternatingLoopDirection source, TNumber mean, TNumber step, TCount count)
      where TNumber : System.Numerics.INumber<TNumber>
      where TCount : System.Numerics.IBinaryInteger<TCount>
      => mean.LoopAlternating(step, count, source);
  }
}
