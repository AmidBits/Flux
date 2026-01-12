namespace Flux.CoordinateSystems
{
  /// <summary>
  /// <para>Spherical coordinate.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Spherical_coordinate_system"/></para>
  /// </summary>
  /// <remarks>This implementation follows the ISO physics convention.</remarks>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct SphericalCoordinate
    : System.IFormattable
  {
    public static SphericalCoordinate Empty { get; }

    private readonly double m_radius;
    private readonly double m_inclination;
    private readonly double m_azimuth;

    public SphericalCoordinate(Units.Length radius, Units.Angle inclination, Units.Angle azimuth)
    {
      m_radius = radius.Value;
      m_inclination = inclination.Value;
      m_azimuth = azimuth.Value;
    }

    public SphericalCoordinate(double radiusValue, Units.LengthUnit radiusUnit, double inclinationValue, Units.AngleUnit inclinationUnit, double azimuthValue, Units.AngleUnit azimuthUnit)
    {
      m_radius = Units.Length.ConvertFromUnit(radiusUnit, radiusValue);
      m_inclination = Units.Angle.ConvertFromUnit(inclinationUnit, inclinationValue);
      m_azimuth = Units.Angle.ConvertFromUnit(azimuthUnit, azimuthValue);
    }

    public SphericalCoordinate(double radiusMeter, double inclinationRadian, double azimuthRadian)
    {
      m_radius = radiusMeter;
      m_inclination = inclinationRadian;
      m_azimuth = azimuthRadian;
    }

    public void Deconstruct(out double radiusMeter, out double inclinationRadian, out double azimuthRadian)
    {
      radiusMeter = m_radius;
      inclinationRadian = m_inclination;
      azimuthRadian = m_azimuth;
    }

    /// <summary>
    /// <para>The radius or radial distance is the Euclidean distance from the origin.</para>
    /// </summary>
    /// <remarks>If the radius is zero, both azimuth and inclination are arbitrary.</remarks>
    public Units.Length Radius { get => new(m_radius); init => m_radius = value.Value; }

    /// <summary>
    /// <para>The inclination (a.k.a. polar angle, colatitude, zenith angle, normal angle) is the signed angle from the zenith reference direction. (Elevation may be used as the polar angle instead of inclination). This is equivalent to latitudinal or vertical angle in geographical coordinate systems.</para>
    /// </summary>
    /// <remarks>If the inclination is zero or 180 degrees (PI radians), the azimuth is arbitrary.</remarks>
    public Units.Angle Inclination { get => new(m_inclination); init => m_inclination = value.Value; }

    /// <summary>
    /// <para>The azimuth (or azimuthal angle) is the signed angle measured from the azimuth reference direction to the orthogonal projection of the radial line segment on the reference plane. This is equivalent to longitudinal or horizontal angle in geographical coordinate systems.</para>
    /// </summary>
    public Units.Angle Azimuth { get => new(m_azimuth); init => m_azimuth = value.Value; }

    /// <summary>
    /// <para>The elevation is the signed angle from the x-y reference plane to the radial line segment, where positive angles are designated as upward, towards the zenith reference. This is an option/alternative to <see cref="Inclination"/>.</para>
    /// </summary>
    /// <remarks>The elevation angle is 90 degrees (PI/2 radians) minus the <see cref="Inclination"/> angle.</remarks>
    public Units.Angle Elevation { get => new(ConvertInclinationToElevation(m_inclination)); init => m_inclination = ConvertElevationToInclination(value.Value); }

    /// <summary>Creates new <see cref="CartesianCoordinate"/> from the <see cref="SphericalCoordinate"/>.</summary>
    public CartesianCoordinate ToCartesianCoordinate()
    {
      var (x, y, z) = ConvertSphericalByInclinationToCartesian3(m_inclination, m_azimuth, m_radius, m_radius, m_radius);

      return new(x, y, z);
    }

    /// <summary>Creates a new <see cref="CylindricalCoordinate"/> from the <see cref="SphericalCoordinate"/>.</summary>
    public CylindricalCoordinate ToCylindricalCoordinate()
    {
      var (si, ci) = double.SinCos(m_inclination);

      return new(
        m_radius * si, Units.LengthUnit.Meter,
        m_azimuth, Units.AngleUnit.Radian,
        m_radius * ci, Units.LengthUnit.Meter
      );
    }

    /// <summary>Creates a new <see cref="GeographicCoordinate"/> from the <see cref="SphericalCoordinate"/>.</summary>
    /// <remarks>All angles in radians.</remarks>
    public GeographicCoordinate ToGeographicCoordinate()
      => new(
        m_inclination - double.Pi / 2, Units.AngleUnit.Radian,
        m_azimuth, Units.AngleUnit.Radian,
        m_radius, Units.LengthUnit.Meter
      );

    /// <summary>Creates a new <see cref="System.Numerics.Vector3"/> from the <see cref="SphericalCoordinate"/>.</summary>
    /// <remarks>All angles in radians.</remarks>
    public System.Numerics.Vector3 ToVector3()
    {
      var (x, y, z) = ConvertSphericalByInclinationToCartesian3(m_inclination, m_azimuth, m_radius, m_radius, m_radius);

      return new((float)x, (float)y, (float)z);
    }

    #region Static methods

    public SphericalCoordinate CreateRandom(double radius, System.Random? rng = null)
    {
      rng ??= System.Random.Shared;

      return new(
        rng.NextDouble(radius),
        rng.NextDouble(double.Pi),
        rng.NextDouble(double.Tau)
      );
    }

    public SphericalCoordinate CreateRandomOnSurface(double radius, System.Random? rng = null)
    {
      rng ??= System.Random.Shared;

      return new(
        radius,
        rng.NextDouble(double.Pi),
        rng.NextDouble(double.Tau)
      );
    }

    #region Conversion methods

    /// <summary>
    /// <para>Creates cartesian 3D coordinates from the inclination and azimuth of a <see cref="SphericalCoordinate"/>, but with triaxial ellipsoid as three radii for the X (A), Y (B) and Z (C) axis.</para>
    /// <remarks>All angles in radians.</remarks>
    /// </summary>
    /// <param name="inclination"></param>
    /// <param name="azimuth"></param>
    /// <param name="radiusA"></param>
    /// <param name="radiusB"></param>
    /// <param name="radiusC"></param>
    /// <returns></returns>
    public static (double x, double y, double z) ConvertSphericalByInclinationToCartesian3(double inclination, double azimuth, double radiusA, double radiusB, double radiusC)
    {
      var (si, ci) = double.SinCos(inclination);
      var (sa, ca) = double.SinCos(azimuth);

      return (
        radiusA * si * ca,
        radiusB * si * sa,
        radiusC * ci
      );
    }

    /// <summary>
    /// <para>Creates cartesian 3D coordinates from the latitude (elevation) and longitude (azimuth) of a <see cref="SphericalCoordinate"/>, but with triaxial ellipsoid as three radii for the X (A), Y (B) and Z (C) axis.</para>
    /// <remarks>All angles in radians.</remarks>
    /// </summary>
    /// <param name="latitude"></param>
    /// <param name="longitude"></param>
    /// <param name="radiusA"></param>
    /// <param name="radiusB"></param>
    /// <param name="radiusC"></param>
    /// <returns></returns>
    public static (double x, double y, double z) ConvertSphericalByElevationToCartesian3(double latitude, double longitude, double radiusA, double radiusB, double radiusC)
    {
      var (slat, clat) = double.SinCos(latitude);
      var (slon, clon) = double.SinCos(longitude);

      return (
        radiusA * clat * clon,
        radiusB * clat * slon,
        radiusC * slat
      );
    }

    /// <summary>Converting from inclination to elevation is simply a quarter turn (PI / 2) minus the inclination.</summary>
    public static double ConvertInclinationToElevation(double inclination) => double.Pi / 2 - inclination;

    /// <summary>Converting from elevation to inclination is simply a quarter turn (PI / 2) minus the elevation.</summary>
    public static double ConvertElevationToInclination(double elevation) => double.Pi / 2 - elevation;

    #endregion // Conversion methods

    //public static SphericalCoordinate FromCartesianCoordinates(double x, double y, double z)
    //{
    //  var x2y2 = x * x + y * y;

    //  return new(
    //    double.Sqrt(x2y2 + z * z), Units.LengthUnit.Meter,
    //    double.Atan2(double.Sqrt(x2y2), z) + double.Pi, Units.AngleUnit.Radian,
    //    double.Atan2(y, x) + double.Pi, Units.AngleUnit.Radian
    //  );
    //}

    #endregion // Static methods

    #region Implemented interfaces

    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => $"<{Radius.ToString(format ?? BinaryIntegers.GetFormatStringWithCountDecimals(3), formatProvider)}, {Inclination.ToUnitString(Units.AngleUnit.Degree, format ?? BinaryIntegers.GetFormatStringWithCountDecimals(6), formatProvider)} / {Elevation.ToUnitString(Units.AngleUnit.Degree, format ?? BinaryIntegers.GetFormatStringWithCountDecimals(6), formatProvider)}, {Azimuth.ToUnitString(Units.AngleUnit.Degree, format ?? BinaryIntegers.GetFormatStringWithCountDecimals(6), formatProvider)}>";

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
