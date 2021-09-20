namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns the cross product of two 3D vectors as out variables.</summary>
    public static CoordinateSystems.CartesianCoordinate3 CrossProduct(this CoordinateSystems.CartesianCoordinate3 cc1, CoordinateSystems.CartesianCoordinate3 cc2)
    {
      CoordinateSystems.CartesianCoordinate3.CrossProduct(cc1.X, cc1.Y, cc1.Z, cc2.X, cc2.Y, cc2.Z, out var x, out var y, out var z);

      return new CoordinateSystems.CartesianCoordinate3(x, y, z);
    }

    /// <summary>Returns the dot product of two 3D vectors.</summary>
    public static double DotProduct(this CoordinateSystems.CartesianCoordinate3 cc1, CoordinateSystems.CartesianCoordinate3 cc2)
      => CoordinateSystems.CartesianCoordinate3.DotProduct(cc1.X, cc1.Y, cc1.Z, cc2.X, cc2.Y, cc2.Z);

    /// <summary>Compute the scalar triple product, i.e. dot(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Scalar_triple_product"/>
    public static double ScalarTripleProduct(this CoordinateSystems.CartesianCoordinate3 cc1, CoordinateSystems.CartesianCoordinate3 cc2, CoordinateSystems.CartesianCoordinate3 cc3)
      => DotProduct(cc1, CrossProduct(cc2, cc3));

    /// <summary>Create a new vector by computing the vector triple product, i.e. cross(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Vector_triple_product"/>
    public static CoordinateSystems.CartesianCoordinate3 VectorTripleProduct(this CoordinateSystems.CartesianCoordinate3 cc1, CoordinateSystems.CartesianCoordinate3 cc2, CoordinateSystems.CartesianCoordinate3 cc3)
      => CrossProduct(cc1, CrossProduct(cc2, cc3));

    public static CoordinateSystems.CartesianCoordinate3 ToCartesianCoordinate3(this System.Numerics.Vector3 source)
      => new CoordinateSystems.CartesianCoordinate3(source.X, source.Y, source.Z);
    public static System.Numerics.Vector3 ToVector3(this CoordinateSystems.CartesianCoordinate3 source)
      => new System.Numerics.Vector3((float)source.X, (float)source.Y, (float)source.Z);
  }

  namespace CoordinateSystems
  {
    /// <summary>Cartesian coordinate.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Cartesian_coordinate_system"/>
    public struct CartesianCoordinate3
      : System.IEquatable<CartesianCoordinate3>
    {
      private readonly double m_x;
      private readonly double m_y;
      private readonly double m_z;

      public CartesianCoordinate3(double x, double y, double z)
      {
        m_x = x;
        m_y = y;
        m_z = z;
      }

      public double X
        => m_x;
      public double Y
        => m_y;
      public double Z
        => m_z;

      public CylindricalCoordinate ToCylindricalCoordinate()
        => new CylindricalCoordinate(System.Math.Sqrt(m_x * m_x + m_y * m_y), (System.Math.Atan2(m_y, m_x) + Maths.PiX2) % Maths.PiX2, m_z);
      public PolarCoordinate ToPolarCoordinate()
        => new PolarCoordinate(System.Math.Sqrt(m_x * m_x + m_y * m_y), System.Math.Atan2(m_y, m_x));
      public SphericalCoordinate ToSphericalCoordinate()
      {
        var x2y2 = m_x * m_x + m_y * m_y;
        return new SphericalCoordinate(System.Math.Sqrt(x2y2 + m_z * m_z), System.Math.Atan2(System.Math.Sqrt(x2y2), m_z) + System.Math.PI, System.Math.Atan2(m_y, m_x) + System.Math.PI);
      }

      #region Static methods
      /// <summary>(3D) Calculate the angle between the source vector and the specified target vector.
      /// When dot eq 0 then the vectors are perpendicular.
      /// When dot gt 0 then the angle is less than 90 degrees (dot=1 can be interpreted as the same direction).
      /// When dot lt 0 then the angle is greater than 90 degrees (dot=-1 can be interpreted as the opposite direction).
      /// </summary>
      public static double AngleBetween(double x1, double y1, double z1, double x2, double y2, double z2)
        => System.Math.Acos(System.Math.Clamp(DotProduct(x1, y1, z1, x2, y2, z2) / (EuclideanLength(x1, y1, z1) * EuclideanLength(x2, y2, z2)), -1, 1));

      /// <summary>Returns the angle to the 3D X-axis.</summary>
      public static double AngleToAxisX(double x, double y, double z)
        => System.Math.Atan2(System.Math.Sqrt(y * y + z * z), x);
      /// <summary>Returns the angle to the 3D Y-axis.</summary>
      public static double AngleToAxisY(double x, double y, double z)
        => System.Math.Atan2(System.Math.Sqrt(z * z + x * x), y);
      /// <summary>Returns the angle to the 3D Z-axis.</summary>
      public static double AngleToAxisZ(double x, double y, double z)
        => System.Math.Atan2(System.Math.Sqrt(x * x + y * y), z);

      /// <summary>Returns the cross product of two 3D vectors as out variables.</summary>
      public static void CrossProduct(double x1, double y1, double z1, double x2, double y2, double z2, out double rx, out double ry, out double rz)
      {
        rx = y1 * z2 - z1 * y2;
        ry = z1 * x2 - x1 * z2;
        rz = x1 * y2 - y1 * x2;
      }

      /// <summary>Returns the dot product of two 3D vectors.</summary>
      public static double DotProduct(double x1, double y1, double z1, double x2, double y2, double z2)
        => x1 * x2 + y1 * y2 + z1 * z2;

      /// <summary>Compute the Chebyshev distance from vector a to vector b.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
      public static double ChebyshevDistance(double x1, double y1, double z1, double x2, double y2, double z2, double edgeLength = 1)
        => ChebyshevLength(x2 - x1, y2 - y1, z2 - z1, edgeLength);
      /// <summary>Compute the Chebyshev length of the vector. To compute the Chebyshev distance between two vectors, ChebyshevLength(target - source).</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
      public static double ChebyshevLength(double x, double y, double z, double edgeLength = 1)
        => Maths.Max(System.Math.Abs(x / edgeLength), System.Math.Abs(y / edgeLength), System.Math.Abs(z / edgeLength));

      /// <summary>Compute the Euclidean distance from vector a to vector b.</summary>
      public static double EuclideanDistance(double x1, double y1, double z1, double x2, double y2, double z2)
        => EuclideanLength(x2 - x1, y2 - y1, z2 - z1);
      /// <summary>Compute the Euclidean length of the vector.</summary>
      public static double EuclideanLength(double x, double y, double z)
        => System.Math.Sqrt(EuclideanLengthSquared(x, y, z));
      /// <summary>Compute the Euclidean length squared of the vector.</summary>
      public static double EuclideanLengthSquared(double x, double y, double z)
        => x * x + y * y + z * z;

      /// <summary>Lerp is a linear interpolation between point a (unit interval = 0.0) and point b (unit interval = 1.0).</summary>
      public static void Lerp(double x1, double y1, double z1, double x2, double y2, double z2, out double rx, out double ry, out double rz, double unitInterval = 0.5)
      {
        var imu = 1 - unitInterval;

        rx = x1 * imu + x2 * unitInterval;
        ry = y1 * imu + y2 * unitInterval;
        rz = z1 * imu + z2 * unitInterval;
      }

      /// <summary>Compute the Manhattan length (or magnitude) of the vector. Known as the Manhattan distance (i.e. from 0,0,0).</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
      public static double ManhattanDistance(double x1, double y1, double z1, double x2, double y2, double z2, double edgeLength = 1)
        => ManhattanLength(x2 - x1, y2 - y1, z2 - z1, edgeLength);
      /// <summary>Compute the Manhattan length (or magnitude) of the vector. To compute the Manhattan distance between two vectors, ManhattanLength(target - source).</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
      public static double ManhattanLength(double x, double y, double z, double edgeLength = 1)
        => System.Math.Abs(x / edgeLength) + System.Math.Abs(y / edgeLength) + System.Math.Abs(z / edgeLength);

      /// <summary>Always works if the input is non-zero. Does not require the input to be normalized, and does not normalize the output.</summary>
      /// <see cref="http://lolengine.net/blog/2013/09/21/picking-orthogonal-vector-combing-coconuts"/>
      public static void Orthogonal(double x, double y, double z, out double rx, out double ry, out double rz)
      {
        if (System.Math.Abs(x) > System.Math.Abs(z))
        {
          rx = -y;
          ry = x;
          rz = 0;
        }
        else
        {
          rx = 0;
          ry = -x;
          rz = y;
        }
      }

      /// <summary>Compute the scalar triple product, i.e. dot(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Scalar_triple_product"/>
      public static double ScalarTripleProduct(double x1, double y1, double z1, double x2, double y2, double z2, double x3, double y3, double z3)
      {
        CrossProduct(x2, y2, z2, x3, y3, z3, out var x23, out var y23, out var z23);

        return DotProduct(x1, y1, z1, x23, y23, z23);
      }

      /// <summary>Slerp travels the torque-minimal path, which means it travels along the straightest path the rounded surface of a sphere.</summary>>
      public static void Slerp(double x1, double y1, double z1, double x2, double y2, double z2, out double rx, out double ry, out double rz, double unitInterval = 0.5)
      {
        var dot = System.Math.Clamp(DotProduct(x1, y1, z1, x2, y2, z2), -1.0, 1.0); // Ensure precision doesn't exceed acos limits.
        var theta = System.Math.Acos(dot) * unitInterval; // Angle between start and desired.
        var cos = System.Math.Cos(theta);
        var sin = System.Math.Sin(theta);

        rx = x1 * cos + ((x2 - x1) * dot) * sin;
        ry = y1 * cos + ((y2 - y1) * dot) * sin;
        rz = z1 * cos + ((z2 - z1) * dot) * sin;
      }

      /// <summary>Create a new vector by computing the vector triple product, i.e. cross(a, cross(b, c)), of the vector (a) and the vectors b and c.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Triple_product#Vector_triple_product"/>
      public static void VectorTripleProduct(double x1, double y1, double z1, double x2, double y2, double z2, double x3, double y3, double z3, out double rx, out double ry, out double rz)
      {
        CrossProduct(x2, y2, z2, x3, y3, z3, out var x23, out var y23, out var z23);
        CrossProduct(x1, y1, z1, x23, y23, z23, out rx, out ry, out rz);
      }
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator CartesianCoordinate3(System.ValueTuple<double, double, double> xyz)
        => new CartesianCoordinate3(xyz.Item1, xyz.Item2, xyz.Item3);

      public static bool operator ==(CartesianCoordinate3 a, CartesianCoordinate3 b)
        => a.Equals(b);
      public static bool operator !=(CartesianCoordinate3 a, CartesianCoordinate3 b)
        => !a.Equals(b);
      #endregion Overloaded operators

      #region Implemented interfaces
      // IEquatable
      public bool Equals(CartesianCoordinate3 other)
        => m_x == other.m_x && m_y == other.m_y && m_z == other.m_z;
      #endregion Implemented interfaces

      #region Object overrides
      public override bool Equals(object? obj)
        => obj is CartesianCoordinate3 o && Equals(o);
      public override int GetHashCode()
        => System.HashCode.Combine(m_x, m_y, m_z);
      public override string ToString()
        => $"<{GetType().Name}: {m_x} x, {m_y} y, {m_z} z, ({EuclideanLength(m_x, m_y, m_z)} length)>";
      #endregion Object overrides
    }
  }
}
