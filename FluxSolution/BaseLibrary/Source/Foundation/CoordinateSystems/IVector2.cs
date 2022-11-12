namespace Flux
{
#if NET7_0_OR_GREATER
  /// <summary>Cartesian 2D coordinate with real numbers.</summary>
  public interface IVector2<TSelf>
    : ICartesianCoordinate2<TSelf>
    where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IRootFunctions<TSelf>
  {
    /// <summary>Compute the Chebyshev length of the 2D vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    TSelf ChebyshevLength(TSelf edgeLength);
    // => TSelf.Max(TSelf.Abs(X / edgeLength), TSelf.Abs(Y / edgeLength));

    //IVector2<TSelf> Create(TSelf x, TSelf y);

    /// <summary>Compute the Euclidean length of the vector.</summary>
    TSelf EuclideanLength();
    //=> TSelf.Sqrt(EuclideanLengthSquared());

    /// <summary>Compute the length squared of the 2D vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    TSelf EuclideanLengthSquared();
    //=> X * X + Y * Y;

    /// <summary>Compute the Manhattan length of the 2D vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    TSelf ManhattanLength(TSelf edgeLength);
    //=> TSelf.Abs(X / edgeLength) + TSelf.Abs(Y / edgeLength);

    //IVector2<TSelf> Normalized();
    //=> EuclideanLength() is var m && !TSelf.IsZero(m) ? Create(X / m, Y / m) : this;

    /// <summary>Returns the orthant (quadrant) of the 2D vector using the specified center and orthant numbering.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Orthant"/>
    //int OrthantNumber(IVector2<TSelf> center, OrthantNumbering numbering);
    //=> numbering switch
    //{
    //  OrthantNumbering.Traditional => Y >= center.Y ? (X >= center.X ? 0 : 1) : (X >= center.X ? 3 : 2),
    //  OrthantNumbering.BinaryNegativeAs1 => (X >= center.X ? 0 : 1) + (Y >= center.Y ? 0 : 2),
    //  OrthantNumbering.BinaryPositiveAs1 => (X < center.X ? 0 : 1) + (Y < center.Y ? 0 : 2),
    //  _ => throw new System.ArgumentOutOfRangeException(nameof(numbering))
    //};

    /// <summary>Returns a point -90 degrees perpendicular to the point, i.e. the point rotated 90 degrees counter clockwise. Only X and Y.</summary>
    //IVector2<TSelf> PerpendicularCcw();
    //=> Create(-Y, X);

    /// <summary>Returns a point 90 degrees perpendicular to the point, i.e. the point rotated 90 degrees clockwise. Only X and Y.</summary>
    //IVector2<TSelf> PerpendicularCw();
      //=> Create(Y, -X);
  }
#else
  public interface IVector2
    : ICartesianCoordinate2
  {
  }
#endif
}
