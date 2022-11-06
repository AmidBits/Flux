namespace Flux
{
#if NET7_0_OR_GREATER
  /// <summary>Cartesian 2D coordinate using integers.</summary>
  public interface IPoint2<TSelf>
    : ICartesianCoordinate2<TSelf>
    where TSelf : System.Numerics.IBinaryInteger<TSelf>
  {
    ///// <summary>Compute the Chebyshev length of the 2D vector.</summary>
    ///// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    //TSelf ChebyshevLength(TSelf edgeLength)
    // => TSelf.Max(TSelf.Abs(X / edgeLength), TSelf.Abs(Y / edgeLength));

    //IPoint2<TSelf> Create(TSelf x, TSelf y);

    ///// <summary>Computes the closest cartesian coordinate point at the specified angle and distance.</summary>
    //IPoint2<TSelf> CreatePoint(double radAngle, double radius)
    //  => Create(TSelf.CreateChecked(System.Math.Sin(radAngle) * radius), TSelf.CreateChecked(System.Math.Cos(radAngle) * radius));

    ///// <summary>Compute the length (or magnitude) of the vector.</summary>
    ///// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    //public TSelf EuclideanLength()
    //  => EuclideanLengthSquared().IntegerSqrt();

    ///// <summary>Compute the length squared of the 2D vector.</summary>
    ///// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    //TSelf EuclideanLengthSquared()
    //  => X * X + Y * Y;

    ///// <summary>Compute the Manhattan length of the 2D vector.</summary>
    ///// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    //TSelf ManhattanLength(TSelf edgeLength)
    //  => TSelf.Abs(X / edgeLength) + TSelf.Abs(Y / edgeLength);

    //IPoint2<TSelf> Normalized()
    //  => EuclideanLength() is var m && !TSelf.IsZero(m) ? Create(X / m, Y / m) : this;

    /// <summary>Returns the orthant (quadrant) of the 2D vector using the specified center and orthant numbering.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Orthant"/>
    //int OrthantNumber(IPoint2<TSelf> center, OrthantNumbering numbering)
    // => numbering switch
    // {
    //   OrthantNumbering.Traditional => Y >= center.Y ? (X >= center.X ? 0 : 1) : (X >= center.X ? 3 : 2),
    //   OrthantNumbering.BinaryNegativeAs1 => (X >= center.X ? 0 : 1) + (Y >= center.Y ? 0 : 2),
    //   OrthantNumbering.BinaryPositiveAs1 => (X < center.X ? 0 : 1) + (Y < center.Y ? 0 : 2),
    //   _ => throw new System.ArgumentOutOfRangeException(nameof(numbering))
    // };

    ///// <summary>Returns a point -90 degrees perpendicular to the point, i.e. the point rotated 90 degrees counter clockwise. Only X and Y.</summary>
    //IPoint2<TSelf> PerpendicularCcw()
    //  => Create(-Y, X);

    ///// <summary>Returns a point 90 degrees perpendicular to the point, i.e. the point rotated 90 degrees clockwise. Only X and Y.</summary>
    //IPoint2<TSelf> PerpendicularCw()
    //  => Create(Y, -X);
  }
#else
  public interface IPoint2
  {
    int X { get; }
    int Y { get; }

    IPoint2 Create(int x, int y);

    /// <summary>Compute the Chebyshev length of the 2D vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    int ChebyshevLength(int edgeLength)
     => System.Math.Max(System.Math.Abs(X / edgeLength), System.Math.Abs(Y / edgeLength));

    /// <summary>Compute the length (or magnitude) of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    int EuclideanLength()
      => System.Convert.ToInt32(System.Math.Floor(System.Math.Sqrt(EuclideanLengthSquared())));

    /// <summary>Compute the length squared of the 2D vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    int EuclideanLengthSquared()
      => X * X + Y * Y;

    /// <summary>Compute the Manhattan length of the 2D vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    int ManhattanLength(int edgeLength)
      => System.Math.Abs(X / edgeLength) + System.Math.Abs(Y / edgeLength);

    /// <summary>Returns the orthant (quadrant) of the 2D vector using the specified center and orthant numbering.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Orthant"/>
    int OrthantNumber(IPoint2 center, OrthantNumbering numbering)
     => numbering switch
     {
       OrthantNumbering.Traditional => Y >= center.Y ? (X >= center.X ? 0 : 1) : (X >= center.X ? 3 : 2),
       OrthantNumbering.BinaryNegativeAs1 => (X >= center.X ? 0 : 1) + (Y >= center.Y ? 0 : 2),
       OrthantNumbering.BinaryPositiveAs1 => (X < center.X ? 0 : 1) + (Y < center.Y ? 0 : 2),
       _ => throw new System.ArgumentOutOfRangeException(nameof(numbering))
     };

    /// <summary>Returns a point -90 degrees perpendicular to the point, i.e. the point rotated 90 degrees counter clockwise. Only X and Y.</summary>
    IPoint2 PerpendicularCcw()
      => Create(-Y, X);

    /// <summary>Returns a point 90 degrees perpendicular to the point, i.e. the point rotated 90 degrees clockwise. Only X and Y.</summary>
    IPoint2 PerpendicularCw()
      => Create(Y, -X);
  }
#endif
}
