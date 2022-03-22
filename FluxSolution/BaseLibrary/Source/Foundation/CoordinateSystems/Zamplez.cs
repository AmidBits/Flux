#if ZAMPLEZ
namespace Flux
{
  public static partial class Zamplez
  {
    /// <summary>Run the coordinate systems zample.</summary>
    public static void RunCoordinateSystems()
    {
      System.Console.WriteLine();
      System.Console.WriteLine(nameof(RunCoordinateSystems));
      System.Console.WriteLine();

      Draw(GeographicCoordinate.MadridSpain, nameof(GeographicCoordinate.MadridSpain));
      Draw(GeographicCoordinate.PhoenixAzUsa, nameof(GeographicCoordinate.PhoenixAzUsa));
      Draw(GeographicCoordinate.TakapauNewZealand, nameof(GeographicCoordinate.TakapauNewZealand));
      Draw(GeographicCoordinate.TucsonAzUsa, nameof(GeographicCoordinate.TucsonAzUsa));

      static void Draw(GeographicCoordinate gc0, System.ReadOnlySpan<char> label)
      {
        System.Console.WriteLine($"{label.ToString()}:");

        System.Console.WriteLine(gc0);

        var sc = gc0.ToSphericalCoordinate(); // SphericalCoordinate
        System.Console.WriteLine(sc);

        var cc = sc.ToCylindricalCoordinate(); // CylindricalCoordinate
        System.Console.WriteLine(cc);

        var cc3 = cc.ToCartesianCoordinate3(); // CartesianCoordinate3
        System.Console.WriteLine(cc3);

        // Show 2D coordinate systems also.
        {
          var cc2 = new Flux.CartesianCoordinate2(cc3.X, cc3.Y); // CartesianCoordinate2 (2D)
          System.Console.WriteLine($" ({cc2})");

          var pc = cc2.ToPolarCoordinate(); // PolarCoordinate (2D)
          System.Console.WriteLine($" ({pc})");

          var cc2r = pc.ToCartesianCoordinate2(); // CartesianCoordinate2 (2D)
          System.Console.WriteLine($" ({cc2r})");
        }

        var ccr = cc3.ToCylindricalCoordinate(); // CylindricalCoordinate
        System.Console.WriteLine(ccr);

        var scr = ccr.ToSphericalCoordinate(); // SphericalCoordinate
        System.Console.WriteLine(scr);

        var gcr = scr.ToGeographicCoordinate(); // GeographicalCoordinate
        System.Console.WriteLine(gcr);

        System.Console.WriteLine();
      }
    }
  }
}
#endif
