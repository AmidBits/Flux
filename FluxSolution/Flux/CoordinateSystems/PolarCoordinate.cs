namespace Flux.CoordinateSystems
{
  /// <summary>
  /// <para>Polar coordinate. Please note that polar coordinates are two dimensional.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Polar_coordinate_system"/></para>
  /// </summary>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct PolarCoordinate
    : System.IFormattable
  {
    public static PolarCoordinate Empty { get; }

    private readonly double m_radius;
    private readonly double m_azimuth;

    /// <summary>Return the <see cref="PolarCoordinate"/> from the specified components.</summary>
    public PolarCoordinate(Units.Length radius, Units.Angle azimuth)
    {
      m_radius = radius.Value;
      m_azimuth = azimuth.Value;
    }

    /// <summary>Return the <see cref="PolarCoordinate"/> from the specified components.</summary>
    public PolarCoordinate(double radiusValue, Units.LengthUnit radiusUnit, double azimuthValue, Units.AngleUnit azimuthUnit)
    {
      m_radius = Units.Length.ConvertFromUnit(radiusUnit, radiusValue);
      m_azimuth = Units.Angle.ConvertFromUnit(azimuthUnit, azimuthValue);
    }

    public PolarCoordinate(double radiusMeter, double azimuthRadian)
    {
      m_radius = radiusMeter;
      m_azimuth = azimuthRadian;
    }

    public void Deconstruct(out double radiusMeter, out double azimuthRadian)
    {
      radiusMeter = m_radius;
      azimuthRadian = m_azimuth;
    }

    /// <summary>
    /// <para>Radius, (length) unit of meter. A.k.a. radial coordinate, or radial distance.</para>
    /// </summary>
    /// <remarks>If the radius is zero, azimuth is arbitrary.</remarks>
    public Units.Length Radius { get => new(m_radius); init => m_radius = value.Value; }

    /// <summary>
    /// <para>Azimuth angle, unit of radian. A.k.a. angular coordinate, or polar angle.</para>
    /// </summary>
    /// <remarks>The angle is defined to start at 0° from a reference direction, and to increase for rotations in either clockwise (cw) or counterclockwise (ccw) orientation.</remarks>
    public Units.Angle Azimuth { get => new(m_azimuth); init => m_azimuth = value.Value; }

    public CartesianCoordinate ToCartesianCoordinate()
    {
      var (x, y) = ConvertPolarToCartesian2(m_radius, m_azimuth);

      return new(x, y, 0, 0);
    }

    public CartesianCoordinate ToCartesianCoordinateEx()
    {
      var (x, y) = ConvertPolarToCartesian2Ex(m_radius, m_azimuth);

      return new(x, y, 0, 0);
    }

    public Numerics.Geometry.Circles.CircleGeometry ToCircleFigure() => new(m_radius);

    /// <summary>Creates a new <see cref="CylindricalCoordinate"/> from the <see cref="PolarCoordinate"/> by adding the third component <paramref name="height"/>.</summary>
    /// <remarks>All angles in radians.</remarks>
    public CylindricalCoordinate ToCylindricalCoordinate(double heightMeter)
      => new(
        m_radius,
        m_azimuth,
        heightMeter
      );

    /// <summary>Creates a new <see cref="CylindricalCoordinate"/> from the <see cref="PolarCoordinate"/> by adding the third component <paramref name="heightValue"/>.</summary>
    /// <remarks>All angles in radians.</remarks>
    public CylindricalCoordinate ToCylindricalCoordinate(double heightValue, Units.LengthUnit heightUnit)
      => ToCylindricalCoordinate(Units.Length.ConvertFromUnit(heightUnit, heightValue));

    /// <summary>Creates a new <see cref="CylindricalCoordinate"/> from the <see cref="PolarCoordinate"/> by adding the third component <paramref name="heightValue"/>.</summary>
    /// <remarks>All angles in radians.</remarks>
    public CylindricalCoordinate ToCylindricalCoordinate(Units.Length height)
      => ToCylindricalCoordinate(height.Value);

    /// <summary>Creates a <see cref="System.Numerics.Complex"/> from the <see cref="PolarCoordinate"/>.</summary>
    /// <remarks>All angles in radians.</remarks>
    public System.Numerics.Complex ToComplex()
      => System.Numerics.Complex.FromPolarCoordinates(
        m_radius,
        m_azimuth
      );

    /// <summary>Creates a <see cref="System.Numerics.Vector2"/> from the <see cref="PolarCoordinate"/>.</summary>
    /// <remarks>All angles in radians.</remarks>
    public System.Numerics.Vector2 ToVector2()
    {
      var (x, y) = ConvertPolarToCartesian2(m_radius, m_azimuth);

      return new((float)x, (float)y);
    }

    #region Static methods

    public static PolarCoordinate CreateRandom(double radius, System.Random? rng = null)
    {
      rng ??= System.Random.Shared;

      return new(
        rng.NextNumber(radius),
        rng.NextNumber(double.Tau)
      );
    }

    public static PolarCoordinate CreateRandomOnEdge(double radius, System.Random? rng = null)
    {
      rng ??= System.Random.Shared;

      return new(
        radius,
        rng.NextNumber(double.Tau)
      );
    }

    #region Conversion methods

    /// <summary>
    /// <para>Creates a <see cref="PolarCoordinate"/> from the cartesian 2D coordinates (x, y) where 'right-center' is 'zero' (i.e. positive-x and neutral-y) to a counter-clockwise rotation angle [0, PI*2] (i.e. radians). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/></para>
    /// </summary>
    public static (double radius, double azimuth) ConvertCartesian2ToPolar(double x, double y)
    {
      var azimuth = double.Atan2(y, x);

      if (azimuth < 0)
        azimuth += double.Tau;

      return (
        double.Sqrt(x * x + y * y),
        azimuth
      );
    }

    /// <summary>
    /// <para>Creates a <see cref="PolarCoordinate"/> from the cartesian 2D coordinates (x, y) where 'center-up' is 'zero' (i.e. neutral-x and positive-y) to a clockwise rotation angle [0, PI*2] (i.e. radians). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/></para>
    /// </summary>
    public static (double radius, double azimuth) ConvertCartesian2ToPolarEx(double x, double y)
    {
      var azimuth = double.Atan2(x, y); // Ex version is atan2(x,y) instead of atan2(y,x).

      if (azimuth < 0)
        azimuth += double.Tau;

      return (
        double.Sqrt(x * x + y * y),
        azimuth
      );
    }

    /// <summary>
    /// <para>Convert the polar coordinate [0, Tau(2*Pi)] (i.e. radians) where 'zero' azimuth is 'right-center' (i.e. positive-x and neutral-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/></para>
    /// </summary>
    public static (double x, double y) ConvertPolarToCartesian2(double radius, double azimuth)
    {
      var (sin, cos) = double.SinCos(azimuth);

      return (radius * cos, radius * sin);
    }

    /// <summary>
    /// <para>Convert the polar coordinate [0, Tau(2*Pi)] (i.e. radians) where 'zero' azimuth is 'center-up' (i.e. neutral-x and positive-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/></para>
    /// </summary>
    public static (double x, double y) ConvertPolarToCartesian2Ex(double radius, double azimuth)
    {
      var (sin, cos) = double.SinCos(azimuth);

      return (radius * sin, radius * cos); // Ex version is (r*sin,r*cos) instead of (r*cos,r*sin).
    }

    #endregion // Conversion methods

    //public static PolarCoordinate FromCartesianCoordinates(double x, double y, bool likeCompass = false)
    //{
    //  var (radius, angle) = likeCompass ? ConvertCartesian2ToPolarEx(x, y) : ConvertCartesian2ToPolar(x, y);

    //  return new(radius, angle);
    //}

    #endregion // Static methods

    #region Implemented interfaces

    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => $"<{Radius.ToUnitString(format: format ?? BinaryInteger.GetFormatStringWithCountDecimals(3), provider: formatProvider)}, {Azimuth.ToUnitString(Units.AngleUnit.Degree, format ?? BinaryInteger.GetFormatStringWithCountDecimals(6), formatProvider)}>";

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
