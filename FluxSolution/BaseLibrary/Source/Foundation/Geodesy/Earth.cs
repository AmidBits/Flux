namespace Flux
{
  /// <summary>Earth related information.</summary>
  public static partial class Earth
  {
    /// <summary>The amount of deviation from concentricity.</summary>
    public static double Eccentricity
      => System.Math.Pow(1 - System.Math.Pow(PolarRadiusWgs84.Value, 2) / System.Math.Pow(EquatorialRadiusWgs84.Value, 2), 0.5);
    public static double EccentricityOfCrossSection
      => PolarDiameterWgs84.Value / EquatorialDiameter.Value;

    /// <summary>The equatorial circumference of Earth is simply the circle perimeter.</summary>
    public static Length EquatorialCircumference
      => EquatorialRadiusWgs84 * Maths.PiX2;
    /// <summary>Diameter of Earth's semi-major axis.</summary>
    public static Length EquatorialDiameter
      => EquatorialRadiusWgs84 * 2;
    /// <summary>Radius Earth's semi-major axis.</summary>
    public static Length EquatorialRadiusWgs84
      => new(6378137.0); // WGS-84

    public static Length MeanRadiusWgs84
      => new(6371008.7714); // WGS-84

    public static double Oblateness
      => (EquatorialRadiusWgs84.Value - PolarRadiusWgs84.Value) / EquatorialRadiusWgs84.Value;

    /// <summary>The polar circumference equals Cp=4mp, i.e. four times the quarter meridian.</summary>
    public static Length PolarCircumferenceWgs84
      => QuarterMeridianWgs84 * 4;
    /// <summary>Diameter of Earth's semi-minor axis.</summary>
    public static Length PolarDiameterWgs84
      => PolarRadiusWgs84 * 2;
    /// <summary>Radius of Earth's semi-minor axis.</summary>
    public static Length PolarRadiusWgs84
      => new(6356752.3142); // WGS-84

    public static Length QuarterMeridianWgs84
      => new(10001965.729); // WGS-84

    public static Volume Volume
      => new(Maths.PiTimesFourThirds * System.Math.Pow(EquatorialRadiusWgs84.Value, 2) * PolarRadiusWgs84.Value);
  }
}
