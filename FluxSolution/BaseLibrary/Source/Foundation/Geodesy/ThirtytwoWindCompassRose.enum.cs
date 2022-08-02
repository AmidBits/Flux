namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns an extrapolated Azimuth, i.e. a value in the range [0, 359] (every 11.25°, starting at 0), from a thirty-two value compass point [0, 31]. Each principal wind is 11.25° from its two neighbours.</summary>
    public static Azimuth GetAzimuth(this ThirtytwoWindCompassRose thirtyTwoWindCompassRose)
      => new Azimuth(11.25 * (int)thirtyTwoWindCompassRose);
    public static string ToStringOfWords(this ThirtytwoWindCompassRose thirtyTwoWindCompassRose)
    {
      var sb = new Flux.SpanBuilder<char>();

      var s = thirtyTwoWindCompassRose.ToString().AsSpan();

      for (var index = 0; index < s.Length; index++)
      {
        if (index > 0)
          sb.Append(' ');

        switch (s[index])
        {
          case 'N':
            sb.Append("north");
            break;
          case 'E':
            sb.Append("east");
            break;
          case 'S':
            sb.Append("south");
            break;
          case 'W':
            sb.Append("west");
            break;
          case 'b':
            sb.Append("by");
            break;
          default:
            throw new System.ArgumentOutOfRangeException(nameof(thirtyTwoWindCompassRose));
        }
      }

      return sb.ToString(0);
    }
  }

  /// <summary>The 32-wind compass rose is yielded from the eight principal winds, eight half-winds and sixteen quarter-winds combined together, with each compass direction point at an 11.25° angle from the next.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Points_of_the_compass#32-wind_compass_rose"/>
  public enum ThirtytwoWindCompassRose
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
