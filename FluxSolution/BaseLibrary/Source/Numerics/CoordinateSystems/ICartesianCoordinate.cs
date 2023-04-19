namespace Flux
{
  namespace Numerics
  {
    /// <summary>A cartesian coordinate.</summary>
    public interface ICartesianCoordinate<TSelf>
#if NET7_0_OR_GREATER
      where TSelf : System.Numerics.INumber<TSelf>
#endif
    {
    }
  }
}
