namespace Flux
{
  public static partial class Fx
  {
    public static string ToStringOfUnits(this System.TimeSpan source, bool useFullNames)
    {
      var sm = new SpanMaker<char>();

      if (source.Days > 0)
      {
        var td = new Quantities.Time(source.Days, Quantities.TimeUnit.Day);
        sm = sm.Append(td.ToUnitString(Quantities.TimeUnit.Day));
      }

      sm = sm.Append(' ');

      if (source.Hours > 0)
      {
        var th = new Quantities.Time(source.Hours, Quantities.TimeUnit.Hour);
        sm = sm.Append(th.ToUnitString(Quantities.TimeUnit.Hour));
      }

      sm = sm.Append(' ');

      if (source.Minutes > 0)
      {
        var tm = new Quantities.Time(source.Minutes, Quantities.TimeUnit.Minute);
        sm = sm.Append(tm.ToUnitString(Quantities.TimeUnit.Minute));
      }

      sm = sm.Append(' ');

      if (source.Seconds > 0)
      {
        var ts = new Quantities.Time(source.Seconds, Quantities.TimeUnit.Second);
        sm = sm.Append(ts.ToUnitString(Quantities.TimeUnit.Second));
      }

      return sm.ToString();
    }
  }
}
