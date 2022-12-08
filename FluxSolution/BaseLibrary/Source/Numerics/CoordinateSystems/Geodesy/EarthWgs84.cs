namespace Flux
{
  /// <summary>Earth related information in terms of WGS-84.</summary>
  public static partial class EarthWgs84
  {
    public const double MeanRadiusInMeters = 6371008.7714; // WGS-84
    public const double QuarterMeridianInMeters = 10001965.729; // WGS-84

    public const double SemiMajorAxisInMeters = 6378137.0; // WGS-84
    public const double SemiMinorAxisInMeters = 6356752.314245; // WGS-84

    /// <summary>The amount of deviation from concentricity.</summary>
    public static double Eccentricity
      => System.Math.Pow(1 - System.Math.Pow(PolarRadius.Value, 2) / System.Math.Pow(EquatorialRadius.Value, 2), 0.5);
    public static double EccentricityOfCrossSection
      => PolarDiameter.Value / EquatorialDiameter.Value;

    /// <summary>The equatorial circumference of Earth is simply the circle perimeter.</summary>
    public static Quantities.Length EquatorialCircumference
      => EquatorialRadius * Constants.PiX2;
    /// <summary>Diameter of Earth's semi-major axis.</summary>
    public static Quantities.Length EquatorialDiameter
      => EquatorialRadius * 2;
    /// <summary>Radius Earth's semi-major axis.</summary>
    public static Quantities.Length EquatorialRadius
      => new(SemiMajorAxisInMeters);

    public static Quantities.Length MeanRadius
      => new(MeanRadiusInMeters); // WGS-84

    /// <summary>This is the amount of oblateness of the Earth.</summary>
    public static double Oblateness
      => (EquatorialRadius.Value - PolarRadius.Value) / EquatorialRadius.Value;

    /// <summary>The polar circumference equals Cp=4mp, i.e. four times the quarter meridian.</summary>
    public static Quantities.Length PolarCircumference
      => new(QuarterMeridianInMeters * 4);
    /// <summary>Diameter of Earth's semi-minor axis.</summary>
    public static Quantities.Length PolarDiameter
      => PolarRadius * 2;
    /// <summary>Radius of Earth's semi-minor axis.</summary>
    public static Quantities.Length PolarRadius
      => new(SemiMinorAxisInMeters);

    /// <summary>Approximate volume of the Earth's oblate sphere.</summary>
    public static Quantities.Volume Volume
      => new(Constants.PiTimesFourThirds * System.Math.Pow(EquatorialRadius.Value, 2) * PolarRadius.Value);
  }
}
