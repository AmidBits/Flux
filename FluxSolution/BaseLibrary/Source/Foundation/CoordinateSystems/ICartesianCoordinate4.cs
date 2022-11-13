namespace Flux
{
#if NET7_0_OR_GREATER
  /// <summary>Cartesian 4D coordinate.</summary>
  public interface ICartesianCoordinate4<TSelf>
    : ICartesianCoordinate3<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    //TSelf X { get; }
    //TSelf Y { get; }
    //TSelf Z { get; }
    TSelf W { get; }
  }
#else
  public interface ICartesianCoordinate4
    : ICartesianCoordinate3
  {
    double X { get; }
    double Y { get; }
    double Z { get; }
    double W { get; }
  }
#endif
}
