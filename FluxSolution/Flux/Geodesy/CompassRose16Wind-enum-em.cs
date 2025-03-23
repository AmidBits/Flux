namespace Flux
{
  public static partial class Em
  {
    /// <summary>Returns an extrapolated Azimuth, i.e. a value in the range [0, 359] (every 22.5° notch, starting at 0), from a fifteen value compass point [0, 15].</summary>
    public static Geodesy.Azimuth GetAzimuth(this Geodesy.CompassRose16Wind source) => new(22.5 * (int)source);

    public static System.Collections.Generic.List<string> ToWords(this Geodesy.CompassRose16Wind source) => ((Geodesy.CompassRose32Wind)source).ToWords();
  }
}
