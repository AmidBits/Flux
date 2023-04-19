namespace Flux.Interpolation
{
#if NET7_0_OR_GREATER

  public interface I2NodeInterpolatable<TSelf>
    where TSelf : System.Numerics.IFloatingPoint<TSelf>
  {
    TSelf Interpolate2Node(TSelf y1, TSelf y2, TSelf mu);
  }

#else

  public interface I2NodeInterpolatable
  {
    double Interpolate2Node(double y1, double y2, double mu);
  }

#endif
}
