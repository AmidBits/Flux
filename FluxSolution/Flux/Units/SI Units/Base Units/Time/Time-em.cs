namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static System.DateTime AddTime(this System.DateTime source, Units.Time qtime)
      => source.AddSeconds(qtime.Value);
  }
}
