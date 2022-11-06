namespace Flux
{
#if NET7_0_OR_GREATER
  /// <summary>Cartesian 3D coordinate using integers.</summary>
  public interface IPoint3<TSelf>
    : ICartesianCoordinate3<TSelf>
    where TSelf : System.Numerics.IBinaryInteger<TSelf>
  {
    /// <summary>Compute the Chebyshev length of the 3D vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    TSelf ChebyshevLength(TSelf edgeLength)
     => TSelf.Max(TSelf.Max(TSelf.Abs(X / edgeLength), TSelf.Abs(Y / edgeLength)), TSelf.Abs(Z / edgeLength));

    IPoint3<TSelf> Create(TSelf x, TSelf y, TSelf z);

    /// <summary>Compute the length (or magnitude) of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public TSelf EuclideanLength()
      => EuclideanLengthSquared().IntegerSqrt();

    /// <summary>Compute the length squared of the 3D vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    TSelf EuclideanLengthSquared()
     => X * X + Y * Y + Z * Z;

    /// <summary>Compute the Manhattan length of the 3D vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    TSelf ManhattanLength(TSelf edgeLength)
     => TSelf.Abs(X / edgeLength) + TSelf.Abs(Y / edgeLength) + TSelf.Abs(Z / edgeLength);

    IPoint3<TSelf> Normalized()
      => EuclideanLength() is var m && !TSelf.IsZero(m) ? Create(X / m, Y / m, Z / m) : this;

    /// <summary>Returns the orthant (octant) of the 3D vector using the specified center and orthant numbering.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Orthant"/>
    int OrthantNumber(IPoint3<TSelf> center, OrthantNumbering numbering)
     => numbering switch
     {
       OrthantNumbering.Traditional => Z >= center.Z ? (Y >= center.Y ? (X >= center.X ? 0 : 1) : (X >= center.X ? 3 : 2)) : (Y >= center.Y ? (X >= center.X ? 7 : 6) : (X >= center.X ? 4 : 5)),
       OrthantNumbering.BinaryNegativeAs1 => (X >= center.X ? 0 : 1) + (Y >= center.Y ? 0 : 2) + (Z >= center.Z ? 0 : 4),
       OrthantNumbering.BinaryPositiveAs1 => (X < center.X ? 0 : 1) + (Y < center.Y ? 0 : 2) + (Z < center.Z ? 0 : 4),
       _ => throw new System.ArgumentOutOfRangeException(nameof(numbering))
     };
  }
#else
  public interface IPoint3
  {
    int X { get; }
    int Y { get; }
    int Z { get; }

    IPoint3 Create(int x, int y, int z);

    /// <summary>Compute the Chebyshev length of the 3D vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    int ChebyshevLength(int edgeLength)
     => System.Math.Max(System.Math.Max(System.Math.Abs(X / edgeLength), System.Math.Abs(Y / edgeLength)), System.Math.Abs(Z / edgeLength));

    /// <summary>Compute the length (or magnitude) of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    int EuclideanLength()
      => System.Convert.ToInt32(System.Math.Floor(System.Math.Sqrt(EuclideanLengthSquared())));

    /// <summary>Compute the length squared of the 3D vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    int EuclideanLengthSquared()
     => X * X + Y * Y + Z * Z;

    /// <summary>Compute the Manhattan length of the 3D vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    int ManhattanLength(int edgeLength)
     => System.Math.Abs(X / edgeLength) + System.Math.Abs(Y / edgeLength) + System.Math.Abs(Z / edgeLength);

    /// <summary>Returns the orthant (octant) of the 3D vector using the specified center and orthant numbering.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Orthant"/>
    int OrthantNumber(IPoint3 center, OrthantNumbering numbering)
     => numbering switch
     {
       OrthantNumbering.Traditional => Z >= center.Z ? (Y >= center.Y ? (X >= center.X ? 0 : 1) : (X >= center.X ? 3 : 2)) : (Y >= center.Y ? (X >= center.X ? 7 : 6) : (X >= center.X ? 4 : 5)),
       OrthantNumbering.BinaryNegativeAs1 => (X >= center.X ? 0 : 1) + (Y >= center.Y ? 0 : 2) + (Z >= center.Z ? 0 : 4),
       OrthantNumbering.BinaryPositiveAs1 => (X < center.X ? 0 : 1) + (Y < center.Y ? 0 : 2) + (Z < center.Z ? 0 : 4),
       _ => throw new System.ArgumentOutOfRangeException(nameof(numbering))
     };
  }
#endif
}
