namespace Flux
{
  #region ExtensionMethods
  public static partial class Em
  {
    /// <summary>Creates a new <see cref="Geometry.PolarCoordinate"/> from a <see cref="System.Numerics.Vector2"/>.</summary>
    public static Geometry.Coordinates.PolarCoordinate ToPolarCoordinate(this System.Numerics.Vector2 source)
      => new(
        System.Math.Sqrt(source.X * source.X + source.Y * source.Y), Quantities.LengthUnit.Metre,
        System.Math.Atan2(source.Y, source.X), Quantities.AngleUnit.Radian
      );
  }
  #endregion

  namespace Geometry.Coordinates
  {
    /// <summary>
    /// <para>Polar coordinate. Please note that polar coordinates are two dimensional.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Polar_coordinate_system"/></para>
    /// </summary>
    /// <remarks>All angles in radians, unless noted otherwise.</remarks>
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public readonly record struct PolarCoordinate
      : System.IFormattable
    {
      public static readonly PolarCoordinate Zero;

      private readonly Quantities.Length m_radius;
      private readonly Quantities.Angle m_azimuth;

      /// <summary>
      /// <para>Polar coordinates in meters and radians.</para>
      /// </summary>
      /// <param name="radius"></param>
      /// <param name="azimuth"></param>
      //public PolarCoordinate(double radius, double azimuth)
      //{
      //  m_radius = radius;
      //  m_azimuth = azimuth;
      //}

      /// <summary>Return the <see cref="PolarCoordinate"/> from the specified components.</summary>
      public PolarCoordinate(Quantities.Length radius, Quantities.Angle azimuth)
      {
        m_radius = radius;
        m_azimuth = azimuth;
      }

      /// <summary>Return the <see cref="PolarCoordinate"/> from the specified components.</summary>
      public PolarCoordinate(double radiusValue, Quantities.LengthUnit radiusUnit, double azimuthValue, Quantities.AngleUnit azimuthUnit)
        : this(new Quantities.Length(radiusValue, radiusUnit), new Quantities.Angle(azimuthValue, azimuthUnit))
      { }

      /// <summary>
      /// <para>Radius, (length) unit of meter. A.k.a. radial coordinate, or radial distance.</para>
      /// </summary>
      public Quantities.Length Radius { get => m_radius; init => m_radius = value; }
      /// <summary>
      /// <para>Azimuth angle, unit of radian. A.k.a. angular coordinate, or polar angle.</para>
      /// </summary>
      /// <remarks>The angle is defined to start at 0° from a reference direction, and to increase for rotations in either clockwise (cw) or counterclockwise (ccw) orientation.</remarks>
      public Quantities.Angle Azimuth { get => m_azimuth; init => m_azimuth = value; }

      public void Deconstruct(out Quantities.Length radius, out Quantities.Angle azimuth)
      {
        radius = m_radius;
        azimuth = m_azimuth;
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

      /// <summary>Creates a <see cref="PolarCoordinate"/> from the cartesian 2D coordinates (x, y) where 'right-center' is 'zero' (i.e. positive-x and neutral-y) to a counter-clockwise rotation angle [0, PI*2] (i.e. radians). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
      public static (double radius, double azimuth) ConvertCartesian2ToPolar(double x, double y)
        => new(
          System.Math.Sqrt(x * x + y * y),
          System.Math.Atan2(y, x)
        );

      /// <summary>Creates a <see cref="PolarCoordinate"/> from the cartesian 2D coordinates (x, y) where 'center-up' is 'zero' (i.e. neutral-x and positive-y) to a clockwise rotation angle [0, PI*2] (i.e. radians). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
      public static (double radius, double azimuth) ConvertCartesian2ToPolarEx(double x, double y)
        => (
          System.Math.Sqrt(x * x + y * y),
          //System.Math.Tau - System.Math.Atan2(-x, y) is var atan2 && atan2 < 0 ? System.Math.Tau + atan2 : atan2
          System.Math.Atan2(x, y) is var atan2 && atan2 < 0 ? System.Math.Tau + atan2 : atan2
        );

      /// <summary>Convert the polar coordinate [0, PI*2] (i.e. radians) where 'zero' azimuth is 'right-center' (i.e. positive-x and neutral-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
      public static (double x, double y) ConvertPolarToCartesian2(double radius, double azimuth)
      {
        var (sin, cos) = System.Math.SinCos(azimuth);

        return (radius * cos, radius * sin);
      }

      /// <summary>Convert the polar coordinate [0, PI*2] (i.e. radians) where 'zero' azimuth is 'center-up' (i.e. neutral-x and positive-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
      public static (double x, double y) ConvertPolarToCartesian2Ex(double radius, double azimuth)
        => ConvertPolarToCartesian2(radius, (System.Math.Tau * 1.25) - (azimuth % System.Math.Tau) is var h && h > System.Math.Tau ? h - System.Math.Tau : h); // Adjust azimuth.
      //{
      //  var adjustedAzimuth = (System.Math.Tau * 1.25) - (azimuth % System.Math.Tau) is var h && h > System.Math.Tau ? h - System.Math.Tau : h;
      //  //var adjustedAzimuth = System.Math.Tau - (azimuth % System.Math.Tau is var rad && rad < 0 ? rad + System.Math.Tau : rad) + System.Math.PI / 2;

      //  var (sin, cos) = System.Math.SinCos(adjustedAzimuth);

      //  return (radius * cos, radius * sin);
      //}

      #endregion // Static methods

      public string ToString(string? format, System.IFormatProvider? provider)
      {
        if (string.IsNullOrWhiteSpace(format)) format = "N3";

        return $"<{m_radius.Value.ToString(format)}, {new Quantities.Azimuth(m_azimuth.Value, Quantities.AngleUnit.Radian).ToString(format, null)} ({m_azimuth.Value.ToString(format)})>";
      }
    }
  }
}
