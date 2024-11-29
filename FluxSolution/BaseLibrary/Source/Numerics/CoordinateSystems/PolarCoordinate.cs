namespace Flux
{
  #region ExtensionMethods
  public static partial class Fx
  {
    /// <summary>Creates a new <see cref="Coordinates.PolarCoordinate"/> from a <see cref="System.Numerics.Vector2"/>.</summary>
    public static Coordinates.PolarCoordinate ToPolarCoordinate(this System.Numerics.Vector2 source)
    {
      var x = source.X;
      var y = source.Y;

      return new(
        System.Math.Sqrt(x * x + y * y), Quantities.LengthUnit.Meter,
        System.Math.Atan2(y, x), Quantities.AngleUnit.Radian
      );
    }
  }
  #endregion

  namespace Coordinates
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

      private readonly Quantities.Length m_radius;
      private readonly Quantities.Angle m_azimuth;

      /// <summary>Return the <see cref="PolarCoordinate"/> from the specified components.</summary>
      public PolarCoordinate(Quantities.Length radius, Quantities.Angle azimuth)
      {
        m_radius = radius;
        m_azimuth = azimuth;
      }

      /// <summary>Return the <see cref="PolarCoordinate"/> from the specified components.</summary>
      public PolarCoordinate(double radiusValue, Quantities.LengthUnit radiusUnit, double azimuthValue, Quantities.AngleUnit azimuthUnit)
        : this(new Quantities.Length(radiusValue, radiusUnit), new Quantities.Angle(azimuthValue, azimuthUnit)) { }

      public PolarCoordinate(double radiusMeter, double azimuthRadian)
        : this(radiusMeter, Quantities.LengthUnit.Meter, azimuthRadian, Quantities.AngleUnit.Radian) { }

      public void Deconstruct(out double radiusMeter, out double azimuthRadian)
      {
        radiusMeter = m_radius.Value;
        azimuthRadian = m_azimuth.Value;
      }

      /// <summary>
      /// <para>Radius, (length) unit of meter. A.k.a. radial coordinate, or radial distance.</para>
      /// </summary>
      /// <remarks>If the radius is zero, azimuth is arbitrary.</remarks>
      public Quantities.Length Radius { get => m_radius; init => m_radius = value; }

      /// <summary>
      /// <para>Azimuth angle, unit of radian. A.k.a. angular coordinate, or polar angle.</para>
      /// </summary>
      /// <remarks>The angle is defined to start at 0° from a reference direction, and to increase for rotations in either clockwise (cw) or counterclockwise (ccw) orientation.</remarks>
      public Quantities.Angle Azimuth { get => m_azimuth; init => m_azimuth = value; }

      public CartesianCoordinate ToCartesianCoordinate()
      {
        var (x, y) = ConvertPolarToCartesian2(m_radius.Value, m_azimuth.Value);

        return new(x, y, 0, 0);
      }

      /// <summary>Creates a new <see cref="CylindricalCoordinate"/> from the <see cref="PolarCoordinate"/> by adding the third component <paramref name="height"/>.</summary>
      /// <remarks>All angles in radians.</remarks>
      public CylindricalCoordinate ToCylindricalCoordinate(Quantities.Length height)
        => new(
          m_radius,
          m_azimuth,
          height
        );

      /// <summary>Creates a new <see cref="CylindricalCoordinate"/> from the <see cref="PolarCoordinate"/> by adding the third component <paramref name="height"/>.</summary>
      /// <remarks>All angles in radians.</remarks>
      public CylindricalCoordinate ToCylindricalCoordinate(double height, Quantities.LengthUnit heightUnit)
        => new(
          m_radius,
          m_azimuth,
          new Quantities.Length(height, heightUnit)
        );

      /// <summary>Creates a <see cref="System.Numerics.Complex"/> from the <see cref="PolarCoordinate"/>.</summary>
      /// <remarks>All angles in radians.</remarks>
      public System.Numerics.Complex ToComplex()
        => System.Numerics.Complex.FromPolarCoordinates(
          m_radius.Value,
          m_azimuth.Value
        );

      /// <summary>Creates a <see cref="System.Numerics.Vector2"/> from the <see cref="PolarCoordinate"/>.</summary>
      /// <remarks>All angles in radians.</remarks>
      public System.Numerics.Vector2 ToVector2()
      {
        var (x, y) = ConvertPolarToCartesian2(m_radius.Value, m_azimuth.Value);

        return new((float)x, (float)y);
      }

      #region Static methods

      #region Conversion methods

      /// <summary>
      /// <para>Creates a <see cref="PolarCoordinate"/> from the cartesian 2D coordinates (x, y) where 'right-center' is 'zero' (i.e. positive-x and neutral-y) to a counter-clockwise rotation angle [0, PI*2] (i.e. radians). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/></para>
      /// </summary>
      public static (double radius, double azimuth) ConvertCartesian2ToPolar(double x, double y)
        => new(
          double.Sqrt(x * x + y * y),
          double.Atan2(y, x)
        );

      /// <summary>
      /// <para>Creates a <see cref="PolarCoordinate"/> from the cartesian 2D coordinates (x, y) where 'center-up' is 'zero' (i.e. neutral-x and positive-y) to a clockwise rotation angle [0, PI*2] (i.e. radians). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/></para>
      /// </summary>
      public static (double radius, double azimuth) ConvertCartesian2ToPolarEx(double x, double y)
        => (
          double.Sqrt(x * x + y * y),
          double.Atan2(x, y) is var atan2 && atan2 < 0 ? double.Tau + atan2 : atan2
        );

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
        => ConvertPolarToCartesian2(radius, (double.Tau * 1.25) - (azimuth % double.Tau) is var h && h > double.Tau ? h - double.Tau : h);

      #endregion // Conversion methods

      #endregion // Static methods

      #region Implemented interfaces

      public string ToString(string? format, System.IFormatProvider? provider)
        => $"<{m_radius.ToString(format ?? Format.UpTo3Decimals, provider)}, {m_azimuth.ToUnitString(Quantities.AngleUnit.Degree, format ?? Format.UpTo6Decimals, provider)}>";

      #endregion // Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
