namespace Flux
{
  /// <summary>Earth related information in terms of WGS-84.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Earth"/>
  /// <see href="https://en.wikipedia.org/wiki/Eccentric_anomaly"/>
  /// <see href="https://en.wikipedia.org/wiki/European_Terrestrial_Reference_System_1989"/>
  /// <see href="https://en.wikipedia.org/wiki/World_Geodetic_System"/>
  public record class EllipsoidReference
  {
    public static EllipsoidReference Etrs89 => new(298.257222101, 6378137.000, 6356752.314140);
    public static EllipsoidReference Wgs84 => new(298.257223563, 6378137.0, 6356752.314245);

    private readonly double m_inverseFlattening;
    private readonly double m_semiMajorAxis;
    private readonly double m_semiMinorAxis;

    public EllipsoidReference(double inverseFlattening, double semiMajorAxis, double semiMinorAxis)
    {
      m_inverseFlattening = inverseFlattening;
      m_semiMajorAxis = semiMajorAxis;
      m_semiMinorAxis = semiMinorAxis;
    }

    public double InverseFlattening { get => m_inverseFlattening; init => m_inverseFlattening = value; }
    public Quantities.Length SemiMajorAxis { get => new(m_semiMajorAxis); init => m_semiMajorAxis = value.Value; }
    public Quantities.Length SemiMinorAxis { get => new(m_semiMinorAxis); init => m_semiMinorAxis = value.Value; }

    /// <summary>The equatorial circumference of Earth is simply the circle perimeter.</summary>
    public Quantities.Length EquatorialCircumference => EquatorialRadius * double.Tau;

    /// <summary>Diameter of Earth's semi-major axis.</summary>
    public Quantities.Length EquatorialDiameter => EquatorialRadius * 2;

    /// <summary>Radius Earth's semi-major axis.</summary>
    public Quantities.Length EquatorialRadius => new(m_semiMajorAxis);

    /// <summary>This is the amount of ellipticity (flattening, oblateness) of the Earth.</summary>
    public double Flattening => 1 / m_inverseFlattening;

    /// <summary>The polar circumference equals Cp=4mp, i.e. four times the quarter meridian.</summary>
    public Quantities.Length PolarCircumference => new(EllipseGeometry.SurfacePerimeter(m_semiMajorAxis, m_semiMinorAxis));

    /// <summary>Diameter of Earth's semi-minor axis.</summary>
    public Quantities.Length PolarDiameter => PolarRadius * 2;

    /// <summary>Radius of Earth's semi-minor axis.</summary>
    public Quantities.Length PolarRadius => new(SemiMinorAxis.Value);

    /// <summary>Approximate volume of the Earth's oblate sphere.</summary>
    public Quantities.Volume Volume => new(GenericMath.PiTimesFourThirds * System.Math.Pow(EquatorialRadius.Value, 2) * PolarRadius.Value);
  }
}
