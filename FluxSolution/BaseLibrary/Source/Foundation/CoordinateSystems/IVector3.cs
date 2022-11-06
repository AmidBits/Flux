namespace Flux
{
#if NET7_0_OR_GREATER
  public interface IVector3<TSelf>
    where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IRootFunctions<TSelf>
  {
    TSelf X { get; }
    TSelf Y { get; }
    TSelf Z { get; }

    /// <summary>Compute the Chebyshev length of the 3D vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    TSelf ChebyshevLength(TSelf edgeLength)
     => TSelf.Max(TSelf.Max(TSelf.Abs(X / edgeLength), TSelf.Abs(Y / edgeLength)), TSelf.Abs(Z / edgeLength));

    /// <summary>Compute the Euclidean length of the vector.</summary>
    TSelf EuclideanLength()
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
#else
  public interface IVector3
  {
    double X { get; }
    double Y { get; }
    double Z { get; }
  }
#endif
  public interface ICartesianCoordinate3
  {
    double X { get; }
    double Y { get; }
    double Z { get; }

    abstract ICartesianCoordinate3 Create(double x, double y, double z);

    /// <summary>Compute the Chebyshev length of the 3D vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    double ChebyshevLength(double edgeLength)
     => System.Math.Max(System.Math.Max(System.Math.Abs(X / edgeLength), System.Math.Abs(Y / edgeLength)), System.Math.Abs(Z / edgeLength));

    /// <summary>Compute the Euclidean length of the vector.</summary>
    double EuclideanLength()
      => System.Math.Sqrt(EuclideanLengthSquared());

    /// <summary>Compute the length squared of the 3D vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    double EuclideanLengthSquared()
      => X * X + Y * Y + Z * Z;

    /// <summary>Compute the Manhattan length of the 3D vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    double ManhattanLength(double edgeLength)
      => System.Math.Abs(X / edgeLength) + System.Math.Abs(Y / edgeLength) + System.Math.Abs(Z / edgeLength);

    /// <summary>Returns the orthant (octant) of the 3D vector using the specified center and orthant numbering.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Orthant"/>
    int OrthantNumber(ICartesianCoordinate3 center, OrthantNumbering numbering)
      => numbering switch
      {
        OrthantNumbering.Traditional => Z >= center.Z ? (Y >= center.Y ? (X >= center.X ? 0 : 1) : (X >= center.X ? 3 : 2)) : (Y >= center.Y ? (X >= center.X ? 7 : 6) : (X >= center.X ? 4 : 5)),
        OrthantNumbering.BinaryNegativeAs1 => (X >= center.X ? 0 : 1) + (Y >= center.Y ? 0 : 2) + (Z >= center.Z ? 0 : 4),
        OrthantNumbering.BinaryPositiveAs1 => (X < center.X ? 0 : 1) + (Y < center.Y ? 0 : 2) + (Z < center.Z ? 0 : 4),
        _ => throw new System.ArgumentOutOfRangeException(nameof(numbering))
      };

    /// <summary>Converts the <see cref="ICartesianCoordinate3"/> to a <see cref="ICylindricalCoordinate"/>.</summary>
    ICylindricalCoordinate ToCylindricalCoordinate()
      => new CylindricalCoordinate(System.Math.Sqrt(X * X + Y * Y), (System.Math.Atan2(Y, X) + Maths.PiX2) % Maths.PiX2, Z);

#if NET7_0_OR_GREATER
    /// <summary>Converts the <see cref="ICartesianCoordinate3"/> to a <see cref="System.ValueTuple{int, int, int}"/>.</summary>
    (int x, int y, int z) ToPoint(RoundingMode mode)
    {
      var rm = new Rounding<double>(mode);

      return (int.CreateChecked(rm.RoundNumber(X)), int.CreateChecked(rm.RoundNumber(Y)), int.CreateChecked(rm.RoundNumber(Z)));
    }
#endif
    /// <summary>Converts the <see cref="ICartesianCoordinate3"/> to a <see cref="ISphericalCoordinate"/>.</summary>
    ISphericalCoordinate ToSphericalCoordinate()
    {
      var x2y2 = X * X + Y * Y;

      return new SphericalCoordinate(System.Math.Sqrt(x2y2 + Z * Z), System.Math.Atan2(System.Math.Sqrt(x2y2), Z) + System.Math.PI, System.Math.Atan2(Y, X) + System.Math.PI);
    }
  }
}
