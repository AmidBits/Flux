namespace Flux.Interpolation
{
  public interface I2NodeInterpolatable<TSelf>
#if NET7_0_OR_GREATER
   where TSelf : System.Numerics.IFloatingPoint<TSelf>
#endif
  {
    TSelf Interpolate2Node(TSelf y1, TSelf y2, TSelf mu);
  }

  //#else

  //  public interface I2NodeInterpolatable
  //  {
  //    double Interpolate2Node(double y1, double y2, double mu);
  //  }

  //#endif
}
