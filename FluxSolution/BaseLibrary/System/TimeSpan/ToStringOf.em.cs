using Flux.Units;

namespace Flux
{
  public static partial class Fx
  {
    public static string ToStringOf(this System.TimeSpan source)
      => string.Join(' ', new string[] {
        (source.Days > 0 ? new Units.Time(source.Days, Units.TimeUnit.Day).ToUnitValueString(Units.TimeUnit.Day) : string.Empty),
        (source.Hours > 0 ? new Units.Time(source.Hours, Units.TimeUnit.Hour).ToUnitValueString(Units.TimeUnit.Hour) : string.Empty),
        (source.Minutes > 0 ? new Units.Time(source.Minutes, Units.TimeUnit.Minute).ToUnitValueString(Units.TimeUnit.Minute) : string.Empty),
        source.Seconds > 0 ? new Units.Time(source.Seconds, Units.TimeUnit.Second).ToUnitValueString(Units.TimeUnit.Second) : string.Empty,
      }.Where(s => s.Length > 0)).Trim();
  }
}
