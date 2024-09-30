namespace Flux
{
  #region ExtensionMethods

  public static partial class Fx
  {
    /// <summary>Creates a new <see cref="Coordinates.CylindricalCoordinate"/> from a <see cref="System.Numerics.Vector3"/>.</summary>
    public static Coordinates.CylindricalCoordinate ToCylindricalCoordinate(this System.Numerics.Vector3 source)
    {
      var x = source.X;
      var y = source.Y;
      var z = source.Z;

      return new(
        double.Sqrt(x * x + y * y), Quantities.LengthUnit.Meter,
        (double.Atan2(y, x) + double.Tau) % double.Tau, Quantities.AngleUnit.Radian,
        z, Quantities.LengthUnit.Meter
      );
    }
  }

  #endregion

  namespace Coordinates
  {
    /// <summary>
    /// <para>Cylindrical coordinate. It is assumed that the reference plane is the Cartesian xy-plane (with equation z/height = 0), and the cylindrical axis is the Cartesian z-axis, i.e. the z-coordinate is the same in both systems, and the correspondence between cylindrical (radius, azimuth, height) and Cartesian (x, y, z) are the same as for polar coordinates.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Cylindrical_coordinate_system"/></para>
    /// </summary>
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public readonly record struct CylindricalCoordinate
      : System.IFormattable
    {
      public static readonly CylindricalCoordinate Zero;

      private readonly Quantities.Length m_radius;
      private readonly Quantities.Angle m_azimuth;
      private readonly Quantities.Length m_height;

      /// <summary>
      /// 
      /// </summary>
      /// <param name="radius"></param>
      /// <param name="azimuth"></param>
      /// <param name="height"></param>
      public CylindricalCoordinate(Quantities.Length radius, Quantities.Angle azimuth, Quantities.Length height)
      {
        m_radius = radius;
        m_azimuth = azimuth;
        m_height = height;
      }

      public CylindricalCoordinate(double radiusValue, Quantities.LengthUnit radiusUnit, double azimuthValue, Quantities.AngleUnit azimuthUnit, double heightValue, Quantities.LengthUnit heightUnit)
        : this(new Quantities.Length(radiusValue, radiusUnit), new Quantities.Angle(azimuthValue, azimuthUnit), new Quantities.Length(heightValue, heightUnit)) { }

      public CylindricalCoordinate(double radiusMeter, double azimuthRadian, double heightMeter)
        : this(radiusMeter, Quantities.LengthUnit.Meter, azimuthRadian, Quantities.AngleUnit.Radian, heightMeter, Quantities.LengthUnit.Meter) { }

      public void Deconstruct(out double radiusMeter, out double azimuthRadian, out double heightMeter)
      {
        radiusMeter = m_radius.Value;
        azimuthRadian = m_azimuth.Value;
        heightMeter = m_height.Value;
      }

      /// <summary>
      /// <para>Radius, (length) unit of meter. A.k.a. radial distance, or axial distance.</para>
      /// </summary>
      public Quantities.Length Radius { get => m_radius; init => m_radius = value; }

      /// <summary>
      /// <para>Azimuth angle, unit of radian. A.k.a. angular position.</para>
      /// </summary>
      public Quantities.Angle Azimuth { get => m_azimuth; init => m_azimuth = value; }

      /// <summary>
      /// <para>Height, (length) unit of meter. A.k.a. altitude (if the reference plane is considered horizontal), longitudinal position, axial position, or axial coordinate.</para>
      /// </summary>
      public Quantities.Length Height { get => m_height; init => m_height = value; }

      public double CylinderSurfaceArea => CylindricalCoordinate.SurfaceAreaOfCylinder(m_radius.Value, m_height.Value);

      public CartesianCoordinate ToCartesianCoordinate()
      {
        var (x, y, z) = ToCartesianCoordinate3();

        return new(x, y, z);
      }

      /// <summary>Creates cartesian 3D coordinates from the <see cref="CylindricalCoordinate"/>.</summary>
      /// <remarks>All angles in radians.</remarks>
      public (double x, double y, double z) ToCartesianCoordinate3()
      {
        var (sin, cos) = double.SinCos(m_azimuth.Value);
        var r = m_radius.Value;

        return (
          r * cos,
          r * sin,
          m_height.Value
        );
      }

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
        var r = m_radius.Value;
        var h = m_height.Value;

        return new(
          double.Sqrt(r * r + h * h), Quantities.LengthUnit.Meter,
          (double.Pi / 2) - double.Atan(h / r), Quantities.AngleUnit.Radian, // "System.Math.Atan(m_radius / m_height);", does NOT work for Takapau, New Zealand. Have to use elevation math instead of inclination, and investigate.
          m_azimuth.Value, Quantities.AngleUnit.Radian
        );
      }

      /// <summary>Creates a new <see cref="System.Numerics.Vector3"/> from the <see cref="CylindricalCoordinate"/>.</summary>
      /// <remarks>All angles in radians.</remarks>
      public System.Numerics.Vector3 ToVector3()
      {
        var (x, y, z) = ToCartesianCoordinate3();

        return new((float)x, (float)y, (float)z);
      }

      #region Static methods

      /// <summary>
      /// <para>Computes the area of a cylinder with the specified <paramref name="radius"/> and <paramref name="height"/>.</para>
      /// <para><see cref="https://en.wikipedia.org/wiki/Surface_area"/></para>
      /// </summary>
      /// <param name="radius">The radius of a cylinder.</param>
      /// <param name="height">The height of a cylinder.</param>
      public static double SurfaceAreaOfCylinder(double radius, double height) => 2 * System.Math.PI * radius * (radius + height);

      #endregion // Static methods

      #region Implemented interfaces

      public string ToString(string? format, System.IFormatProvider? provider)
        => $"<{m_radius.ToString(format ?? Format.UpTo3Decimals, provider)}, {m_azimuth.ToUnitString(Quantities.AngleUnit.Degree, format ?? Format.UpTo6Decimals, provider)}, {m_height.ToString(format ?? Format.UpTo3Decimals, provider)}>";

      #endregion // Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
