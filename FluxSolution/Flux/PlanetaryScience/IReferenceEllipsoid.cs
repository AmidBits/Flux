//namespace Flux.PlanetaryScience
//{
//  /// <summary>
//  /// <para>An ellipsoid reference, which is an object yielding Earth related information in terms of an ellipsoid from specific reference values.</para>
//  /// <para>In geodesy, a reference ellipsoid is a mathematically defined surface that approximates the geoid, which is the truer, imperfect figure of the Earth, or other planetary body, as opposed to a perfect, smooth, and unaltered sphere, which factors in the undulations of the bodies' gravity due to variations in the composition and density of the interior, as well as the subsequent flattening caused by the centrifugal force from the rotation of these massive objects (for planetary bodies that do rotate).</para>
//  /// <para><see href="https://en.wikipedia.org/wiki/Earth"/></para>
//  /// <para><see href="https://en.wikipedia.org/wiki/Eccentric_anomaly"/></para>
//  /// <para><see href="https://en.wikipedia.org/wiki/European_Terrestrial_Reference_System_1989"/></para>
//  /// <para><see href="https://en.wikipedia.org/wiki/World_Geodetic_System"/></para>
//  /// </summary>
//  public interface IReferenceEllipsoid
//  {
//    public double InverseFlattening { get; }
//    public double SemiMajorAxis { get; }
//    public double SemiMinorAxis { get; }

//    /// <summary>The equatorial circumference of Earth is simply the circle perimeter.</summary>
//    public double EquatorialCircumference => EquatorialRadius * double.Tau;

//    /// <summary>Diameter of Earth's semi-major axis.</summary>
//    public double EquatorialDiameter => 2 * SemiMajorAxis;

//    /// <summary>Radius Earth's semi-major axis.</summary>
//    public double EquatorialRadius => SemiMajorAxis;

//    /// <summary>This is the amount of ellipticity (flattening, oblateness) of the Earth.</summary>
//    public double Flattening => 1 / InverseFlattening;
//    public double Flattening2 => (SemiMajorAxis - SemiMinorAxis) / SemiMajorAxis;

//    /// <summary>The polar circumference equals Cp=4mp, i.e. four times the quarter meridian.</summary>
//    public double PolarCircumference => Units.Length.OfEllipsePerimeter(SemiMajorAxis, SemiMinorAxis);

//    /// <summary>Diameter of Earth's semi-minor axis.</summary>
//    public double PolarDiameter => 2 * SemiMinorAxis;

//    /// <summary>Radius of Earth's semi-minor axis.</summary>
//    public double PolarRadius => SemiMinorAxis;

//    /// <summary>Approximate volume of the Earth's oblate sphere.</summary>
//    public double Volume => double.Pi * (4d / 3d) * double.Pow(EquatorialRadius, 2) * PolarRadius;
//  }
//}
