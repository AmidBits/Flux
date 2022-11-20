namespace Flux
{
  /// <summary>Cartesian 2D coordinate using integers.</summary>
  public interface IPoint2<TSelf>
    : ICartesianCoordinate2<TSelf>
    where TSelf : System.Numerics.IBinaryInteger<TSelf>
  {
  }
}
