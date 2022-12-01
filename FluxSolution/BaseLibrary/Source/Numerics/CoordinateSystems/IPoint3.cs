namespace Flux
{
  /// <summary>Cartesian 3D coordinate using integers.</summary>
  public interface IPoint3<TSelf>
    : ICartesianCoordinate3<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
  }
}
