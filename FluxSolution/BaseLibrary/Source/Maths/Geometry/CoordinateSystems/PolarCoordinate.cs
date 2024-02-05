using Flux.Units;

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

      public PolarCoordinate(double radius, double azimuth)
      {
        m_radius = radius;
        m_azimuth = azimuth;
      }

      /// <summary>Return the <see cref="PolarCoordinate"/> from the specified components.</summary>
      public PolarCoordinate(Units.Length radius, Units.Azimuth azimuth)
        : this(radius.Value, azimuth.Angle.Value)
      { }

      /// <summary>Return the <see cref="PolarCoordinate"/> from the specified components.</summary>
      public PolarCoordinate(double radiusValue, Units.LengthUnit radiusUnit, double azimuthValue, Units.AngleUnit azimuthUnit)
        : this(new Units.Length(radiusValue, radiusUnit), new Units.Azimuth(azimuthValue, azimuthUnit))
      { }

      public double Radius { get => m_radius; init => m_radius = value; }
      public double Azimuth { get => m_azimuth; init => m_azimuth = value; }

      /// <summary>Creates a cartesian 2D coordinate from the <see cref="PolarCoordinate"/>.</summary>
      /// <remarks>All angles in radians.</remarks>
      public (double x, double y) ToCartesianCoordinate2()
      {
        var (sa, ca) = System.Math.SinCos(m_azimuth);

        return (m_radius * ca, m_radius * sa);
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

      #endregion // Static methods

      public string ToString(string? format, System.IFormatProvider? provider)
        => $"{GetType().GetNameEx()} {{ Radius = {m_radius.ToString("N1")}, Azimuth = {new Units.Azimuth(m_azimuth, Units.AngleUnit.Radian).ToValueString(new() { Format = "N3" })} }}"
        + $" <{m_radius}, {m_azimuth}>";

      public override string ToString() => ToString(null, null);
    }
  }
}
