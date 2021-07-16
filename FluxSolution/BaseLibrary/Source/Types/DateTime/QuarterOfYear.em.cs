namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns the current calendar quarter [1, 4] of the <paramref name="source"/>.</summary>
    public static int QuarterOfYear(this System.DateTime source)
      => ((source.Month - 1) / 3) + 1;
  }
}
