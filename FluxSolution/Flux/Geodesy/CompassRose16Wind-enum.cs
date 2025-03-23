namespace Flux.Geodesy
{
  /// <summary>The eight principal winds (or main winds) are the four cardinals and four intercardinals considered together, that is: N, NE, E, SE, S, SW, W, NW. Each principal wind is 22.5° from its two neighbours. The directional values are the degrees they represent.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Points_of_the_compass#8-wind_compass_rose"/>
  public enum CompassRose16Wind
  {
    N = (int)CompassRose32Wind.N,
    NNE = (int)CompassRose32Wind.NNE,
    NE = (int)CompassRose32Wind.NE,
    ENE = (int)CompassRose32Wind.ENE,
    E = (int)CompassRose32Wind.E,
    ESE = (int)CompassRose32Wind.ESE,
    SE = (int)CompassRose32Wind.SE,
    SSE = (int)CompassRose32Wind.SSE,
    S = (int)CompassRose32Wind.S,
    SSW = (int)CompassRose32Wind.SSW,
    SW = (int)CompassRose32Wind.SW,
    WSW = (int)CompassRose32Wind.WSW,
    W = (int)CompassRose32Wind.W,
    WNW = (int)CompassRose32Wind.WNW,
    NW = (int)CompassRose32Wind.NW,
    NNW = (int)CompassRose32Wind.NNW
  }
}
