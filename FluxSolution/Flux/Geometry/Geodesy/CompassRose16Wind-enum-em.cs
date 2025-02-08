namespace Flux
{
  public static partial class Em
  {
    /// <summary>Returns an extrapolated Azimuth, i.e. a value in the range [0, 359] (every 22.5° notch, starting at 0), from a fifteen value compass point [0, 15].</summary>
    public static Geometry.Geodesy.Azimuth GetAzimuth(this Geometry.Geodesy.CompassRose16Wind source) => new(22.5 * (int)source);

    public static System.Collections.Generic.List<string> ToWords(this Geometry.Geodesy.CompassRose16Wind source) => ((Geometry.Geodesy.CompassRose32Wind)source).ToWords();
  }
}
