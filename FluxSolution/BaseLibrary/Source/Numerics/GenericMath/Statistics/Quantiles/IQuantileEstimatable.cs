#if NET7_0_OR_GREATER
namespace Flux
{
  public interface IQuantileEstimatable
  {
    TSelf EstimateQuantile<TSelf>(System.Collections.Generic.IList<TSelf> sample, TSelf probability)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>;
  }
}
#endif