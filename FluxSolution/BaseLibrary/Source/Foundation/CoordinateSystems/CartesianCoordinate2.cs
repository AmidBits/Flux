namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns the cross product of two 2D vectors.</summary>
    /// <remarks>For 2D vectors, this is equivalent to DotProduct(a, CrossProduct(b)), which is consistent with the notion of a "perpendicular dot product", which this is known as.</remarks>
    public static double CrossProduct(this CoordinateSystems.CartesianCoordinate2 cc1, CoordinateSystems.CartesianCoordinate2 cc2)
      => CoordinateSystems.CartesianCoordinate2.CrossProduct(cc1.X, cc1.Y, cc2.X, cc2.Y);

    /// <summary>Returns the dot product of two 2D vectors.</summary>
    public static double DotProduct(this CoordinateSystems.CartesianCoordinate2 cc1, CoordinateSystems.CartesianCoordinate2 cc2)
      => CoordinateSystems.CartesianCoordinate2.DotProduct(cc1.X, cc1.Y, cc2.X, cc2.Y);

    public static CoordinateSystems.CartesianCoordinate2 ToCartesianCoordinate2(this System.Numerics.Vector2 source)
      => new CoordinateSystems.CartesianCoordinate2(source.X, source.Y);
    public static System.Numerics.Vector2 ToVector2(this CoordinateSystems.CartesianCoordinate2 source)
      => new System.Numerics.Vector2((float)source.X, (float)source.Y);
  }

  namespace CoordinateSystems
  {
    /// <summary>Cartesian coordinate.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Cartesian_coordinate_system"/>
    public struct CartesianCoordinate2
      : System.IEquatable<CartesianCoordinate2>
    {
      private readonly double m_x;
      private readonly double m_y;

      public CartesianCoordinate2(double x, double y)
      {
        m_x = x;
        m_y = y;
      }

      public double X
        => m_x;
      public double Y
        => m_y;

      public Quantity.Angle ToRotationAngle()
        => new Quantity.Angle(ConvertToRotationAngle(m_x, m_y));
      public Quantity.Angle ToRotationAngleEx()
        => new Quantity.Angle(ConvertToRotationAngleEx(m_x, m_y));
      public PolarCoordinate ToPolarCoordinate()
        => new PolarCoordinate(System.Math.Sqrt(m_x * m_x + m_y * m_y), System.Math.Atan2(m_y, m_x));

      #region Static methods
      /// <summary>(2D) Calculate the angle between the source vector and the specified target vector.
      /// When dot eq 0 then the vectors are perpendicular.
      /// When dot gt 0 then the angle is less than 90 degrees (dot=1 can be interpreted as the same direction).
      /// When dot lt 0 then the angle is greater than 90 degrees (dot=-1 can be interpreted as the opposite direction).
      /// </summary>
      public static double AngleBetween(double x1, double y1, double x2, double y2)
        => System.Math.Acos(System.Math.Clamp(DotProduct(x1, y1, x2, y2) / (EuclideanLength(x1, y1) * EuclideanLength(x2, y2)), -1, 1));

      /// <summary>Returns the angle to the 2D X-axis.</summary>
      public static double AngleToAxisX(double x, double y)
        => System.Math.Atan2(System.Math.Sqrt(y * y), x);
      /// <summary>Returns the angle to the 2D Y-axis.</summary>
      public static double AngleToAxisY(double x, double y)
        => System.Math.Atan2(System.Math.Sqrt(x * x), y);

      /// <summary>Convert the cartesian 2D coordinate (x, y) where 'right-center' is 'zero' (i.e. positive-x and neutral-y) to a counter-clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
      public static double ConvertToRotationAngle(double x, double y)
        => System.Math.Atan2(y, x) is var atan2 && atan2 < 0 ? Maths.PiX2 + atan2 : atan2;
      /// <summary>Convert the cartesian 2D coordinate (x, y) where 'center-up' is 'zero' (i.e. neutral-x and positive-y) to a clockwise rotation angle [0, PI*2] (radians). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
      public static double ConvertToRotationAngleEx(double x, double y)
        => Maths.PiX2 - ConvertToRotationAngle(y, -x); // Pass the cartesian vector (x, y) rotated 90 degrees counter-clockwise.

      /// <summary>Returns the cross product of two 2D vectors.</summary>
      /// <remarks>For 2D vectors, this is equivalent to DotProduct(a, CrossProduct(b)), which is consistent with the notion of a "perpendicular dot product", which this is known as.</remarks>
      public static double CrossProduct(double x1, double y1, double x2, double y2)
        => x1 * y2 - y1 * x2;

      /// <summary>Returns the dot product of two 2D vectors.</summary>
      public static double DotProduct(double x1, double y1, double x2, double y2)
        => x1 * x2 + y1 * y2;

      /// <summary>Compute the Chebyshev distance from vector a to vector b.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
      public static double ChebyshevDistance(double x1, double y1, double x2, double y2, double edgeLength = 1)
        => ChebyshevLength(x2 - x1, y2 - y1, edgeLength);
      /// <summary>Compute the Chebyshev length of the vector. To compute the Chebyshev distance between two vectors, ChebyshevLength(target - source).</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
      public static double ChebyshevLength(double x, double y, double edgeLength = 1)
        => System.Math.Max(System.Math.Abs(x / edgeLength), System.Math.Abs(x / edgeLength));

      /// <summary>Compute the Euclidean distance from vector a to vector b.</summary>
      public static double EuclideanDistance(double x1, double y1, double x2, double y2)
        => EuclideanLength(x2 - x1, y2 - y1);
      /// <summary>Compute the Euclidean length of the vector.</summary>
      public static double EuclideanLength(double x, double y)
        => System.Math.Sqrt(EuclideanLengthSquared(x, y));
      /// <summary>Compute the Euclidean length squared of the vector.</summary>
      public static double EuclideanLengthSquared(double x, double y)
        => x * x + y * y;

      /// <summary>Lerp is a linear interpolation between point a (unit interval = 0.0) and point b (unit interval = 1.0).</summary>
      public static void Lerp(double x1, double y1, double x2, double y2, out double rx, out double ry, double unitInterval = 0.5)
      {
        var imu = 1 - unitInterval;

        rx = x1 * imu + x2 * unitInterval;
        ry = y1 * imu + y2 * unitInterval;
      }

      /// <summary>Returns the X-slope of the line to the point (x, y).</summary>
      public static double LineSlopeX(double x, double y)
        => System.Math.CopySign(x / y, x);
      /// <summary>Returns the Y-slope of the line to the point (x, y).</summary>
      public static double LineSlopeY(double x, double y)
        => System.Math.CopySign(y / x, y);

      /// <summary>Compute the Manhattan length (or magnitude) of the vector. Known as the Manhattan distance (i.e. from 0,0,0).</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
      public static double ManhattanDistance(double x1, double y1, double x2, double y2, double edgeLength = 1)
        => ManhattanLength(x2 - x1, y2 - y1, edgeLength);
      /// <summary>Compute the Manhattan length (or magnitude) of the vector. To compute the Manhattan distance between two vectors, ManhattanLength(target - source).</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
      public static double ManhattanLength(double x, double y, double edgeLength = 1)
        => System.Math.Abs(x / edgeLength) + System.Math.Abs(y / edgeLength);

      /// <summary>Returns a point -90 degrees perpendicular to the point, i.e. the point rotated 90 degrees counter clockwise. Only X and Y.</summary>
      public static void PerpendicularCcw(double x, double y, out double rx, out double ry)
      {
        rx = -y;
        ry = x;
      }
      /// <summary>Returns a point 90 degrees perpendicular to the point, i.e. the point rotated 90 degrees clockwise. Only X and Y.</summary>
      public static void PerpendicularCw(double x, double y, out double rx, out double ry)
      {
        rx = y;
        ry = -x;
      }

      /// <summary>Returns the perpendicular distance from the 2D point (x, y) to the to the 2D line (x1, y1) to (x2, y2).</summary>
      public static double PerpendicularDistance(double x, double y, double x1, double y1, double x2, double y2)
      {
        var x2x1 = x2 - x1;
        var y2y1 = y2 - y1;

        var xx1 = x - x1;
        var yy1 = y - y1;

        return EuclideanLength(x2x1 * xx1, y2y1 * yy1) / EuclideanLength(x2x1, y2y1);
      }

      /// <summary>Find foot of perpendicular from a point in 2D a plane to a line equation (ax+by+c=0).</summary>
      /// <see cref="https://www.geeksforgeeks.org/find-foot-of-perpendicular-from-a-point-in-2-d-plane-to-a-line/"/>
      /// <param name="a">Represents a of the line equation (ax+by+c=0).</param>
      /// <param name="b">Represents b of the line equation (ax+by+c=0).</param>
      /// <param name="c">Represents c of the line equation (ax+by+c=0).</param>
      /// <param name="source">A given point.</param>
      public static void PerpendicularFoot(double x, double y, double a, double b, double c, out double rx, out double ry)
      {
        var m = -1 * (a * x + b * y + c) / (a * a + b * b);

        rx = m * (a + x);
        ry = m * (b + y);
      }

      /// <summary>Returns the sign indicating whether the point is Left|On|Right of an infinite line (a to b). Through point1 and point2 the result has the meaning: greater than 0 is to the left of the line, equal to 0 is on the line, less than 0 is to the right of the line. (This is also known as an IsLeft function.)</summary>
      public static int SideTest(double x, double y, double x1, double y1, double x2, double y2)
        => System.Math.Sign((x - x2) * (y1 - y2) - (y - y2) * (x1 - x2));

      /// <summary>Slerp is a sherical linear interpolation between point a (unit interval = 0.0) and point b (unit interval = 1.0). Slerp travels the torque-minimal path, which means it travels along the straightest path the rounded surface of a sphere.</summary>>
      public static void Slerp(double x1, double y1, double x2, double y2, out double rx, out double ry, double unitInterval = 0.5)
      {
        var dot = System.Math.Clamp(DotProduct(x1, y1, x2, y2), -1.0, 1.0); // Ensure precision doesn't exceed acos limits.
        var theta = System.Math.Acos(dot) * unitInterval; // Angle between start and desired.
        var cos = System.Math.Cos(theta);
        var sin = System.Math.Sin(theta);

        rx = x1 * cos + ((x2 - x1) * dot) * sin;
        ry = y1 * cos + ((y2 - y1) * dot) * sin;
      }
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator CartesianCoordinate2(System.ValueTuple<double, double> xy)
        => new CartesianCoordinate2(xy.Item1, xy.Item2);

      public static bool operator ==(CartesianCoordinate2 a, CartesianCoordinate2 b)
        => a.Equals(b);
      public static bool operator !=(CartesianCoordinate2 a, CartesianCoordinate2 b)
        => !a.Equals(b);
      #endregion Overloaded operators

      #region Implemented interfaces
      // IEquatable
      public bool Equals(CartesianCoordinate2 other)
        => m_x == other.m_x && m_y == other.m_y;
      #endregion Implemented interfaces

      #region Object overrides
      public override bool Equals(object? obj)
        => obj is CartesianCoordinate2 o && Equals(o);
      public override int GetHashCode()
        => System.HashCode.Combine(m_x, m_y);
      public override string ToString()
        => $"<{GetType().Name}: {m_x} x, {m_y} y, ({EuclideanLength(m_x, m_y)} length)>";
      #endregion Object overrides
    }
  }
}
