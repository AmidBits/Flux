namespace Flux
{
  /// <summary>Cartesian coordinate.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Cartesian_coordinate_system"/>
  public struct CartesianCoord
    : System.IEquatable<CartesianCoord>
  {
    private readonly double m_x;
    private readonly double m_y;
    private readonly double m_z;

    public CartesianCoord(double x, double y, double z)
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

    public CylindricalCoord ToCylindricalCoord()
    //=> new CylindricalCoord(System.Math.Sqrt(System.Math.Pow(m_x, 2) + System.Math.Pow(m_y, 2)), new Quantity.Angle(System.Math.Atan2(m_y, m_x)), m_z);
    {
      var (radius, azimuthRad, height) = ConvertToCylindricalCoordinate(m_x, m_y, m_z);

      return new CylindricalCoord(radius, new Quantity.Angle(azimuthRad), height);
    }
    public PolarCoord ToPolarCoord()
    {
      var (radius, angle) = ConvertToPolarCoordinate(m_x, m_y);

      return new PolarCoord(radius, new Quantity.Angle(angle));
    }
    public SphericalCoord ToSphericalCoord()
    {
      var (radius, inclinationRad, azimuthRad) = ConvertToSphericalCoordinate(m_x, m_y, m_z);

      return new SphericalCoord(radius, new Quantity.Angle(inclinationRad), new Quantity.Angle(azimuthRad));
    }

    #region Static methods
    public static (double radius, double azimuthRad, double height) ConvertToCylindricalCoordinate(double x, double y, double z)
      => (System.Math.Sqrt(x * x + y * y), System.Math.Atan2(y, x), z);
    public static (double radius, double angleRad) ConvertToPolarCoordinate(double x, double y)
      => (System.Math.Sqrt(x * x + y * y), System.Math.Atan2(y, x));
    public static (double radius, double inclinationRad, double azimuthRad) ConvertToSphericalCoordinate(double x, double y, double z)
    {
      var x2y2 = x * x + y * y;

      return (System.Math.Sqrt(x2y2 + z * z), System.Math.Atan2(System.Math.Sqrt(x2y2), z), System.Math.Atan2(y, x));
    }
    #endregion Static methods

    #region Overloaded operators
    public static bool operator ==(CartesianCoord a, CartesianCoord b)
      => a.Equals(b);
    public static bool operator !=(CartesianCoord a, CartesianCoord b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(CartesianCoord other)
      => m_x == other.m_x && m_y == other.m_y && m_z == other.m_z;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is CartesianCoord o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_x, m_y, m_z);
    public override string ToString()
      => $"<{GetType().Name}: {m_x}, {m_y}, {m_z}>";
    #endregion Object overrides
  }
}
