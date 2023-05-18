namespace Flux.Numerics
{
  /// <summary>Spherical coordinate.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Spherical_coordinate_system"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct SphericalCoordinate
    : System.IFormattable, ISphericalCoordinate
  {
    public static readonly SphericalCoordinate Zero;

    private readonly double m_radius;
    private readonly double m_inclination;
    private readonly double m_azimuth;

    public SphericalCoordinate(double radius, double inclination, double azimuth)
    {
      m_radius = radius;
      m_inclination = inclination;
      m_azimuth = azimuth;
    }

    public void Deconstruct(out double radius, out double inclination, out double azimuth) { radius = m_radius; inclination = m_inclination; azimuth = m_azimuth; }

    public double Radius { get => m_radius; init => m_radius = value; }
    public double Inclination { get => m_inclination; init => m_inclination = value; }
    public double Azimuth { get => m_azimuth; init => m_azimuth = value; }

    public double Elevation { get => ConvertInclinationToElevation(m_inclination); init => m_inclination = ConvertElevationToInclination(value); }

    ///// <summary>Converts the <see cref="SphericalCoordinate"/> to a <see cref=" CartesianCoordinate3{double}">CartesianCoordinate3</see>.</summary>
    //public CartesianCoordinate3<double> ToCartesianCoordinate3()
    //{
    //  var (si, ci) = System.Math.SinCos(m_inclination);
    //  var (sa, ca) = System.Math.SinCos(m_azimuth);

    //  return new(
    //    m_radius * ca * si,
    //    m_radius * sa * si,
    //    m_radius * ci
    //  );
    //}

    ///// <summary>Converts the <see cref="SphericalCoordinate"/> to a <see cref="CylindricalCoordinate"/>.</summary>
    //public CylindricalCoordinate ToCylindricalCoordinate()
    //{
    //  var (si, ci) = System.Math.SinCos(m_inclination);

    //  return new(
    //    m_radius * si,
    //    m_azimuth,
    //    m_radius * ci
    //  );
    //}

    ///// <summary>Converts the <see cref="SphericalCoordinate"/> to a <see cref="GeographicCoordinate"/>.</summary>
    //public GeographicCoordinate ToGeographicCoordinate()
    // => new(
    //   Units.Angle.ConvertRadianToDegree(System.Math.PI - m_inclination - System.Math.PI / 2),
    //   Units.Angle.ConvertRadianToDegree(m_azimuth - System.Math.PI),
    //   m_radius
    // );

    #region Static methods

    /// <summary>Converting from inclination to elevation is simply a quarter turn (PI / 2) minus the inclination.</summary>

    public static double ConvertInclinationToElevation(double inclination)
      => System.Math.PI / 2 - inclination;

    /// <summary>Converting from elevation to inclination is simply a quarter turn (PI / 2) minus the elevation.</summary>

    public static double ConvertElevationToInclination(double elevation)
      => System.Math.PI / 2 - elevation;

    ///// <summary>Return the <see cref="ISphericalCoordinate"/> from the specified components.</summary>
    //public static SphericalCoordinate<TSelf> From(Quantities.Length radius, Quantities.Angle inclination, Azimuth azimuth)
    //  => new(
    //    TSelf.CreateChecked(radius.Value),
    //    TSelf.CreateChecked(inclination.Value),
    //    TSelf.CreateChecked(azimuth.ToRadians())
    //  );

    #endregion Static methods

    public string ToString(string? format, System.IFormatProvider? provider)
      => $"{GetType().GetNameEx()} {{ Radius = {string.Format($"{{0:{format ?? "N1"}}}", Radius)}, Inclination = {new Units.Angle(Inclination).ToUnitString(Units.AngleUnit.Degree, format ?? "N3", true)} (Elevation = {new Units.Angle(Elevation).ToUnitString(Units.AngleUnit.Degree, format ?? "N3", true)}), Azimuth = {new Units.Angle(Azimuth).ToUnitString(Units.AngleUnit.Degree, format ?? "N3", true)} }}";

    public override string ToString() => ToString(null, null);
  }
}
