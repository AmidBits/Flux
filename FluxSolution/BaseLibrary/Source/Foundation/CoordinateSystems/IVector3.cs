#if NET7_0_OR_GREATER
namespace Flux
{
  public interface IVector3<TSelf>
    where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IRootFunctions<TSelf>
  {
    TSelf X { get; }
    TSelf Y { get; }
    TSelf Z { get; }

    IVector3<TSelf> Create(TSelf x, TSelf y, TSelf z);

    /// <summary>Compute the Chebyshev length of the 3D vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    TSelf ChebyshevLength(TSelf edgeLength)
     => TSelf.Max(TSelf.Max(TSelf.Abs(X / edgeLength), TSelf.Abs(Y / edgeLength)), TSelf.Abs(Z / edgeLength));

    /// <summary>Compute the Euclidean length of the vector.</summary>
    public TSelf EuclideanLength()
      => TSelf.Sqrt(EuclideanLengthSquared());

    /// <summary>Compute the length squared of the 3D vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    TSelf EuclideanLengthSquared()
     => X * X + Y * Y + Z * Z;

    /// <summary>Compute the Manhattan length of the 3D vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    TSelf ManhattanLength(TSelf edgeLength)
     => TSelf.Abs(X / edgeLength) + TSelf.Abs(Y / edgeLength) + TSelf.Abs(Z / edgeLength);

    /// <summary>Returns the orthant (octant) of the 3D vector using the specified center and orthant numbering.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Orthant"/>
    int OrthantNumber(IVector3<TSelf> center, OrthantNumbering numbering)
     => numbering switch
     {
       OrthantNumbering.Traditional => Z >= center.Z ? (Y >= center.Y ? (X >= center.X ? 0 : 1) : (X >= center.X ? 3 : 2)) : (Y >= center.Y ? (X >= center.X ? 7 : 6) : (X >= center.X ? 4 : 5)),
       OrthantNumbering.BinaryNegativeAs1 => (X >= center.X ? 0 : 1) + (Y >= center.Y ? 0 : 2) + (Z >= center.Z ? 0 : 4),
       OrthantNumbering.BinaryPositiveAs1 => (X < center.X ? 0 : 1) + (Y < center.Y ? 0 : 2) + (Z < center.Z ? 0 : 4),
       _ => throw new System.ArgumentOutOfRangeException(nameof(numbering))
     };
  }
}
#endif
