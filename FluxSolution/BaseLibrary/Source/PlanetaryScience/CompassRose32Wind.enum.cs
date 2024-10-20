namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns an extrapolated Azimuth, i.e. a value in the range [0, 359] (every 11.25°, starting at 0), from a thirty-two value compass point [0, 31]. Each principal wind is 11.25° from its two neighbours.</summary>
    public static Quantities.Azimuth GetAzimuth(this CompassRose32Wind source) => new(11.25 * (int)source);

    public static System.Collections.Generic.List<string> ToWords(this CompassRose32Wind source)
    {
      var list = new System.Collections.Generic.List<string>();

      var s = source.ToString();

      var words = System.Enum.GetNames<WordsOfTheCompass>();

      for (var index = 0; index < s.Length; index++)
      {
        var characterAsString = s[index].ToString();

        foreach (var word in words)
          if (word.StartsWith(characterAsString, StringComparison.CurrentCultureIgnoreCase))
          {
            list.Add(word);

            break;
          }
      }

      return list;
    }
  }

  /// <summary>The 32-wind compass rose is yielded from the eight principal winds, eight half-winds and sixteen quarter-winds combined together, with each compass direction point at an 11.25° angle from the next.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Points_of_the_compass#32-wind_compass_rose"/>
  public enum CompassRose32Wind
  {
    N = 0,
    NbE,
    NNE,
    NEbN,
    NE,
    NEbE,
    ENE,
    EbN,
    E,
    EbS,
    ESE,
    SEbE,
    SE,
    SEbS,
    SSE,
    SbE,
    S,
    SbW,
    SSW,
    SWbS,
    SW,
    SWbW,
    WSW,
    WbS,
    W,
    WbN,
    WNW,
    NWbW,
    NW,
    NWbN,
    NNW,
    NbW
  }
}
