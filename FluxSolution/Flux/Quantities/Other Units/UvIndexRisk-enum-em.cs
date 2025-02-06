namespace Flux
{
  public static partial class Em
  {
    public static System.Drawing.Color GetMediaGraphicColor(this Quantities.UvIndexRisk source)
      => source switch
      {
        Quantities.UvIndexRisk.Low => System.Drawing.Color.Green,
        Quantities.UvIndexRisk.Moderate => System.Drawing.Color.Yellow,
        Quantities.UvIndexRisk.High => System.Drawing.Color.Orange,
        Quantities.UvIndexRisk.VeryHigh => System.Drawing.Color.Red,
        Quantities.UvIndexRisk.Extreme => System.Drawing.Color.Violet,
        _ => throw new System.ArgumentOutOfRangeException(nameof(source))
      };
  }
}
