namespace Flux
{
  #region ExtensionMethods
  public static partial class Em
  {
    /// <summary>Creates a new <see cref="Geometry.SphericalCoordinate"/> from a <see cref="System.Numerics.Vector3"/>.</summary>
    public static Geometry.SphericalCoordinate ToSphericalCoordinate(this System.Numerics.Vector3 source)
    {
      var x2y2 = source.X * source.X + source.Y * source.Y;

      return new(
        System.Math.Sqrt(x2y2 + source.Z * source.Z),
        System.Math.Atan2(System.Math.Sqrt(x2y2), source.Z) + System.Math.PI,
        System.Math.Atan2(source.Y, source.X) + System.Math.PI
      );
    }
  }
  #endregion

  namespace Geometry
  {
    /// <summary>
    /// <para>Spherical coordinate.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Spherical_coordinate_system"/></para>
    /// </summary>
    /// <remarks>All angles in radians, unless noted otherwise.</remarks>
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public readonly record struct SphericalCoordinate
      : System.IFormattable
    {
      public static readonly SphericalCoordinate Zero;

      private readonly double m_radius;
      private readonly double m_inclination; // In radians.
      private readonly double m_azimuth; // In radians.

      public SphericalCoordinate(double radius, double inclination, double azimuth)
      {
        m_radius = radius;
        m_inclination = inclination;
        m_azimuth = azimuth;
      }

      public Units.Length Radius { get => new(m_radius); init => m_radius = value.Value; }
      public Units.Angle Inclination { get => new(m_inclination); init => m_inclination = value.Value; }
      public Units.Azimuth Azimuth { get => new(Units.Angle.RadianToDegree(m_azimuth)); init => m_azimuth = Units.Angle.DegreeToRadian(value.Value); }

      public Units.Angle Elevation { get => new(ConvertInclinationToElevation(m_inclination)); init => m_inclination = ConvertElevationToInclination(value.Value); }

      /// <summary>Creates cartesian 3D coordinates from the <see cref="SphericalCoordinate"/>.</summary>
      /// <remarks>All angles in radians.</remarks>
      public (double x, double y, double z) ToCartesianCoordinate3()
      {
        var (si, ci) = System.Math.SinCos(m_inclination);
        var (sa, ca) = System.Math.SinCos(m_azimuth);

        return (
          m_radius * si * ca,
          m_radius * si * sa,
          m_radius * ci
        );
      }

      /// <summary>Creates a new <see cref="CylindricalCoordinate"/> from the <see cref="SphericalCoordinate"/>.</summary>
      /// <remarks>All angles in radians.</remarks>
      public CylindricalCoordinate ToCylindricalCoordinate()
      {
        var (si, ci) = System.Math.SinCos(m_inclination);

        return new(
          m_radius * si,
          m_azimuth,
          m_radius * ci
        );
      }

      /// <summary>Creates a new <see cref="GeographicCoordinate"/> from the <see cref="SphericalCoordinate"/>.</summary>
      /// <remarks>All angles in radians.</remarks>
      public GeographicCoordinate ToGeographicCoordinate()
        => new(
          Units.Angle.RadianToDegree(System.Math.PI - m_inclination - System.Math.PI / 2),
          Units.Angle.RadianToDegree(m_azimuth - System.Math.PI),
          m_radius
        );

      /// <summary>Creates a new <see cref="System.Numerics.Vector3"/> from the <see cref="SphericalCoordinate"/>.</summary>
      /// <remarks>All angles in radians.</remarks>
      public System.Numerics.Vector3 ToVector3()
      {
        var (x, y, z) = ToCartesianCoordinate3();

        return new((float)x, (float)y, (float)z);
      }

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
        => $"{GetType().GetNameEx()} {{ Radius = {Radius.ToValueString("N1")}, Inclination = {Inclination.ToUnitValueString(Units.AngleUnit.Degree, "N3", true)} (Elevation = {Elevation.ToUnitValueString(Units.AngleUnit.Degree, "N3", true)}), Azimuth = {Azimuth.ToValueString("N3")} }}"
        + $" <{m_radius}, {m_inclination}, {m_azimuth}>";

      public override string ToString() => ToString(null, null);
    }
  }
}
