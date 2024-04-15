namespace Flux
{
  /// <summary>
  /// <para>An ellipsoid reference, which is an object yielding Earth related information in terms of an ellipsoid from specific reference values.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Earth"/></para>
  /// <para><see href="https://en.wikipedia.org/wiki/Eccentric_anomaly"/></para>
  /// <para><see href="https://en.wikipedia.org/wiki/European_Terrestrial_Reference_System_1989"/></para>
  /// <para><see href="https://en.wikipedia.org/wiki/World_Geodetic_System"/></para>
  /// </summary>
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
    public double SemiMajorAxis => m_semiMajorAxis;
    public double SemiMinorAxis => m_semiMinorAxis;

    /// <summary>The equatorial circumference of Earth is simply the circle perimeter.</summary>
    public double EquatorialCircumference => EquatorialRadius * System.Math.Tau;

    ///// <summary>Diameter of Earth's semi-major axis.</summary>
    //public double EquatorialDiameter => EquatorialRadius * 2;

    /// <summary>Radius Earth's semi-major axis.</summary>
    public double EquatorialRadius => m_semiMajorAxis;

    /// <summary>This is the amount of ellipticity (flattening, oblateness) of the Earth.</summary>
    public double Flattening => 1 / m_inverseFlattening;

    /// <summary>The polar circumference equals Cp=4mp, i.e. four times the quarter meridian.</summary>
    public double PolarCircumference => Geometry.EllipseGeometry.PerimeterOfEllipse(m_semiMajorAxis, m_semiMinorAxis);

    ///// <summary>Diameter of Earth's semi-minor axis.</summary>
    //public double PolarDiameter => PolarRadius * 2;

    /// <summary>Radius of Earth's semi-minor axis.</summary>
    public double PolarRadius => m_semiMinorAxis;

    /// <summary>Approximate volume of the Earth's oblate sphere.</summary>
    public double Volume => (System.Math.PI * 4.0 / 3.0) * System.Math.Pow(EquatorialRadius, 2) * PolarRadius;
  }
}
