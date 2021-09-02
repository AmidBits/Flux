namespace Flux.CoordinateSystems
{
  /// <summary>Polar coordinate. Please note that polar coordinates are two dimensional.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Polar_coordinate_system"/>
  public struct PolarCoordinate
    : System.IEquatable<PolarCoordinate>
  {
    private readonly double m_radius;
    private readonly Quantity.Angle m_azimuth;

    public PolarCoordinate(double radius, double azimuthRad)
    {
      m_radius = radius;
      m_azimuth = new Quantity.Angle(azimuthRad);
    }
    public PolarCoordinate(System.ValueTuple<double, double> radius_azimuthRad)
      : this(radius_azimuthRad.Item1, radius_azimuthRad.Item2)
    { }

    /// <summary>Radial distance (to origin) or radial coordinate.</summary>
    public double Radius
      => m_radius;
    /// <summary>Polar angle or angular coordinate.</summary>
    public Quantity.Angle Azimuth
      => m_azimuth;

    public CartesianCoordinate2 ToCartesianCoordinate2()
      => new CartesianCoordinate2(ConvertToCartesianCoordinate2(m_radius, m_azimuth.Radian));

    #region Static methods
    public static (double x, double y) ConvertToCartesianCoordinate2(double radius, double azimuthRad)
      => (radius * System.Math.Cos(azimuthRad), radius * System.Math.Sin(azimuthRad));
    #endregion Static methods

    #region Overloaded operators
    public static bool operator ==(PolarCoordinate a, PolarCoordinate b)
      => a.Equals(b);
    public static bool operator !=(PolarCoordinate a, PolarCoordinate b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(PolarCoordinate other)
      => m_azimuth == other.m_azimuth && m_radius == other.m_radius;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is PolarCoordinate o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_azimuth, m_radius);
    public override string ToString()
      => $"<{GetType().Name}: radius = {m_radius}, azimuth = {m_azimuth.Degree:N1}{Quantity.Angle.DegreeSymbol}>";
    #endregion Object overrides
  }
}
