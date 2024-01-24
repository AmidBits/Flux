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
      : System.IFormattable
    {
      public static readonly PolarCoordinate Zero;

      private readonly double m_radius;
      private readonly double m_azimuth; // In radians.

      public PolarCoordinate(double radius, double azimuth)
      {
        m_radius = radius;
        m_azimuth = azimuth;
      }

      public Units.Length Radius { get => new(m_radius); init => m_radius = value.Value; }
      public Units.Azimuth Azimuth { get => new(Units.Angle.RadianToDegree(m_azimuth)); init => m_azimuth = Units.Angle.DegreeToRadian(value.Value); }

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

      //#region Static methods
      ///// <summary>Return the <see cref="IPolarCoordinate"/> from the specified components.</summary>
      //public static PolarCoordinate<TSelf> From(Quantities.Length radius, Azimuth azimuth)
      //  => new(
      //    TSelf.CreateChecked(radius.Value),
      //    TSelf.CreateChecked(azimuth.ToRadians())
      //  );
      //#endregion // Static methods

      public string ToString(string? format, System.IFormatProvider? provider)
        => $"{GetType().GetNameEx()} {{ Radius = {Radius.ToValueString("N1")}, Azimuth = {Azimuth.ToValueString("N3")} }}"
        + $" <{m_radius}, {m_azimuth}>";

      public override string ToString() => ToString(null, null);
    }
  }
}
