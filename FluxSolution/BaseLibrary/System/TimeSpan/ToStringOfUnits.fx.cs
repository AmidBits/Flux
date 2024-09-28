namespace Flux
{
  public static partial class Fx
  {
    public static string ToStringOfUnits(this System.TimeSpan source, bool useFullNames)
    {
      var sb = new System.Text.StringBuilder();

      if (source.Days > 0)
      {
        var td = new Quantities.Time(source.Days, Quantities.TimeUnit.Day);
        sb.Append(td.ToUnitString(Quantities.TimeUnit.Day));
      }

      sb.Append(' ');

      if (source.Hours > 0)
      {
        var th = new Quantities.Time(source.Hours, Quantities.TimeUnit.Hour);
        sb.Append(th.ToUnitString(Quantities.TimeUnit.Hour));
      }

      sb.Append(' ');

      if (source.Minutes > 0)
      {
        var tm = new Quantities.Time(source.Minutes, Quantities.TimeUnit.Minute);
        sb.Append(tm.ToUnitString(Quantities.TimeUnit.Minute));
      }

      sb.Append(' ');

      if (source.Seconds > 0)
      {
        var ts = new Quantities.Time(source.Seconds, Quantities.TimeUnit.Second);
        sb.Append(ts.ToUnitString(Quantities.TimeUnit.Second));
      }

      return sb.ToString();
    }
  }
}
