namespace Flux
{
  public static partial class Em
  {
    public static System.DateTime AddTime(this System.DateTime source, Quantities.Time qtime)
      => source.AddSeconds(qtime.Value);
  }
}
