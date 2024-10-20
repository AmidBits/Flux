namespace Flux
{
  public static partial class Em
  {
    public static double GetUnitPhase(this LunarPhase source)
      => source switch
      {
        LunarPhase.NewMoon => 0,
        LunarPhase.WaxingCrescent => 0.125,
        LunarPhase.FirstQuarter => 0.25,
        LunarPhase.WaxingGibbous => 0.375,
        LunarPhase.FullMoon => 0.5,
        LunarPhase.WaningGibbous => 0.625,
        LunarPhase.LastQuarter => 0.75,
        LunarPhase.WaningCrescent => 0.875,
        _ => throw new NotImplementedException(),
      };
  }

  public enum LunarPhase
  {
    NewMoon,
    WaxingCrescent,
    FirstQuarter,
    WaxingGibbous,
    FullMoon,
    WaningGibbous,
    LastQuarter,
    WaningCrescent
  }
}
