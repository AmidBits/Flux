namespace Flux
{
  namespace Numerics
  {
    /// <summary>A 4D cartesian coordinate.</summary>
    public interface ICartesianCoordinate4<TSelf>
    : ICartesianCoordinate<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
    {
      TSelf X { get; }
      TSelf Y { get; }
      TSelf Z { get; }
      TSelf W { get; }
    }
  }
}
