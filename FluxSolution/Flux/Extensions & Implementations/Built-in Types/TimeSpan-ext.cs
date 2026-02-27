namespace Flux
{
  public static partial class TimeSpanExtensions
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
        var sb = new System.Text.StringBuilder();

        if (source.Days > 0)
        {
          var td = new Units.Time(source.Days, Units.TimeUnit.Day);
          sb.Append(td.ToUnitString(Units.TimeUnit.Day, null, null, spacing, fullNames));
        }

        sb.Append(' ');

        if (source.Hours > 0)
        {
          var th = new Units.Time(source.Hours, Units.TimeUnit.Hour);
          sb.Append(th.ToUnitString(Units.TimeUnit.Hour, null, null, spacing, fullNames));
        }

        sb.Append(' ');

        if (source.Minutes > 0)
        {
          var tm = new Units.Time(source.Minutes, Units.TimeUnit.Minute);
          sb.Append(tm.ToUnitString(Units.TimeUnit.Minute, null, null, spacing, fullNames));
        }

        sb.Append(' ');

        if (source.Seconds > 0)
        {
          var ts = new Units.Time(source.Seconds, Units.TimeUnit.Second);
          sb.Append(ts.ToUnitString(Units.TimeUnit.Second, null, null, spacing, fullNames));
        }

        return sb.ToString();
      }

      #endregion

      #region ToXsdDurationString

      public string ToXsdDurationString()
      {
        var sb = new System.Text.StringBuilder();

        if (source.Ticks < 0)
          sb.Append('-');

        sb.Append('P');

        if (int.Abs(source.Days) is var days && days > 0)
          sb.Append(days).Append('D');

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
          sb.Append('T');

          if (h != 0)
            sb.Append(h).Append('H');

          if (m != 0)
            sb.Append(m).Append('M');

          if (s != 0 || milli != 0 || micro != 0 || nano != 0)
          {
            sb.Append(s);

            if (milli != 0 || micro != 0 || nano != 0)
            {
              sb.Append('.');

              if (milli != 0)
                sb.Append(milli.ToString("D3"));
              if (micro != 0)
                sb.Append(micro.ToString("D3"));
              if (nano != 0)
                sb.Append(nano.ToString("D3"));
            }

            sb.Append('S');
          }
        }

        return sb.ToString();
      }

      #endregion
    }
  }
}
