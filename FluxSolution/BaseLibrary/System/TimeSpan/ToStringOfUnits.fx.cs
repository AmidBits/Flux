using Flux.Quantities;

namespace Flux
{
  public static partial class Fx
  {
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
