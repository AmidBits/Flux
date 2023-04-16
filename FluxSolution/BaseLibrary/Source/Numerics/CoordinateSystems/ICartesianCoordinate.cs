namespace Flux
{
  namespace Numerics
  {
#if NET7_0_OR_GREATER
    /// <summary>A cartesian coordinate.</summary>
    public interface ICartesianCoordinate<TSelf>
      where TSelf : System.Numerics.INumber<TSelf>
    {
    }
#endif
  }
}
