namespace Flux
{
  #region ExtensionMethods
  public static partial class Em
  {
    /// <summary>Creates a new <see cref="Geometry.PolarCoordinate"/> from a <see cref="System.Numerics.Vector2"/>.</summary>
    public static Geometry.PolarCoordinate ToPolarCoordinate(this System.Numerics.Vector2 source)
      => new(
        System.Math.Sqrt(source.X * source.X + source.Y * source.Y),
        System.Math.Atan2(source.Y, source.X)
      );
  }
  #endregion

  namespace Geometry
  {
    /// <summary>
    /// <para>Polar coordinate. Please note that polar coordinates are two dimensional.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Polar_coordinate_system"/></para>
    /// </summary>
    /// <remarks>All angles in radians, unless noted otherwise.</remarks>
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public readonly record struct PolarCoordinate
      : System.IFormattable, IPolarCoordinate<double>
    {
      public static readonly PolarCoordinate Zero;

      private readonly double m_radius;
      private readonly double m_azimuth; // In radians.

      public PolarCoordinate(double radius, double azimuth, bool constrain = true)
      {
        m_radius = radius;
        m_azimuth = constrain ? azimuth % System.Math.Tau : azimuth;
      }

      /// <summary>Return the <see cref="PolarCoordinate"/> from the specified components.</summary>
      public PolarCoordinate(Units.Length radius, Units.Azimuth azimuth, bool constrain = true)
        : this(radius.Value, azimuth.Angle.Value, constrain)
      { }

      /// <summary>Return the <see cref="PolarCoordinate"/> from the specified components.</summary>
      public PolarCoordinate(double radiusValue, Units.LengthUnit radiusUnit, double azimuthValue, Units.AngleUnit azimuthUnit, bool constrain = true)
        : this(new Units.Length(radiusValue, radiusUnit), new Units.Azimuth(azimuthValue, azimuthUnit), constrain)
      { }

      public double Radius { get => m_radius; init => m_radius = value; }
      public double Azimuth { get => m_azimuth; init => m_azimuth = value; }

      /// <summary>Convert the polar coordinate [0, PI*2] (i.e. radians) where 'zero' azimuth is 'right-center' (i.e. positive-x and neutral-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
      public (double x, double y) ToCartesianCoordinate2()
      {
        var (sin, cos) = System.Math.SinCos(m_azimuth);

        return (m_radius * cos, m_radius * sin);
      }

      /// <summary>Convert the polar coordinate [0, PI*2] (i.e. radians) where 'zero' azimuth is 'center-up' (i.e. neutral-x and positive-y) to a cartesian 2D coordinate (x, y). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
      public (double x, double y) ToCartesianCoordinate2Ex()
      {
        var adjustedAzimuth = System.Math.Tau - (m_azimuth % System.Math.Tau is var rad && rad < 0 ? rad + System.Math.Tau : rad) + System.Math.PI / 2;

        var (sin, cos) = System.Math.SinCos(adjustedAzimuth);

        return (m_radius * cos, m_radius * sin);
      }

      /// <summary>Creates a new <see cref="CylindricalCoordinate"/> from the <see cref="PolarCoordinate"/> by adding the third component <paramref name="height"/>.</summary>
      /// <remarks>All angles in radians.</remarks>
      public CylindricalCoordinate ToCylindricalCoordinate(double height)
        => new(
          m_radius,
          m_azimuth,
          height
        );

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
        var (x, y) = ToCartesianCoordinate2();

        return new((float)x, (float)y);
      }

      #region Static methods

      /// <summary>Creates a <see cref="PolarCoordinate"/> from the cartesian 2D coordinates (x, y) where 'right-center' is 'zero' (i.e. positive-x and neutral-y) to a counter-clockwise rotation angle [0, PI*2] (i.e. radians). Looking at the face of a clock, this goes counter-clockwise from and to 3 o'clock.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
      public static PolarCoordinate FromCartesian2(double x, double y)
        => new(
          System.Math.Sqrt(x * x + y * y),
          System.Math.Atan2(y, x)
        );

      /// <summary>Creates a <see cref="PolarCoordinate"/> from the cartesian 2D coordinates (x, y) where 'center-up' is 'zero' (i.e. neutral-x and positive-y) to a clockwise rotation angle [0, PI*2] (i.e. radians). Looking at the face of a clock, this goes clockwise from and to 12 o'clock.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Rotation_matrix#In_two_dimensions"/>
      public static PolarCoordinate FromCartesian2Ex(double x, double y)
        => new(
          System.Math.Sqrt(x * x + y * y),
          System.Math.Tau - System.Math.Atan2(-x, y) is var atan2 && atan2 < 0 ? System.Math.Tau + atan2 : atan2
        );

      #endregion // Static methods

      public string ToString(string? format, System.IFormatProvider? provider)
      {
        if (string.IsNullOrWhiteSpace(format)) format = "N3";

        return $"<{m_radius.ToString(format)}, {new Units.Azimuth(m_azimuth, Units.AngleUnit.Radian).ToString(format, null)} ({m_azimuth.ToString(format)})>";
      }
    }
  }
}
