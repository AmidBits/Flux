namespace Flux
{
#if NET7_0_OR_GREATER
  /// <summary>Cartesian 3D coordinate.</summary>
  public interface ICartesianCoordinate3<TSelf>
    : ICartesianCoordinate2<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    //TSelf X { get; }
    //TSelf Y { get; }
    TSelf Z { get; }
  }
#else
  public interface ICartesianCoordinate3
    : ICartesianCoordinate2
  {
    double X { get; }
    double Y { get; }
    double Z { get; }
  }
#endif
}
