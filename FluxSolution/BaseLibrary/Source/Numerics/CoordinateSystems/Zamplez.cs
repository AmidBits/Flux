#if ZAMPLEZ
using Flux.Numerics;

namespace Flux
{
  public static partial class Zamplez
  {
    /// <summary>Run the coordinate systems zample.</summary>
    public static void RunCoordinateSystems()
    {
      //System.Console.WriteLine();
      System.Console.WriteLine(nameof(RunCoordinateSystems));
      System.Console.WriteLine();

      Draw(Numerics.GeographicCoordinate.MadridSpain, nameof(Numerics.GeographicCoordinate.MadridSpain));
      Draw(Numerics.GeographicCoordinate.PhoenixAzUsa, nameof(Numerics.GeographicCoordinate.PhoenixAzUsa));
      Draw(Numerics.GeographicCoordinate.TakapauNewZealand, nameof(Numerics.GeographicCoordinate.TakapauNewZealand));
      Draw(Numerics.GeographicCoordinate.TucsonAzUsa, nameof(Numerics.GeographicCoordinate.TucsonAzUsa));

      static void Draw(Numerics.GeographicCoordinate gc, System.ReadOnlySpan<char> label)
      {
        Flux.Console.WriteInformationLine($"{label.ToString()}:");

        System.Console.WriteLine(gc);

        var sca = gc.ToSphericalCoordinate(); System.Console.WriteLine(sca);
        var cca = sca.ToCylindricalCoordinate(); System.Console.WriteLine(cca);
        var cc3a = cca.ToCartesianCoordinate3(); System.Console.WriteLine(cc3a);

        // Show 2D coordinate systems also.
        {
          Flux.Console.WriteWarningLine($" Sub 2D coordinate show-case from the 3D components X and Y.");

          var cc2a = cc3a.ToCartesianCoordinate2XY(); System.Console.Write(' '); System.Console.WriteLine(cc2a);
          var pca = cc2a.ToPolarCoordinate(); System.Console.Write(' '); System.Console.WriteLine(pca);
          var cc2b = pca.ToCartesianCoordinate2(); System.Console.Write(' '); System.Console.WriteLine(cc2b);
        }

        var ccb = cc3a.ToCylindricalCoordinate(); System.Console.WriteLine(ccb);
        var scb = ccb.ToSphericalCoordinate(); System.Console.WriteLine(scb);
        var gcb = scb.ToGeographicCoordinate(); System.Console.WriteLine(gcb);

        System.Console.WriteLine();
      }
    }
  }
}
#endif
