namespace Flux.Geometry
{
  /// <summary>
  /// <para>Polar coordinate. Please note that polar coordinates are two dimensional.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Polar_coordinate_system"/></para>
  /// </summary>
  /// <remarks>All angles in radians, unless noted otherwise.</remarks>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct PolarCoordinate
    : System.IFormattable, IPolarCoordinate
  {
    public static readonly PolarCoordinate Zero;

    private readonly double m_radius;
    private readonly double m_azimuth; // In radians.

    public PolarCoordinate(double radius, double azimuth)
    {
      m_radius = radius;
      m_azimuth = azimuth;
    }

    public void Deconstruct(out double radius, out double azimuth) { radius = m_radius; azimuth = m_azimuth; }

    public double Radius { get => m_radius; init => m_radius = value; }
    public double Azimuth { get => m_azimuth; init => m_azimuth = value; }

    ///// <summary>Converts the <see cref="PolarCoordinate"/> to a <see cref="Vector2"/>.</summary>
    //public CartesianCoordinate2<double> ToCartesianCoordinate2()
    // => new(
    //   m_radius * System.Math.Cos(m_azimuth),
    //   m_radius * System.Math.Sin(m_azimuth)
    // );

    ///// <summary>Converts the <see cref="PolarCoordinate"/> to a <see cref="System.Numerics.Complex"/>.</summary>
    //public System.Numerics.Complex ToComplex()
    // => System.Numerics.Complex.FromPolarCoordinates(
    //   m_radius,
    //   m_azimuth
    // );

    //#region Static methods
    ///// <summary>Return the <see cref="IPolarCoordinate"/> from the specified components.</summary>
    //public static PolarCoordinate<TSelf> From(Quantities.Length radius, Azimuth azimuth)
    //  => new(
    //    TSelf.CreateChecked(radius.Value),
    //    TSelf.CreateChecked(azimuth.ToRadians())
    //  );
    //#endregion // Static methods

    public string ToString(string? format, System.IFormatProvider? provider)
      => $"{GetType().GetNameEx()} {{ Radius = {string.Format($"{{0:{format ?? "N1"}}}", Radius)}, Azimuth = {new Units.Angle(Azimuth).ToUnitString(Units.AngleUnit.Degree, format ?? "N3", true)} }}";

    public override string ToString() => ToString(null, null);
  }
}
