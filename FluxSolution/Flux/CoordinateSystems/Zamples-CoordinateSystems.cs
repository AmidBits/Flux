#if DEBUG
namespace Flux
{
  public static partial class Zamples
  {

    /// <summary>This is a reference coordinate for Madrid, Spain, which is antipodal to Takapau, New Zeeland.</summary>
    public static Flux.CoordinateSystems.GeographicCoordinate MadridSpain => new(40.416944, Flux.Units.AngleUnit.Degree, -3.703333, Flux.Units.AngleUnit.Degree, 650);

    /// <summary>This is a reference coordinate for Takapau, New Zeeland, which is antipodal to Madrid, Spain.</summary>
    public static Flux.CoordinateSystems.GeographicCoordinate TakapauNewZealand => new(-40.033333, Flux.Units.AngleUnit.Degree, 176.35, Flux.Units.AngleUnit.Degree, 235);

    /// <summary>This is a reference point for Phoenix, Arizona, USA, from where the C# version of this library originated.</summary>
    public static Flux.CoordinateSystems.GeographicCoordinate PhoenixAzUsa => new(33.448333, Flux.Units.AngleUnit.Degree, -112.073889, Flux.Units.AngleUnit.Degree, 331);
    /// <summary>This is a reference point for Tucson, Arizona, USA, from where the C# version of this library originated.</summary>
    public static Flux.CoordinateSystems.GeographicCoordinate TucsonAzUsa => new(32.221667, Flux.Units.AngleUnit.Degree, -110.926389, Flux.Units.AngleUnit.Degree, 728);

    /// <summary>Run the coordinate systems zample.</summary>
    public static void CoordinateSystemsGeneral()
    {
      Draw(MadridSpain, nameof(MadridSpain));
      Draw(PhoenixAzUsa, nameof(PhoenixAzUsa));
      Draw(TakapauNewZealand, nameof(TakapauNewZealand));
      Draw(TucsonAzUsa, nameof(TucsonAzUsa));

      static void Draw(Flux.CoordinateSystems.GeographicCoordinate gc, System.ReadOnlySpan<char> label)
      {
        System.Diagnostics.Debug.WriteLine($"{label.ToString()}:");

        System.Console.WriteLine($"Geographical: {gc}");

        var sca = gc.ToSphericalCoordinate(); System.Console.WriteLine($"Spherical: {sca}");
        var cca = sca.ToCylindricalCoordinate(); System.Console.WriteLine($"Cylindrical: {cca}");
        var cc3a = cca.ToVector3(); System.Console.WriteLine($"Vector: {cc3a}");
        var cc4a = cca.ToCartesianCoordinate(); System.Console.WriteLine($"(to Cartesian): {cc4a}");
        var cc5a = cc4a.ToCylindricalCoordinate(); System.Console.WriteLine($"(back to Cylindrical): {cc5a}");
        var cc6a = cc4a.ToSphericalCoordinate(); System.Console.WriteLine($"(and to Spherical): {cc6a}");

        System.Diagnostics.Debug.WriteLine($" Sub 2D coordinate show-case from the 3D components X and Y."); // Show 2D coordinate systems also.
        var pca = cca.ToPolarCoordinate(); System.Console.Write(' '); System.Console.WriteLine($"Polar: {pca}");

        var ccb = pca.ToCylindricalCoordinate(cca.Height); System.Console.WriteLine($"Cylindrical: {ccb}");
        var scb = ccb.ToSphericalCoordinate(); System.Console.WriteLine($"Spherical: {scb}");
        var gcb = scb.ToGeographicCoordinate(); System.Console.WriteLine($"Geographical: {gcb}");

        System.Console.WriteLine();
      }
    }
  }
}
#endif
