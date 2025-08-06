#if DEBUG
namespace Flux
{
  public static partial class Zamples
  {
    /// <summary>Run the coordinate systems zample.</summary>
    public static void CoordinateSystemsGeneral()
    {
      //System.Console.WriteLine();
      System.Console.WriteLine(nameof(RunCoordinateSystems));
      System.Console.WriteLine();

      Draw(MadridSpain, nameof(MadridSpain));
      Draw(PhoenixAzUsa, nameof(PhoenixAzUsa));
      Draw(TakapauNewZealand, nameof(TakapauNewZealand));
      Draw(TucsonAzUsa, nameof(TucsonAzUsa));

      static void Draw(Flux.CoordinateSystems.GeographicCoordinate gc, System.ReadOnlySpan<char> label)
      {
        Flux.Console.WriteInformationLine($"{label.ToString()}:");

        System.Console.WriteLine($"Geographical: {gc}");

        var sca = gc.ToSphericalCoordinate(); System.Console.WriteLine($"Spherical: {sca}");
        var cca = sca.ToCylindricalCoordinate(); System.Console.WriteLine($"Cylindrical: {cca}");
        var cc3a = cca.ToVector3(); System.Console.WriteLine($"Vector: {cc3a}");
        var cc4a = cca.ToCartesianCoordinate(); System.Console.WriteLine($"(to Cartesian): {cc4a}");
        var cc5a = cc4a.ToCylindricalCoordinate(); System.Console.WriteLine($"(back to Cylindrical): {cc5a}");
        var cc6a = cc4a.ToSphericalCoordinate(); System.Console.WriteLine($"(and to Spherical): {cc6a}");

        Flux.Console.WriteWarningLine($" Sub 2D coordinate show-case from the 3D components X and Y."); // Show 2D coordinate systems also.
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
