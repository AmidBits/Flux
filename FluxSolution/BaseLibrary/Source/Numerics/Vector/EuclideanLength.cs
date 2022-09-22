#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class Vector
  {
    /// <summary>PREVIEW! Returns the Euclidean length (magnitude) for the vector.</summary>
    public static TSelf EuclideanLength<TSelf>(TSelf euclideanLengthSquared)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.IRootFunctions<TSelf>
      => TSelf.Sqrt(euclideanLengthSquared);

    /// <summary>PREVIEW! Returns the Euclidean length (magnitude) squared for the 2D vector.</summary>
    public static TSelf EuclideanLengthSquared<TSelf>(TSelf x, TSelf y)
      where TSelf : System.Numerics.INumber<TSelf>
      => x * x + y * y;

    /// <summary>PREVIEW! Returns the Euclidean length (magnitude) squared for the 3D vector.</summary>
    public static TSelf EuclideanLengthSquared<TSelf>(TSelf x, TSelf y, TSelf z)
      where TSelf : System.Numerics.INumber<TSelf>
      => x * x + y * y + z + z;
  }
}
#endif
