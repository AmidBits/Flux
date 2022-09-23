#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class Vector
  {
    /// <summary>PREVIEW! Returns the 2D vector normalized.</summary>
    public static (TSelf x, TSelf y) Normalize<TSelf>(this TSelf x, TSelf y)
      where TSelf : System.Numerics.IRootFunctions<TSelf>
      => EuclideanLength(EuclideanLengthSquared(x, y)) is var m && !TSelf.IsZero(m) ? (x / m, y / m) : (x, y);

    /// <summary>PREVIEW! Returns the 3D vector normalized.</summary>
    public static (TSelf x, TSelf y, TSelf z) Normalize<TSelf>(this TSelf x, TSelf y, TSelf z)
      where TSelf : System.Numerics.IRootFunctions<TSelf>
      => EuclideanLength(EuclideanLengthSquared(x, y, z)) is var m && !TSelf.IsZero(m) ? (x / m, y / m, z / m) : (x, y, z);
  }
}
#endif
