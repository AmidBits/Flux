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

    public static string ToStringOfUnits(this System.TimeSpan source, bool useFullNames)
    {
      var sb = new System.Text.StringBuilder();

      if(source.Days > 0)
        sb.Append(new Quantities.Time(source.Days, Quantities.TimeUnit.Day).ToUnitValueString(Quantities.TimeUnit.Day, preferUnicode: false, useFullName: useFullNames));
      if(source.Hours > 0)
        sb.Append(new Quantities.Time(source.Hours, Quantities.TimeUnit.Hour).ToUnitValueString(Quantities.TimeUnit.Hour, preferUnicode: false, useFullName: useFullNames));
      if(source.Minute > 0)
        sb.Append(new Quantities.Time(source.Minute, Quantities.TimeUnit.Minute).ToUnitValueString(Quantities.TimeUnit.Minute, preferUnicode: false, useFullName: useFullNames));
      if(source.Second > 0)
        sb.Append(new Quantities.Time(source.Second, Quantities.TimeUnit.Second).ToUnitValueString(Quantities.TimeUnit.Second, preferUnicode: false, useFullName: useFullNames));

      return sb.ToString();
    }
  }
}
