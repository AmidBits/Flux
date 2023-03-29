namespace Flux.Interpolation
{
  public interface I2NodeInterpolatable<TSelf>
    where TSelf : System.Numerics.IFloatingPoint<TSelf>
  {
    TSelf Interpolate2Node(TSelf y1, TSelf y2, TSelf mu);
  }
}
