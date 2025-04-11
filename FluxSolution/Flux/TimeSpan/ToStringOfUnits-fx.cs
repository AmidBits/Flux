namespace Flux
{
  public static partial class Fx
  {
    public static string ToStringOfUnits(this System.TimeSpan source, bool fullNames)
    {
      var sm = new SpanMaker<char>();

      if (source.Days > 0)
      {
        var td = new Units.Time(source.Days, Units.TimeUnit.Day);
        sm = sm.Append(td.ToUnitString(Units.TimeUnit.Day, null, null, fullNames));
      }

      sm = sm.Append(' ');

      if (source.Hours > 0)
      {
        var th = new Units.Time(source.Hours, Units.TimeUnit.Hour);
        sm = sm.Append(th.ToUnitString(Units.TimeUnit.Hour, null, null, fullNames));
      }

      sm = sm.Append(' ');

      if (source.Minutes > 0)
      {
        var tm = new Units.Time(source.Minutes, Units.TimeUnit.Minute);
        sm = sm.Append(tm.ToUnitString(Units.TimeUnit.Minute, null, null, fullNames));
      }

      sm = sm.Append(' ');

      if (source.Seconds > 0)
      {
        var ts = new Units.Time(source.Seconds, Units.TimeUnit.Second);
        sm = sm.Append(ts.ToUnitString(Units.TimeUnit.Second, null, null, fullNames));
      }

      return sm.ToString();
    }
  }
}
