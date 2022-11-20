namespace Flux
{
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
}
