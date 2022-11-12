namespace Flux
{
#if NET7_0_OR_GREATER
  /// <summary>Cartesian 2D coordinate using integers.</summary>
  public interface ICartesianCoordinate2<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    TSelf X { get; }
    TSelf Y { get; }
  }
#else
  public interface ICartesianCoordinate2
  {
    double X { get; }
    double Y { get; }
  }
#endif
}
