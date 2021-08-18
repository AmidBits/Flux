namespace Flux.CoordinateSystems
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
    public CartesianCoordinate3(System.ValueTuple<double, double, double> xyz)
      : this(xyz.Item1, xyz.Item2, xyz.Item3)
    { }

    public double X
      => m_x;
    public double Y
      => m_y;
    public double Z
      => m_z;

    public CylindricalCoordinate ToCylindricalCoordinate()
      => new CylindricalCoordinate(ConvertToCylindricalCoordinate(m_x, m_y, m_z));
    public PolarCoordinate ToPolarCoordinate()
      => new PolarCoordinate(ConvertToPolarCoordinate(m_x, m_y));
    public SphericalCoordinate ToSphericalCoordinate()
      => new SphericalCoordinate(ConvertToSphericalCoordinate(m_x, m_y, m_z));

    #region Static methods
    public static (double radius, double azimuthRad, double height) ConvertToCylindricalCoordinate(double x, double y, double z)
      => (System.Math.Sqrt(x * x + y * y), System.Math.Atan2(y, x), z);
    public static (double radius, double azimuthRad) ConvertToPolarCoordinate(double x, double y)
      => (System.Math.Sqrt(x * x + y * y), System.Math.Atan2(y, x));
    public static (double radius, double inclinationRad, double azimuthRad) ConvertToSphericalCoordinate(double x, double y, double z)
    {
      var x2y2 = x * x + y * y;
      return (System.Math.Sqrt(x2y2 + z * z), System.Math.Atan2(System.Math.Sqrt(x2y2), z) + System.Math.PI, System.Math.Atan2(y, x) + System.Math.PI);
    }
    #endregion Static methods

    #region Overloaded operators
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
      => $"<{GetType().Name}: {m_x}, {m_y}, {m_z}>";
    #endregion Object overrides
  }
}
