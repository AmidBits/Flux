namespace Flux
{
  public static partial class Em
  {
    /// <summary>Returns an extrapolated Azimuth, i.e. a value in the range [0, 45, 90, 135, 180, 225, 270, 315] (every 45° notch, starting at 0), from an eight value compass point [0, 1, 2, 3, 4, 5, 6, 7].</summary>
    public static Geodesy.Azimuth GetAzimuth(this Geodesy.CompassRose08Wind source) => new(45 * (int)source);

    public static System.Collections.Generic.List<string> ToWords(this Geodesy.CompassRose08Wind source) => ((Geodesy.CompassRose32Wind)source).ToWords();
  }
}
