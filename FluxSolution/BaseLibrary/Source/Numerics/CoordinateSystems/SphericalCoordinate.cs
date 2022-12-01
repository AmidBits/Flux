namespace Flux
{
  /// <summary>Spherical coordinate.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Spherical_coordinate_system"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct SphericalCoordinate<TSelf>
    : ISphericalCoordinate<TSelf>
    where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
  {
    private readonly TSelf m_radius;
    private readonly TSelf m_inclination;
    private readonly TSelf m_azimuth;

    public SphericalCoordinate(TSelf radius, TSelf inclination, TSelf azimuth)
    {
      m_radius = radius;
      m_inclination = inclination;
      m_azimuth = azimuth;
    }

    public TSelf Radius { get => m_radius; init => m_radius = value; }
    public TSelf Inclination { get => m_inclination; init => m_inclination = value; }
    public TSelf Azimuth { get => m_azimuth; init => m_azimuth = value; }

    public TSelf Elevation { get => ConvertInclinationToElevation(m_inclination); init => m_inclination = ConvertElevationToInclination(value); }

    /// <summary>Converts the <see cref="SphericalCoordinate"/> to a <see cref="Vector3">CartesianCoordinate3</see>.</summary>
    public CartesianCoordinate3<TSelf> ToCartesianCoordinate3()
    {
      var sinInclination = TSelf.Sin(m_inclination);

      return new(
        m_radius * TSelf.Cos(m_azimuth) * sinInclination,
        m_radius * TSelf.Sin(m_azimuth) * sinInclination,
        m_radius * TSelf.Cos(m_inclination)
      );
    }

    /// <summary>Converts the <see cref="SphericalCoordinate"/> to a <see cref="CylindricalCoordinate"/>.</summary>
    public CylindricalCoordinate<TSelf> ToCylindricalCoordinate()
      => new(
        m_radius * TSelf.Sin(m_inclination),
        m_azimuth,
        m_radius * TSelf.Cos(m_inclination)
      );

    /// <summary>Converts the <see cref="SphericalCoordinate"/> to a <see cref="GeographicCoordinate"/>.</summary>
    public GeographicCoordinate ToGeographicCoordinate()
     => new GeographicCoordinate(
       Angle.ConvertRadianToDegree(double.CreateChecked(TSelf.Pi - m_inclination - TSelf.Pi.Divide(2))),
       Angle.ConvertRadianToDegree(double.CreateChecked(m_azimuth - TSelf.Pi)),
       double.CreateChecked(m_radius)
     );

    #region Static methods

    /// <summary>Converting from inclination to elevation is simply a quarter turn (PI / 2) minus the inclination.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static TSelf ConvertInclinationToElevation(TSelf inclination)
      => TSelf.Pi.Divide(2) - inclination;

    /// <summary>Converting from elevation to inclination is simply a quarter turn (PI / 2) minus the elevation.</summary>
    [System.Diagnostics.Contracts.Pure]
    public static TSelf ConvertElevationToInclination(TSelf elevation)
      => TSelf.Pi.Divide(2) - elevation;

    /// <summary>Return the <see cref="ISphericalCoordinate"/> from the specified components.</summary>
    public static SphericalCoordinate<TSelf> From(Length radius, Angle inclination, Azimuth azimuth)
      => new(
        TSelf.CreateChecked(radius.Value),
        TSelf.CreateChecked(inclination.Value),
        TSelf.CreateChecked(azimuth.ToRadians())
      );

    #endregion Static methods
  }
}
