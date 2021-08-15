namespace Flux
{
  public static class Earth
  {
    /// <summary>The amount of deviation from concentricity.</summary>
    public static double Eccentricity
      => System.Math.Pow(1 - System.Math.Pow(PolarRadius.Value, 2) / System.Math.Pow(EquatorialRadius.Value, 2), 0.5);
    public static double EccentricityOfCrossSection
      => PolarDiameter.Value / EquatorialDiameter.Value;

    /// <summary>The equatorial circumference of Earth is simply the circle perimeter.</summary>
    public static Quantity.Length EquatorialCircumference
      => EquatorialRadius * System.Math.PI * 2;
    /// <summary>Diameter of Earth's semi-major axis.</summary>
    public static Quantity.Length EquatorialDiameter
      => EquatorialRadius * 2;
    /// <summary>Radius Earth's semi-major axis.</summary>
    public static Quantity.Length EquatorialRadius
      => new Quantity.Length(6378137.0); // WGS-84

    public static Quantity.Length MeanRadius
      => new Quantity.Length(6371008.7714); // WGS-84

    public static double Oblateness
      => (EquatorialRadius.Value - PolarRadius.Value) / EquatorialRadius.Value;

    /// <summary>The polar circumference equals Cp=4mp, i.e. four times the quarter meridian.</summary>
    public static Quantity.Length PolarCircumference
      => QuarterMeridian * 4;
    /// <summary>Diameter of Earth's semi-minor axis.</summary>
    public static Quantity.Length PolarDiameter
      => PolarRadius * 2;
    /// <summary>Radius of Earth's semi-minor axis.</summary>
    public static Quantity.Length PolarRadius
      => new Quantity.Length(6356752.3142); // WGS-84

    public static Quantity.Length QuarterMeridian
      => new Quantity.Length(10001965.729); // WGS-84

    public static Quantity.Volume Volume
      => new Quantity.Volume((4.0 / 3.0) * System.Math.PI * System.Math.Pow(EquatorialRadius.Value, 2) * PolarRadius.Value);
  }
}
