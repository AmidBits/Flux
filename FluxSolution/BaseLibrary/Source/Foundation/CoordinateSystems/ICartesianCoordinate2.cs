﻿namespace Flux
{
  public interface ICartesianCoordinate2
  {
    double X { get; }
    double Y { get; }

    abstract ICartesianCoordinate2 Create(double x, double y);

    /// <summary>Compute the Chebyshev length of the 2D vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    double ChebyshevLength(double edgeLength)
     => System.Math.Max(System.Math.Abs(X / edgeLength), System.Math.Abs(Y / edgeLength));

    /// <summary>Compute the Euclidean length of the vector.</summary>
    double EuclideanLength()
      => System.Math.Sqrt(EuclideanLengthSquared());

    /// <summary>Compute the length squared of the 2D vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    double EuclideanLengthSquared()
      => X * X + Y * Y;

    /// <summary>Compute the Manhattan length of the 2D vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    double ManhattanLength(double edgeLength)
      => System.Math.Abs(X / edgeLength) + System.Math.Abs(Y / edgeLength);

    /// <summary>Returns the orthant (quadrant) of the 2D vector using the specified center and orthant numbering.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Orthant"/>
    int OrthantNumber(ICartesianCoordinate2 center, OrthantNumbering numbering)
      => numbering switch
      {
        OrthantNumbering.Traditional => Y >= center.Y ? (X >= center.X ? 0 : 1) : (X >= center.X ? 3 : 2),
        OrthantNumbering.BinaryNegativeAs1 => (X >= center.X ? 0 : 1) + (Y >= center.Y ? 0 : 2),
        OrthantNumbering.BinaryPositiveAs1 => (X < center.X ? 0 : 1) + (Y < center.Y ? 0 : 2),
        _ => throw new System.ArgumentOutOfRangeException(nameof(numbering))
      };

    /// <summary>Returns a point -90 degrees perpendicular to the point, i.e. the point rotated 90 degrees counter clockwise. Only X and Y.</summary>
    ICartesianCoordinate2 PerpendicularCcw()
      => Create(-Y, X);

    /// <summary>Returns a point 90 degrees perpendicular to the point, i.e. the point rotated 90 degrees clockwise. Only X and Y.</summary>
    ICartesianCoordinate2 PerpendicularCw()
      => Create(Y, -X);

    /// <summary>Converts the <see cref="ICartesianCoordinate2"/> to a <see cref="System.ValueTuple{int, int}"/>.</summary>
    (int x, int y) ToPoint(RoundingMode mode)
    {
      var rm = new Rounding<double>(mode);

      return (int.CreateChecked(rm.RoundNumber(X)), int.CreateChecked(rm.RoundNumber(Y)));
    }

    /// <summary>Converts the <see cref="ICartesianCoordinate2"/> to a <see cref="IPolarCoordinate"/>.</summary>
    public IPolarCoordinate ToPolarCoordinate()
      => new PolarCoordinate(
        System.Math.Sqrt(X * X + Y * Y),
        System.Math.Atan2(Y, X)
      );
  }
}

//namespace Flux
//{
//  public interface ICartesianCoordinate2
//  {
//    double X { get; }
//    double Y { get; }

//    abstract ICartesianCoordinate2 Create(double x, double y);

//    /// <summary>Compute the Chebyshev length of the 2D vector.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
//    double ChebyshevLength(double edgeLength)
//     => System.Math.Max(System.Math.Abs(X / edgeLength), System.Math.Abs(Y / edgeLength));

//    /// <summary>Compute the Euclidean length of the vector.</summary>
//    double EuclideanLength()
//      => System.Math.Sqrt(EuclideanLengthSquared());

//    /// <summary>Compute the length squared of the 2D vector.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
//    double EuclideanLengthSquared()
//      => X * X + Y * Y;

//    /// <summary>Compute the Manhattan length of the 2D vector.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
//    double ManhattanLength(double edgeLength)
//      => System.Math.Abs(X / edgeLength) + System.Math.Abs(Y / edgeLength);

//    /// <summary>Returns the orthant (quadrant) of the 2D vector using the specified center and orthant numbering.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Orthant"/>
//    int OrthantNumber(ICartesianCoordinate2 center, OrthantNumbering numbering)
//      => numbering switch
//      {
//        OrthantNumbering.Traditional => Y >= center.Y ? (X >= center.X ? 0 : 1) : (X >= center.X ? 3 : 2),
//        OrthantNumbering.BinaryNegativeAs1 => (X >= center.X ? 0 : 1) + (Y >= center.Y ? 0 : 2),
//        OrthantNumbering.BinaryPositiveAs1 => (X < center.X ? 0 : 1) + (Y < center.Y ? 0 : 2),
//        _ => throw new System.ArgumentOutOfRangeException(nameof(numbering))
//      };

//    /// <summary>Returns a point -90 degrees perpendicular to the point, i.e. the point rotated 90 degrees counter clockwise. Only X and Y.</summary>
//    ICartesianCoordinate2 PerpendicularCcw()
//      => Create(-Y, X);

//    /// <summary>Returns a point 90 degrees perpendicular to the point, i.e. the point rotated 90 degrees clockwise. Only X and Y.</summary>
//    ICartesianCoordinate2 PerpendicularCw()
//      => Create(Y, -X);

//    /// <summary>Converts the <see cref="ICartesianCoordinate2"/> to a <see cref="IPolarCoordinate"/>.</summary>
//    IPolarCoordinate ToPolarCoordinate()
//      => new PolarCoordinate(
//        System.Math.Sqrt(X * X + Y * Y),
//        System.Math.Atan2(Y, X)
//      );
//  }
//}