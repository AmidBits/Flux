namespace Flux
{
  namespace Numerics
  {
    /// <summary>A 4D cartesian coordinate.</summary>
    public interface ICartesianCoordinate4<TSelf>
      : ICartesianCoordinate<TSelf>
#if NET7_0_OR_GREATER
      where TSelf : System.Numerics.INumber<TSelf>
#endif
    {
      TSelf X { get; }
      TSelf Y { get; }
      TSelf Z { get; }
      TSelf W { get; }
    }
  }
}
