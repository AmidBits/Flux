namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns the current calendar quarter in the range [1, 4] of the <paramref name="source"/>.</summary>
    public static int QuarterOfYear(this System.DateTime source) => ((source.Month - 1) / 3) + 1;
  }
}
