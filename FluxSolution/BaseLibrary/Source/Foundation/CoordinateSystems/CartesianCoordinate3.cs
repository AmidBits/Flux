namespace Flux
{
  public static partial class ExtensionMethods
  {
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
        => (CylindricalCoordinate)ConvertToCylindricalCoordinate(m_x, m_y, m_z);
      public PolarCoordinate ToPolarCoordinate()
        => (PolarCoordinate)ConvertToPolarCoordinate(m_x, m_y);
      public SphericalCoordinate ToSphericalCoordinate()
        => (SphericalCoordinate)ConvertToSphericalCoordinate(m_x, m_y, m_z);

      #region Static methods
      public static double ComputeEuclideanLength(double x, double y, double z)
        => System.Math.Sqrt(ComputeEuclideanLengthSquared(x, y, z));
      public static double ComputeEuclideanLengthSquared(double x, double y, double z)
        => x * x + y * y + z * z;

      public static (double radius, double azimuthRad, double height) ConvertToCylindricalCoordinate(double x, double y, double z)
      {
        var radius = System.Math.Sqrt(x * x + y * y);
        var azimuthRad = (System.Math.Atan2(y, x) + Maths.PiX2) % Maths.PiX2;
        var height = z;
        return (radius, azimuthRad, height);
      }
      public static (double radius, double azimuthRad) ConvertToPolarCoordinate(double x, double y)
        => (System.Math.Sqrt(x * x + y * y), System.Math.Atan2(y, x));
      public static (double radius, double inclinationRad, double azimuthRad) ConvertToSphericalCoordinate(double x, double y, double z)
      {
        var x2y2 = x * x + y * y;
        return (System.Math.Sqrt(x2y2 + z * z), System.Math.Atan2(System.Math.Sqrt(x2y2), z) + System.Math.PI, System.Math.Atan2(y, x) + System.Math.PI);
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
        => $"<{GetType().Name}: {m_x} x, {m_y} y, {m_z} z, ({ComputeEuclideanLength(m_x, m_y, m_z)} length)>";
      #endregion Object overrides
    }
  }
}
