namespace Flux
{
  public static partial class Fx
  {
    public static string ToXsdDurationString(this System.TimeSpan source)
    {
      var sb = new System.Text.StringBuilder();

      if (source.Ticks < 0)
        sb.Append('-');

      sb.Append('P');

      if (System.Math.Abs(source.Days) is var days && days > 0)
        sb.Append(days).Append('D');

      if (
        System.Math.Abs(source.Hours) is var h
        && System.Math.Abs(source.Minutes) is var m
        && System.Math.Abs(source.Seconds) is var s
        && System.Math.Abs(source.Milliseconds) is var milli
        && System.Math.Abs(source.Microseconds) is var micro
        && System.Math.Abs(source.Nanoseconds) is var nano
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
  }
}
