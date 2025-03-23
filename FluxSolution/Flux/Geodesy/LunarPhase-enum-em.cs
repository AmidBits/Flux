namespace Flux
{
  public static partial class Em
  {
    public static double GetUnitPhase(this Geodesy.LunarPhase source)
      => source switch
      {
        Geodesy.LunarPhase.NewMoon => 0,
        Geodesy.LunarPhase.WaxingCrescent => 0.125,
        Geodesy.LunarPhase.FirstQuarter => 0.25,
        Geodesy.LunarPhase.WaxingGibbous => 0.375,
        Geodesy.LunarPhase.FullMoon => 0.5,
        Geodesy.LunarPhase.WaningGibbous => 0.625,
        Geodesy.LunarPhase.LastQuarter => 0.75,
        Geodesy.LunarPhase.WaningCrescent => 0.875,
        _ => throw new NotImplementedException(),
      };
  }
}
