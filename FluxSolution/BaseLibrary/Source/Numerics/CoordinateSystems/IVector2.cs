namespace Flux
{
  /// <summary>Cartesian 2D coordinate with real numbers.</summary>
  public interface IVector2<TSelf>
    : ICartesianCoordinate2<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    //TSelf X { get; }
    //TSelf Y { get; }
  }
}
