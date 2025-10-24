namespace Flux
{
  public static partial class XtensionTimeSpan
  {
    extension(System.TimeSpan source)
    {
      #region ToOptimizedString

      //    [System.Text.RegularExpressions.GeneratedRegex(@"^(00.)(00\:)+0+(?=.)")]
      //    private static partial System.Text.RegularExpressions.Regex RegexTimeSpanOptimizedString();
      //
      //    public string ToOptimizedString()
      //      => RegexTimeSpanOptimizedString().Replace(source.ToString(), string.Empty);

      #endregion

      #region ToStringOfUnits

      public string ToStringOfUnits(UnicodeSpacing spacing, bool fullNames)
      {
        var sm = new SpanMaker<char>();

        if (source.Days > 0)
        {
          var td = new Units.Time(source.Days, Units.TimeUnit.Day);
          sm = sm.Append(td.ToUnitString(Units.TimeUnit.Day, null, null, spacing, fullNames));
        }

        sm = sm.Append(' ');

        if (source.Hours > 0)
        {
          var th = new Units.Time(source.Hours, Units.TimeUnit.Hour);
          sm = sm.Append(th.ToUnitString(Units.TimeUnit.Hour, null, null, spacing, fullNames));
        }

        sm = sm.Append(' ');

        if (source.Minutes > 0)
        {
          var tm = new Units.Time(source.Minutes, Units.TimeUnit.Minute);
          sm = sm.Append(tm.ToUnitString(Units.TimeUnit.Minute, null, null, spacing, fullNames));
        }

        sm = sm.Append(' ');

        if (source.Seconds > 0)
        {
          var ts = new Units.Time(source.Seconds, Units.TimeUnit.Second);
          sm = sm.Append(ts.ToUnitString(Units.TimeUnit.Second, null, null, spacing, fullNames));
        }

        return sm.ToString();
      }

      #endregion

      #region ToXsdDurationString

      public string ToXsdDurationString()
      {
        var sm = new SpanMaker<char>();

        if (source.Ticks < 0)
          sm = sm.Append('-');

        sm = sm.Append('P');

        if (int.Abs(source.Days) is var days && days > 0)
          sm = sm.Append(days).Append('D');

        if (
          int.Abs(source.Hours) is var h
          && int.Abs(source.Minutes) is var m
          && int.Abs(source.Seconds) is var s
          && int.Abs(source.Milliseconds) is var milli
          && int.Abs(source.Microseconds) is var micro
          && int.Abs(source.Nanoseconds) is var nano
          && (h != 0 || m != 0 || s != 0 || milli != 0 || micro != 0 || nano != 0)
        )
        {
          sm = sm.Append('T');

          if (h != 0)
            sm = sm.Append(h).Append('H');

          if (m != 0)
            sm = sm.Append(m).Append('M');

          if (s != 0 || milli != 0 || micro != 0 || nano != 0)
          {
            sm = sm.Append(s);

            if (milli != 0 || micro != 0 || nano != 0)
            {
              sm = sm.Append('.');

              if (milli != 0)
                sm = sm.Append(milli.ToString("D3"));
              if (micro != 0)
                sm = sm.Append(micro.ToString("D3"));
              if (nano != 0)
                sm = sm.Append(nano.ToString("D3"));
            }

            sm = sm.Append('S');
          }
        }

        return sm.ToString();
      }

      #endregion
    }
  }
}
