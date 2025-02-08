namespace Flux
{
  public static partial class Em
  {
    public static System.Drawing.Color GetMediaGraphicColor(this Units.UvIndexRisk source)
      => source switch
      {
        Units.UvIndexRisk.Low => System.Drawing.Color.Green,
        Units.UvIndexRisk.Moderate => System.Drawing.Color.Yellow,
        Units.UvIndexRisk.High => System.Drawing.Color.Orange,
        Units.UvIndexRisk.VeryHigh => System.Drawing.Color.Red,
        Units.UvIndexRisk.Extreme => System.Drawing.Color.Violet,
        _ => throw new System.ArgumentOutOfRangeException(nameof(source))
      };
  }
}
