namespace Flux
{
  /// <summary>Spherical coordinate.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Spherical_coordinate_system"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly struct SphericalCoordinate
    : System.IEquatable<SphericalCoordinate>, ISphericalCoordinate
  {
    private readonly double m_radius;
    private readonly double m_radInclination;
    private readonly double m_radAzimuth;

    public SphericalCoordinate(double radius, double radInclination, double radAzimuth)
    {
      m_radius = radius;
      m_radInclination = radInclination;
      m_radAzimuth = radAzimuth;
    }

    /// <summary>Radial distance (to origin) or radial coordinate, in meters.</summary>
    [System.Diagnostics.Contracts.Pure] public Length Radius { get => new(m_radius); init => m_radius = value.Value; }
    /// <summary>Polar angle or angular coordinate, in radians.</summary>
    [System.Diagnostics.Contracts.Pure] public Angle Inclination { get => new(m_radInclination); init => m_radInclination = value.Value; }
    /// <summary>Azimuthal angle, in radians.</summary>
    [System.Diagnostics.Contracts.Pure] public Azimuth Azimuth { get => Azimuth.FromRadians(m_radAzimuth); init => m_radAzimuth = value.ToRadians(); }

    /// <summary>Converts the <see cref="SphericalCoordinate"/> to a <see cref="CartesianCoordinate3R"/>.</summary>
    [System.Diagnostics.Contracts.Pure]
    public CartesianCoordinate3R ToCartesianCoordinate3R()
    {
      var sinInclination = System.Math.Sin(m_radInclination);

      return new(m_radius * System.Math.Cos(m_radAzimuth) * sinInclination, m_radius * System.Math.Sin(m_radAzimuth) * sinInclination, m_radius * System.Math.Cos(m_radInclination));
    }

    /// <summary>Converts the <see cref="SphericalCoordinate"/> to a <see cref="CylindricalCoordinate"/>.</summary>
    [System.Diagnostics.Contracts.Pure]
    public CylindricalCoordinate ToCylindricalCoordinate()
      => new(m_radius * System.Math.Sin(m_radInclination), m_radAzimuth, m_radius * System.Math.Cos(m_radInclination));

    /// <summary>Converts the <see cref="SphericalCoordinate"/> to a <see cref="GeographicCoordinate"/>.</summary>
    [System.Diagnostics.Contracts.Pure]
    public GeographicCoordinate ToGeographicCoordinate()
      => new(Angle.ConvertRadianToDegree(System.Math.PI - m_radInclination - Maths.PiOver2), Angle.ConvertRadianToDegree(m_radAzimuth - System.Math.PI), m_radius);

    #region Static methods
    /// <summary>Converting from inclination to elevation is simply a quarter turn (PI / 2) minus the inclination.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static double ConvertInclinationToElevation(double inclinationRad)
      => Maths.PiOver2 - inclinationRad;
    /// <summary>Converting from elevation to inclination is simply a quarter turn (PI / 2) minus the elevation.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static double ConvertElevationToInclination(double elevationRad)
      => Maths.PiOver2 - elevationRad;
    #endregion Static methods

    #region Overloaded operators
    [System.Diagnostics.Contracts.Pure] public static bool operator ==(SphericalCoordinate a, SphericalCoordinate b) => a.Equals(b);
    [System.Diagnostics.Contracts.Pure] public static bool operator !=(SphericalCoordinate a, SphericalCoordinate b) => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    [System.Diagnostics.Contracts.Pure] public bool Equals(SphericalCoordinate other) => m_radius == other.m_radius && m_radInclination == other.m_radInclination && m_radAzimuth == other.m_radAzimuth;
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure] public override bool Equals(object? obj) => obj is SphericalCoordinate o && Equals(o);
    [System.Diagnostics.Contracts.Pure] public override int GetHashCode() => System.HashCode.Combine(m_radius, m_radInclination, m_radAzimuth);
    [System.Diagnostics.Contracts.Pure] public override string ToString() => $"{GetType().Name} {{ Radius = {m_radius}, Inclination = {Inclination.ToUnitValue(AngleUnit.Degree):N1}\u00B0 (Elevation = {Angle.ConvertRadianToDegree(ConvertInclinationToElevation(m_radInclination)):N1}\u00B0), Azimuth = {Azimuth.ToAngle().ToUnitValue(AngleUnit.Degree):N1}\u00B0 }}";
    #endregion Object overrides
  }
}
