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

    /// <summary>Converts the <see cref="SphericalCoordinate"/> to a <see cref="Vector3">CartesianCoordinate3</see>.</summary>
    public Vector3 ToCartesianCoordinate3()
    {
      var sinInclination = System.Math.Sin(Inclination);

      return new Vector3(
        Radius * System.Math.Cos(Azimuth) * sinInclination,
        Radius * System.Math.Sin(Azimuth) * sinInclination,
        Radius * System.Math.Cos(Inclination)
      );
    }

    /// <summary>Converts the <see cref="SphericalCoordinate"/> to a <see cref="CylindricalCoordinate"/>.</summary>
    public CylindricalCoordinate ToCylindricalCoordinate()
      => new CylindricalCoordinate(
        Radius * System.Math.Sin(Inclination),
        Azimuth,
        Radius * System.Math.Cos(Inclination)
      );

    /// <summary>Converts the <see cref="SphericalCoordinate"/> to a <see cref="GeographicCoordinate"/>.</summary>
    public GeographicCoordinate ToGeographicCoordinate()
     => new GeographicCoordinate(
       Angle.ConvertRadianToDegree(System.Math.PI - Inclination - System.Math.PI / 2),
       Angle.ConvertRadianToDegree(Azimuth - System.Math.PI),
       Radius
     );

    #region Static methods
    /// <summary>Converting from inclination to elevation is simply a quarter turn (PI / 2) minus the inclination.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static double ConvertInclinationToElevation(double inclination)
      => System.Math.PI / 2 - inclination;

    /// <summary>Converting from elevation to inclination is simply a quarter turn (PI / 2) minus the elevation.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static double ConvertElevationToInclination(double elevation)
      => System.Math.PI / 2 - elevation;

    /// <summary>Return the <see cref="ISphericalCoordinate"/> from the specified components.</summary>
    public static SphericalCoordinate From(Length radius, Angle inclination, Azimuth azimuth)
      => new SphericalCoordinate(radius.Value, inclination.Value, azimuth.ToRadians());
    #endregion Static methods

    public override string ToString()
      => $"{GetType().Name} {{ Radius = {m_radius}, Inclination = {new Angle(m_inclination).ToUnitString(AngleUnit.Degree, "N1", true)} (Elevation = {new Angle(ConvertInclinationToElevation(m_inclination)).ToUnitString(AngleUnit.Degree, "N1", true)}), Azimuth = {new Angle(m_azimuth).ToUnitString(AngleUnit.Degree, "N1", true)} }}";
  }
}
