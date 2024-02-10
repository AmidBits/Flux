using System.Linq;
using Flux.Units;

namespace Flux
{
  public static partial class Fx
  {
    public static string ToStringOf(this System.TimeSpan source)
      => string.Join(' ', new string[] {
        (source.Days > 0 ? new Units.Time(source.Days, Units.TimeUnit.Day).ToUnitValueString(Units.TimeUnit.Day, TextOptions.Default) : string.Empty),
        (source.Hours > 0 ? new Units.Time(source.Hours, Units.TimeUnit.Hour).ToUnitValueString(Units.TimeUnit.Hour, TextOptions.Default) : string.Empty),
        (source.Minutes > 0 ? new Units.Time(source.Minutes, Units.TimeUnit.Minute).ToUnitValueString(Units.TimeUnit.Minute, TextOptions.Default) : string.Empty),
        source.Seconds > 0 ? new Units.Time(source.Seconds, Units.TimeUnit.Second).ToUnitValueString(Units.TimeUnit.Second, TextOptions.Default) : string.Empty,
      }.Where(s => s.Length > 0)).Trim();
  }
}
