using System.Linq;

namespace Flux
{
  public static partial class Reflection
  {
    public static string ToStringOf(this System.TimeSpan source)
      => string.Join(' ', new string[] {
        (source.Days > 0 ? new Units.Time(source.Days, Units.TimeUnit.Day).ToUnitString(Units.TimeUnit.Day) : string.Empty),
        (source.Hours > 0 ? new Units.Time(source.Hours, Units.TimeUnit.Hour).ToUnitString(Units.TimeUnit.Hour) : string.Empty),
        (source.Minutes > 0 ? new Units.Time(source.Minutes, Units.TimeUnit.Minute).ToUnitString(Units.TimeUnit.Minute) : string.Empty),
        source.Seconds > 0 ? new Units.Time(source.Seconds, Units.TimeUnit.Second).ToUnitString(Units.TimeUnit.Second) : string.Empty,
      }.Where(s => s.Length > 0)).Trim();
  }
}
