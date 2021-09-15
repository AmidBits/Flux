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
        => new CylindricalCoordinate(System.Math.Sqrt(m_x * m_x + m_y * m_y), (System.Math.Atan2(m_y, m_x) + Maths.PiX2) % Maths.PiX2, m_z);
      public PolarCoordinate ToPolarCoordinate()
        => new PolarCoordinate(System.Math.Sqrt(m_x * m_x + m_y * m_y), System.Math.Atan2(m_y, m_x));
      public SphericalCoordinate ToSphericalCoordinate()
      {
        var x2y2 = m_x * m_x + m_y * m_y;
        return new SphericalCoordinate(System.Math.Sqrt(x2y2 + m_z * m_z), System.Math.Atan2(System.Math.Sqrt(x2y2), m_z) + System.Math.PI, System.Math.Atan2(m_y, m_x) + System.Math.PI);
      }

      #region Static methods
      public static double GetEuclideanLength(double x, double y, double z)
        => System.Math.Sqrt(GetEuclideanLengthSquared(x, y, z));
      public static double GetEuclideanLengthSquared(double x, double y, double z)
        => x * x + y * y + z * z;

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
        => $"<{GetType().Name}: {m_x} x, {m_y} y, {m_z} z, ({GetEuclideanLength(m_x, m_y, m_z)} length)>";
      #endregion Object overrides
    }
  }
}
