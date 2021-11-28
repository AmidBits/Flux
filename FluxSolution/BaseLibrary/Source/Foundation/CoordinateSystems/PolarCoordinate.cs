namespace Flux
{
  /// <summary>Polar coordinate. Please note that polar coordinates are two dimensional.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Polar_coordinate_system"/>
#if NET5_0
  public struct PolarCoordinate
    : System.IEquatable<PolarCoordinate>
#elif NET6_0
  public record struct PolarCoordinate
#endif
  {
    private readonly double m_radius;
    private readonly Quantity.Angle m_azimuth;

    public PolarCoordinate(double radius, double azimuthRad)
    {
      m_radius = radius;
      m_azimuth = new Quantity.Angle(azimuthRad);
    }

    /// <summary>Radial distance (to origin) or radial coordinate.</summary>
    public double Radius { get => m_radius; }
    /// <summary>Polar angle or angular coordinate.</summary>
    public Quantity.Angle Azimuth { get => m_azimuth; }

    public CartesianCoordinate2 ToCartesianCoordinate2()
    {
      var radAzimuth = m_azimuth.Value;
      return new CartesianCoordinate2(m_radius * System.Math.Cos(radAzimuth), m_radius * System.Math.Sin(radAzimuth));
    }

    #region Overloaded operators
#if NET5_0
    public static bool operator ==(PolarCoordinate a, PolarCoordinate b)
      => a.Equals(b);
    public static bool operator !=(PolarCoordinate a, PolarCoordinate b)
      => !a.Equals(b);
#endif
    #endregion Overloaded operators

    #region Implemented interfaces
#if NET5_0
    // IEquatable
    public bool Equals(PolarCoordinate other)
      => m_azimuth == other.m_azimuth && m_radius == other.m_radius;
#endif
    #endregion Implemented interfaces

    #region Object overrides
#if NET5_0
    public override bool Equals(object? obj)
      => obj is PolarCoordinate o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(m_azimuth, m_radius);
#endif
    public override string ToString()
      => $"{GetType().Name} {{ Radius = {m_radius}, Azimuth = {m_azimuth.ToUnitValue(Quantity.AngleUnit.Degree):N1}{Quantity.Angle.DegreeSymbol} }}";
    #endregion Object overrides
  }
}
