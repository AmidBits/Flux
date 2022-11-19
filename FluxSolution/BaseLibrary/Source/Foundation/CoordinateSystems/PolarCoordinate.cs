namespace Flux
{
  /// <summary>Polar coordinate. Please note that polar coordinates are two dimensional.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Polar_coordinate_system"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct PolarCoordinate
    : IPolarCoordinate
  {
    private readonly double m_radius;
    private readonly double m_azimuth;

    public PolarCoordinate(double radius, double azimuth)
    {
      m_radius = radius;
      m_azimuth = azimuth;
    }

    [System.Diagnostics.Contracts.Pure] public Length Radius { get => new(m_radius); init => m_radius = value.Value; }
    [System.Diagnostics.Contracts.Pure] public Azimuth Azimuth { get => Azimuth.FromRadians(m_azimuth); init => m_azimuth = value.ToRadians(); }

    /// <summary>Converts the <see cref="PolarCoordinate"/> to a <see cref="Vector2"/>.</summary>
    [System.Diagnostics.Contracts.Pure]
    public Vector2 ToCartesianCoordinate2()
     => new Vector2(
       m_radius * System.Math.Cos(m_azimuth),
       m_radius * System.Math.Sin(m_azimuth)
     );

    /// <summary>Converts the <see cref="PolarCoordinate"/> to a <see cref="System.Numerics.Complex"/>.</summary>
    [System.Diagnostics.Contracts.Pure]
    public System.Numerics.Complex ToComplex()
     => System.Numerics.Complex.FromPolarCoordinates(
       m_radius,
       m_azimuth
     );

    #region Static methods
    /// <summary>Return the <see cref="IPolarCoordinate"/> from the specified components.</summary>
    public static PolarCoordinate From(Length radius, Azimuth azimuth)
       => new PolarCoordinate(radius.Value, azimuth.ToRadians());
    #endregion Static methods

    public override string ToString()
      => $"{GetType().Name} {{ Radius = {m_radius}, Azimuth = {new Angle(m_azimuth).ToUnitString(AngleUnit.Degree, "N3", true)} }}";
  }
}
