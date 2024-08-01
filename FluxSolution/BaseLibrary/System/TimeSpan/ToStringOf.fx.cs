using Flux.Quantities;

namespace Flux
{
  public static partial class Fx
  {
    public static string ToStringOf(this System.TimeSpan source)
      => string.Join(' ', new string[] {
        (source.Days > 0 ? new Quantities.Time(source.Days, Quantities.TimeUnit.Day).ToUnitValueString(Quantities.TimeUnit.Day) : string.Empty),
        (source.Hours > 0 ? new Quantities.Time(source.Hours, Quantities.TimeUnit.Hour).ToUnitValueString(Quantities.TimeUnit.Hour) : string.Empty),
        (source.Minutes > 0 ? new Quantities.Time(source.Minutes, Quantities.TimeUnit.Minute).ToUnitValueString(Quantities.TimeUnit.Minute) : string.Empty),
        source.Seconds > 0 ? new Quantities.Time(source.Seconds, Quantities.TimeUnit.Second).ToUnitValueString(Quantities.TimeUnit.Second) : string.Empty,
      }.Where(s => s.Length > 0)).Trim();
  }
}
