namespace Flux
{
  /// <summary>Cartesian 3D coordinate with real numbers.</summary>
  public interface IVector4<TSelf>
    : ICartesianCoordinate4<TSelf>
    where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IRootFunctions<TSelf>
  {
  }
}
