namespace Flux.CoordinateSystems
{
  /// <summary>
  /// <para>Cylindrical coordinate. It is assumed that the reference plane is the Cartesian xy-plane (with equation z/height = 0), and the cylindrical axis is the Cartesian z-axis, i.e. the z-coordinate is the same in both systems, and the correspondence between cylindrical (radius, azimuth, height) and Cartesian (x, y, z) are the same as for polar coordinates.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Cylindrical_coordinate_system"/></para>
  /// </summary>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct CylindricalCoordinate
    : System.IFormattable
  {
    public static CylindricalCoordinate Empty { get; }

    private readonly double m_radius;
    private readonly double m_azimuth;
    private readonly double m_height;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="radius"></param>
    /// <param name="azimuth"></param>
    /// <param name="height"></param>
    public CylindricalCoordinate(Units.Length radius, Units.Angle azimuth, Units.Length height)
    {
      m_radius = radius.Value;
      m_azimuth = azimuth.Value;
      m_height = height.Value;
    }

    public CylindricalCoordinate(double radiusValue, Units.LengthUnit radiusUnit, double azimuthValue, Units.AngleUnit azimuthUnit, double heightValue, Units.LengthUnit heightUnit)
    {
      m_radius = Units.Length.ConvertFromUnit(radiusUnit, radiusValue);
      m_azimuth = Units.Angle.ConvertFromUnit(azimuthUnit, azimuthValue);
      m_height = Units.Length.ConvertFromUnit(heightUnit, heightValue);
    }

    public CylindricalCoordinate(double radiusMeter, double azimuthRadian, double heightMeter)
    {
      m_radius = radiusMeter;
      m_azimuth = azimuthRadian;
      m_height = heightMeter;
    }

    public void Deconstruct(out double radiusMeter, out double azimuthRadian, out double heightMeter)
    {
      radiusMeter = m_radius;
      azimuthRadian = m_azimuth;
      heightMeter = m_height;
    }

    /// <summary>
    /// <para>Radius, (length) unit of meter. A.k.a. radial distance, or axial distance.</para>
    /// </summary>
    public Units.Length Radius { get => new(m_radius); init => m_radius = value.Value; }

    /// <summary>
    /// <para>Azimuth angle, unit of radian. A.k.a. angular position.</para>
    /// </summary>
    public Units.Angle Azimuth { get => new(m_azimuth); init => m_azimuth = value.Value; }

    /// <summary>
    /// <para>Height, (length) unit of meter. A.k.a. altitude (if the reference plane is considered horizontal), longitudinal position, axial position, or axial coordinate.</para>
    /// </summary>
    public Units.Length Height { get => new(m_height); init => m_height = value.Value; }

    public CartesianCoordinate ToCartesianCoordinate()
    {
      var (x, y, z) = ConvertCylindricalToCartesian3(m_radius, m_azimuth, m_height);

      return new(x, y, z);
    }

    ///// <summary>Creates cartesian 3D coordinates from the <see cref="CylindricalCoordinate"/>.</summary>
    ///// <remarks>All angles in radians.</remarks>
    //public (double x, double y, double z) ToCartesianCoordinate3()
    //{
    //  var (sin, cos) = double.SinCos(m_azimuth);
    //  var r = m_radius;

    //  return (
    //    r * cos,
    //    r * sin,
    //    m_height
    //  );
    //}

    /// <summary>Creates a new <see cref="PolarCoordinate"/> from the <see cref="CylindricalCoordinate"/> by removing the third component "height".</summary>
    /// <remarks>All angles in radians.</remarks>
    public PolarCoordinate ToPolarCoordinate()
      => new(
        m_radius,
        m_azimuth
      );

    /// <summary>
    /// <para>Creates a new <see cref="SphericalCoordinate"/> from the <see cref="CylindricalCoordinate"/>.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Cylindrical_coordinate_system#Spherical_coordinates"/></para>
    /// </summary>
    /// <remarks>All angles in radians.</remarks>
    public SphericalCoordinate ToSphericalCoordinate()
    {
      var r = m_radius;
      var h = m_height;

      return new(
        double.Sqrt(r * r + h * h), Units.LengthUnit.Meter,
        (double.Pi / 2) - double.Atan(h / r), Units.AngleUnit.Radian, // "double.Atan(m_radius / m_height);", does NOT work for Takapau, New Zealand. Have to use elevation math instead of inclination, and investigate.
        m_azimuth, Units.AngleUnit.Radian
      );
    }

    /// <summary>Creates a new <see cref="System.Numerics.Vector3"/> from the <see cref="CylindricalCoordinate"/>.</summary>
    /// <remarks>All angles in radians.</remarks>
    public System.Numerics.Vector3 ToVector3()
    {
      var (x, y, z) = ConvertCylindricalToCartesian3(m_radius, m_azimuth, m_height);

      return new((float)x, (float)y, (float)z);
    }

    #region Static methods

    public CylindricalCoordinate CreateRandom(double radius, double height, System.Random? rng = null)
    {
      rng ??= System.Random.Shared;

      return new(
        rng.NextDouble(radius),
        rng.NextDouble(double.Tau),
        rng.NextDouble(height)
      );
    }

    public CylindricalCoordinate CreateRandomOnSurface(double radius, double height, System.Random? rng = null)
    {
      rng ??= System.Random.Shared;

      var rORh = rng.NextBoolean(); // Determines whether radius or height is fixed.

      return new(
        rORh ? radius : rng.NextDouble(radius), // Either fixed (on curved surface), or random.
        rng.NextDouble(double.Tau),
        rORh ? rng.NextDouble(height) : (rng.NextBoolean() ? height : 0) // Either random, or fixed (at one of the poles).
      );
    }

    #region Conversion methods

    /// <summary>Creates cartesian 3D coordinates from the <see cref="CylindricalCoordinate"/>.</summary>
    /// <remarks>All angles in radians.</remarks>
    public static (double x, double y, double z) ConvertCylindricalToCartesian3(double radius, double azimuth, double height)
    {
      var (sin, cos) = double.SinCos(azimuth);
      var r = radius;

      return (
        r * cos,
        r * sin,
        height
      );
    }

    #endregion // Conversion methods

    //public static CylindricalCoordinate FromCartesianCoordinates(double x, double y, double z)
    //{
    //  return new(
    //    double.Sqrt(x * x + y * y), Units.LengthUnit.Meter,
    //    (double.Atan2(y, x) + double.Tau) % double.Tau, Units.AngleUnit.Radian,
    //    z, Units.LengthUnit.Meter
    //  );
    //}

    #endregion // Static methods

    #region Implemented interfaces

    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => $"<{Radius.ToString(format ?? 3.GetFormatWithCountDecimals(), formatProvider)}, {Azimuth.ToUnitString(Units.AngleUnit.Degree, format ?? 6.GetFormatWithCountDecimals(), formatProvider)}, {Height.ToString(format ?? 3.GetFormatWithCountDecimals(), formatProvider)}>";

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
