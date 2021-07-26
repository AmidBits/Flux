namespace Flux
{
  public static class Earth
  {
    /// <summary>The amount of deviation from concentricity.</summary>
    public static double Eccentricity
      => System.Math.Pow(1 - System.Math.Pow(PolarRadius.Value, 2) / System.Math.Pow(EquatorialRadius.Value, 2), 0.5);

    public static double EccentricityOfCrossSection
      => (PolarRadius.Value + PolarRadius.Value) / (EquatorialRadius.Value + EquatorialRadius.Value);
    public static double EccentricityOfCrossSection2
      => PolarCircumference.Value / EquatorialCircumference.Value;
    public static double Oblateness
      => (EquatorialRadius.Value - PolarRadius.Value) / EquatorialRadius.Value;

    /// <summary>Earth's semi-major axis.</summary>
    public static Quantity.Length EquatorialRadius
      => new Quantity.Length(6378137.0);
    public static Quantity.Length MeanRadius
      => new Quantity.Length(6371008.8);
    /// <summary>Earth's semi-minor axis.</summary>
    public static Quantity.Length PolarRadius
      => new Quantity.Length(6356752.3142);


    /// <summary>The equatorial circumference is simply the circle perimeter.</summary>
    public static Quantity.Length EquatorialCircumference
      => new Quantity.Length(2 * System.Math.PI * EquatorialRadius.Value);
    /// <summary>The equatorial circumference is simply the circle perimeter.</summary>
    public static Quantity.Length PolarCircumference
      => new Quantity.Length(2 * System.Math.PI * PolarRadius.Value);

    public static Quantity.Volume Volume
      => new Quantity.Volume((4.0 / 3.0) * System.Math.PI * System.Math.Pow(EquatorialRadius.Value, 2) * PolarRadius.Value);
  }
}
