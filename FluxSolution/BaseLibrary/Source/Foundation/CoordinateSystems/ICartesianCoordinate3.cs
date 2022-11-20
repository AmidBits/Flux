namespace Flux
{
  /// <summary>Cartesian 3D coordinate.</summary>
  public interface ICartesianCoordinate3<TSelf>
    : ICartesianCoordinate2<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    //TSelf X { get; }
    //TSelf Y { get; }
    TSelf Z { get; }
  }
}
