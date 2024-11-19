namespace Flux
{
  /// <summary>
  /// <para>An ellipsoid reference, which is an object yielding Earth related information in terms of an ellipsoid from specific reference values.</para>
  /// <para>In geodesy, a reference ellipsoid is a mathematically defined surface that approximates the geoid, which is the truer, imperfect figure of the Earth, or other planetary body, as opposed to a perfect, smooth, and unaltered sphere, which factors in the undulations of the bodies' gravity due to variations in the composition and density of the interior, as well as the subsequent flattening caused by the centrifugal force from the rotation of these massive objects (for planetary bodies that do rotate).</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Earth"/></para>
  /// <para><see href="https://en.wikipedia.org/wiki/Eccentric_anomaly"/></para>
  /// <para><see href="https://en.wikipedia.org/wiki/European_Terrestrial_Reference_System_1989"/></para>
  /// <para><see href="https://en.wikipedia.org/wiki/World_Geodetic_System"/></para>
  /// </summary>
  public record class EllipsoidReference
  {
    public const double EarthMeanRadius = 6371008.8;

    public static EllipsoidReference Etrs89 { get; } = new(298.257222101, 6378137.000, 6356752.314140);
    public static EllipsoidReference Wgs84 { get; } = new(298.257223563, 6378137.0, 6356752.314245);

    private readonly double m_inverseFlattening;
    private readonly double m_semiMajorAxis;
    private readonly double m_semiMinorAxis;

    public EllipsoidReference(double inverseFlattening, double semiMajorAxis, double semiMinorAxis)
    {
      m_inverseFlattening = inverseFlattening;
      m_semiMajorAxis = semiMajorAxis;
      m_semiMinorAxis = semiMinorAxis;
    }

    public double InverseFlattening => m_inverseFlattening;
    public double SemiMajorAxis => m_semiMajorAxis;
    public double SemiMinorAxis => m_semiMinorAxis;

    /// <summary>The equatorial circumference of Earth is simply the circle perimeter.</summary>
    public double EquatorialCircumference => EquatorialRadius * double.Tau;

    /// <summary>Diameter of Earth's semi-major axis.</summary>
    public double EquatorialDiameter => 2 * m_semiMajorAxis;

    /// <summary>Radius Earth's semi-major axis.</summary>
    public double EquatorialRadius => m_semiMajorAxis;

    /// <summary>This is the amount of ellipticity (flattening, oblateness) of the Earth.</summary>
    public double Flattening => 1 / m_inverseFlattening;

    /// <summary>The polar circumference equals Cp=4mp, i.e. four times the quarter meridian.</summary>
    public double PolarCircumference => Coordinates.PolarCoordinate.PerimeterOfEllipse(m_semiMajorAxis, m_semiMinorAxis);

    /// <summary>Diameter of Earth's semi-minor axis.</summary>
    public double PolarDiameter => 2 * m_semiMinorAxis;

    /// <summary>Radius of Earth's semi-minor axis.</summary>
    public double PolarRadius => m_semiMinorAxis;

    /// <summary>Approximate volume of the Earth's oblate sphere.</summary>
    public double Volume => double.Pi * (4d / 3d) * double.Pow(EquatorialRadius, 2) * PolarRadius;
  }
}
