#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class Vector
  {
    /// <summary>PREVIEW! Returns the Chebyshev length (magnitude) for the 2D vector.</summary>
    public static TSelf ChebyshevLength<TSelf>(TSelf x, TSelf y, TSelf edgeLength)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.Max(TSelf.Abs(x / edgeLength), TSelf.Abs(y / edgeLength));

    /// <summary>PREVIEW! Returns the Chebyshev length (magnitude) for the 3D vector.</summary>
    public static TSelf ChebyshevLength<TSelf>(TSelf x, TSelf y, TSelf z, TSelf edgeLength)
      where TSelf : System.Numerics.INumber<TSelf>
      => GenericMath.Max(TSelf.Abs(x / edgeLength), TSelf.Abs(y / edgeLength), TSelf.Abs(z / edgeLength));
  }
}
#endif
