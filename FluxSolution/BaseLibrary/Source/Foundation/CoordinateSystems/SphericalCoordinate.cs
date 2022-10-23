namespace Flux
{
  /// <summary>Spherical coordinate.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Spherical_coordinate_system"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public record struct SphericalCoordinate
    : ISphericalCoordinate
  {
    private readonly double m_radius;
    private readonly double m_inclination;
    private readonly double m_azimuth;

    public SphericalCoordinate(double radius, double inclination, double azimuth)
    {
      m_radius = radius;
      m_inclination = inclination;
      m_azimuth = azimuth;
    }

    /// <summary>Radial distance (to origin) or radial coordinate, in meters.</summary>
    [System.Diagnostics.Contracts.Pure] public double Radius { get => m_radius; init => m_radius = value; }
    /// <summary>Polar angle or angular coordinate, in radians.</summary>
    [System.Diagnostics.Contracts.Pure] public double Inclination { get => m_inclination; init => m_inclination = value; }
    /// <summary>Azimuthal angle, in radians.</summary>
    [System.Diagnostics.Contracts.Pure] public double Azimuth { get => m_azimuth; init => m_azimuth = value; }

    /// <summary>Converts the <see cref="SphericalCoordinate"/> to a <see cref="CartesianCoordinate3R"/>.</summary>
    [System.Diagnostics.Contracts.Pure] public ICartesianCoordinate3 ToCartesianCoordinate3() => ((ISphericalCoordinate)this).ToCartesianCoordinate3();

    /// <summary>Converts the <see cref="SphericalCoordinate"/> to a <see cref="CylindricalCoordinate"/>.</summary>
    [System.Diagnostics.Contracts.Pure] public ICylindricalCoordinate ToCylindricalCoordinate() => ((ISphericalCoordinate)this).ToCylindricalCoordinate();

    /// <summary>Converts the <see cref="SphericalCoordinate"/> to a <see cref="GeographicCoordinate"/>.</summary>
    [System.Diagnostics.Contracts.Pure] public IGeographicCoordinate ToGeographicCoordinate() => ((ISphericalCoordinate)this).ToGeographicCoordinate();

    #region Static methods
    /// <summary>Converting from inclination to elevation is simply a quarter turn (PI / 2) minus the inclination.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static double ConvertInclinationToElevation(double inclination)
      => System.Math.PI / 2 - inclination;

    /// <summary>Converting from elevation to inclination is simply a quarter turn (PI / 2) minus the elevation.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static double ConvertElevationToInclination(double elevation)
      => System.Math.PI / 2 - elevation;
    #endregion Static methods

    #region Implemented interfaces
    // ISphericalCoordinate
    public ISphericalCoordinate Create(double radius, double inclination, double azimuth)
     => new SphericalCoordinate(radius, inclination, azimuth);
    #endregion Implemented interfaces
  }
}
