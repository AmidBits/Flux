namespace Flux
{
  public static partial class Em
  {
    public static double GetUnitPhase(this PlanetaryScience.LunarPhase source)
      => source switch
      {
        PlanetaryScience.LunarPhase.NewMoon => 0,
        PlanetaryScience.LunarPhase.WaxingCrescent => 0.125,
        PlanetaryScience.LunarPhase.FirstQuarter => 0.25,
        PlanetaryScience.LunarPhase.WaxingGibbous => 0.375,
        PlanetaryScience.LunarPhase.FullMoon => 0.5,
        PlanetaryScience.LunarPhase.WaningGibbous => 0.625,
        PlanetaryScience.LunarPhase.LastQuarter => 0.75,
        PlanetaryScience.LunarPhase.WaningCrescent => 0.875,
        _ => throw new NotImplementedException(),
      };
  }
}
