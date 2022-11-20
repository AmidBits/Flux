namespace Flux
{
  /// <summary>Cartesian 3D coordinate with real numbers.</summary>
  public interface IVector3<TSelf>
    : ICartesianCoordinate3<TSelf>
    where TSelf : System.Numerics.IFloatingPoint<TSelf>
  {
  }
}
