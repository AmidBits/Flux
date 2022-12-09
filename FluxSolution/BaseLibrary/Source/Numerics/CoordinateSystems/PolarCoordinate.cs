namespace Flux.CoordinateSystems
{
  /// <summary>Polar coordinate. Please note that polar coordinates are two dimensional.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Polar_coordinate_system"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct PolarCoordinate<TSelf>
    : IPolarCoordinate<TSelf>
    where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
  {
    public static readonly PolarCoordinate<TSelf> Zero;

    private readonly TSelf m_radius;
    private readonly TSelf m_azimuth;

    public PolarCoordinate(TSelf radius, TSelf azimuth)
    {
      m_radius = radius;
      m_azimuth = azimuth;
    }

    public TSelf Radius { get => m_radius; init => m_radius = value; }
    public TSelf Azimuth { get => m_azimuth; init => m_azimuth = value; }

    ///// <summary>Converts the <see cref="PolarCoordinate"/> to a <see cref="Vector2"/>.</summary>
    //[System.Diagnostics.Contracts.Pure]
    //public CartesianCoordinate2<TSelf> ToCartesianCoordinate2()
    // => new(
    //   m_radius * TSelf.Cos(m_azimuth),
    //   m_radius * TSelf.Sin(m_azimuth)
    // );

    ///// <summary>Converts the <see cref="PolarCoordinate"/> to a <see cref="System.Numerics.Complex"/>.</summary>
    //public System.Numerics.Complex ToComplex()
    // => System.Numerics.Complex.FromPolarCoordinates(
    //   double.CreateChecked(m_radius),
    //   double.CreateChecked(m_azimuth)
    // );

    //#region Static methods
    ///// <summary>Return the <see cref="IPolarCoordinate"/> from the specified components.</summary>
    //public static PolarCoordinate<TSelf> From(Quantities.Length radius, Azimuth azimuth)
    //  => new(
    //    TSelf.CreateChecked(radius.Value),
    //    TSelf.CreateChecked(azimuth.ToRadians())
    //  );
    //#endregion Static methods
  }
}
