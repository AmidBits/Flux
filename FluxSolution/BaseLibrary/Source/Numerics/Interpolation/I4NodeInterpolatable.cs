namespace Flux.Interpolation
{
#if NET7_0_OR_GREATER

  public interface I4NodeInterpolatable<TSelf>
    where TSelf : System.Numerics.IFloatingPoint<TSelf>
  {
    TSelf Interpolate4Node(TSelf y0, TSelf y1, TSelf y2, TSelf y3, TSelf mu);
  }

#else

  public interface I4NodeInterpolatable
  {
    double Interpolate4Node(double y0, double y1, double y2, double y3, double mu);
  }

#endif
}
