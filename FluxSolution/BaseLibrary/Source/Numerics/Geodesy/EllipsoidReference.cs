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
    private readonly Geometry.EllipseGeometry m_ellipseGeometry;

    public EllipsoidReference(double inverseFlattening, double semiMajorAxis, double semiMinorAxis)
    {
      m_inverseFlattening = inverseFlattening;
      m_ellipseGeometry = new(semiMajorAxis, semiMinorAxis);
    }

    public double InverseFlattening { get => m_inverseFlattening; init => m_inverseFlattening = value; }
    public Units.Length SemiMajorAxis => new(m_ellipseGeometry.X);
    public Units.Length SemiMinorAxis => new(m_ellipseGeometry.Y);

    /// <summary>The equatorial circumference of Earth is simply the circle perimeter.</summary>
    public Units.Length EquatorialCircumference => EquatorialRadius * double.Tau;

    /// <summary>Diameter of Earth's semi-major axis.</summary>
    public Units.Length EquatorialDiameter => EquatorialRadius * 2;

    /// <summary>Radius Earth's semi-major axis.</summary>
    public Units.Length EquatorialRadius => new(m_ellipseGeometry.X);

    /// <summary>This is the amount of ellipticity (flattening, oblateness) of the Earth.</summary>
    public double Flattening => 1 / m_inverseFlattening;

    /// <summary>The polar circumference equals Cp=4mp, i.e. four times the quarter meridian.</summary>
    public Units.Length PolarCircumference => new(m_ellipseGeometry.Circumference);

    /// <summary>Diameter of Earth's semi-minor axis.</summary>
    public Units.Length PolarDiameter => PolarRadius * 2;

    /// <summary>Radius of Earth's semi-minor axis.</summary>
    public Units.Length PolarRadius => new(m_ellipseGeometry.Y);

    /// <summary>Approximate volume of the Earth's oblate sphere.</summary>
    public Units.Volume Volume => new(GenericMath.PiTimesFourThirds * System.Math.Pow(EquatorialRadius.Value, 2) * PolarRadius.Value);
  }
}
