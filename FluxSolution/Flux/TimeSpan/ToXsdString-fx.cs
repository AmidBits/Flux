namespace Flux
{
  public static partial class TimeSpans
  {
    public static string ToXsdDurationString(this System.TimeSpan source)
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
  }
}
