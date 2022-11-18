namespace Flux
{
  public interface IQuartileEstimatable
  {
    (TSelf q1, TSelf q2, TSelf q3) EstimateQuartiles<TSelf>(System.Collections.Generic.IList<TSelf> sample)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>;
  }
}
