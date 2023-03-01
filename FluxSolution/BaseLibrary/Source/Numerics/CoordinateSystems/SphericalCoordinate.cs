namespace Flux.Numerics
{
  /// <summary>Spherical coordinate.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Spherical_coordinate_system"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct SphericalCoordinate<TSelf>
    : ISphericalCoordinate<TSelf>
    where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
  {
    public static readonly SphericalCoordinate<TSelf> Zero;

    private readonly TSelf m_radius;
    private readonly TSelf m_inclination;
    private readonly TSelf m_azimuth;

    public SphericalCoordinate(TSelf radius, TSelf inclination, TSelf azimuth)
    {
      m_radius = radius;
      m_inclination = inclination;
      m_azimuth = azimuth;
    }

    public void Deconstruct(out TSelf radius, out TSelf inclination, out TSelf azimuth) { radius = m_radius; inclination = m_inclination; azimuth = m_azimuth; }

    public TSelf Radius { get => m_radius; init => m_radius = value; }
    public TSelf Inclination { get => m_inclination; init => m_inclination = value; }
    public TSelf Azimuth { get => m_azimuth; init => m_azimuth = value; }

    public TSelf Elevation { get => ConvertInclinationToElevation(m_inclination); init => m_inclination = ConvertElevationToInclination(value); }

    /// <summary>Converts the <see cref="SphericalCoordinate"/> to a <see cref=" CartesianCoordinate3{double}">CartesianCoordinate3</see>.</summary>
    public CartesianCoordinate3<TSelf> ToCartesianCoordinate3()
    {
      var (si, ci) = TSelf.SinCos(m_inclination);
      var (sa, ca) = TSelf.SinCos(m_azimuth);

      return new(
        m_radius * ca * si,
        m_radius * sa * si,
        m_radius * ci
      );
    }

    /// <summary>Converts the <see cref="SphericalCoordinate"/> to a <see cref="CylindricalCoordinate"/>.</summary>
    public CylindricalCoordinate<TSelf> ToCylindricalCoordinate()
    {
      var (si, ci) = TSelf.SinCos(m_inclination);

      return new(
        m_radius * si,
        m_azimuth,
        m_radius * ci
      );
    }

    /// <summary>Converts the <see cref="SphericalCoordinate"/> to a <see cref="GeographicCoordinate"/>.</summary>
    public GeographicCoordinate ToGeographicCoordinate()
     => new(
       Quantities.Angle.ConvertRadianToDegree(double.CreateChecked(TSelf.Pi - m_inclination - TSelf.Pi.Divide(2))),
       Quantities.Angle.ConvertRadianToDegree(double.CreateChecked(m_azimuth - TSelf.Pi)),
       double.CreateChecked(m_radius)
     );

    #region Static methods

    /// <summary>Converting from inclination to elevation is simply a quarter turn (PI / 2) minus the inclination.</summary>

    public static TSelf ConvertInclinationToElevation(TSelf inclination)
      => TSelf.Pi.Divide(2) - inclination;

    /// <summary>Converting from elevation to inclination is simply a quarter turn (PI / 2) minus the elevation.</summary>

    public static TSelf ConvertElevationToInclination(TSelf elevation)
      => TSelf.Pi.Divide(2) - elevation;

    ///// <summary>Return the <see cref="ISphericalCoordinate"/> from the specified components.</summary>
    //public static SphericalCoordinate<TSelf> From(Quantities.Length radius, Quantities.Angle inclination, Azimuth azimuth)
    //  => new(
    //    TSelf.CreateChecked(radius.Value),
    //    TSelf.CreateChecked(inclination.Value),
    //    TSelf.CreateChecked(azimuth.ToRadians())
    //  );

    #endregion Static methods

    public override string ToString() => ((ISphericalCoordinate<TSelf>)this).ToString(null, null);
  }
}
