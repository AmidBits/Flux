namespace Flux
{
  /// <summary>Cartesian 2D coordinate using integers.</summary>
  public interface ICartesianCoordinate2<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    TSelf X { get; }
    TSelf Y { get; }
  }
}
