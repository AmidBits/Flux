namespace Flux
{
  /// <summary>Polar coordinate. Please note that polar coordinates are two dimensional.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Polar_coordinate_system"/>
  public struct PolarCoord
    : System.IEquatable<PolarCoord>
  {
    private readonly double m_radius;
    private readonly Quantity.Angle m_angle;

    public PolarCoord(double radius, Quantity.Angle angle)
    {
      m_radius = radius;
      m_angle = angle;
    }

    public double Radius
      => m_radius;
    public Quantity.Angle Angle
      => m_angle;

    public CartesianCoord ToCartesianCoord()
    {
      var (x, y) = ConvertToCartesianCoordinate(m_radius, m_angle.Radian);

      return new CartesianCoord(x, y, 0);
    }

    #region Static methods
    public static (double x, double y) ConvertToCartesianCoordinate(double radius, double angleRad)
      => (radius * System.Math.Cos(angleRad), radius * System.Math.Sin(angleRad));
    #endregion Static methods

    #region Overloaded operators
    public static bool operator ==(PolarCoord a, PolarCoord b)
      => a.Equals(b);
    public static bool operator !=(PolarCoord a, PolarCoord b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(PolarCoord other)
      => m_angle == other.m_angle && m_radius == other.m_radius;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is PolarCoord o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_angle, m_radius);
    public override string ToString()
      => $"<{GetType().Name}: {m_radius}, {m_angle.ToUnitValue(Quantity.AngleUnit.Degree)}{Quantity.Angle.DegreeSymbol}>";
    #endregion Object overrides
  }
}
