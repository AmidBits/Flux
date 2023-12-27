namespace Flux
{
  public static partial class Reflection
  {
    public static System.Text.StringBuilder ToXsdString(this System.TimeSpan source)
    {
      var sb = new System.Text.StringBuilder(24);

      if (source.Ticks < 0)
        sb.Append('-');

      sb.Append('P');

      if (System.Math.Abs(source.Days) is var days && days > 0)
        sb.Append(days).Append('D');

      if (System.Math.Abs(source.Hours) is var h && System.Math.Abs(source.Minutes) is var m && System.Math.Abs(source.Seconds) is var s && System.Math.Abs(source.Milliseconds) is var ms && (h > 0 || m > 0 || s > 0 || ms > 0))
      {
        sb.Append('T');

        if (h > 0)
          sb.Append(h).Append('H');

        if (m > 0)
          sb.Append(m).Append('M');

        if (s > 0 || ms > 0)
        {
          sb.Append(s);

          if (ms > 0)
            sb.Append('.').Append(ms);

          sb.Append('S');
        }
      }

      return sb;
    }

    public static System.Text.StringBuilder ToXsdBasicString(this System.TimeSpan source)
      => new System.Text.StringBuilder(source.Ticks < 0 ? "-" : string.Empty).Append("P000000").AppendFormat("{0:D2}", System.Math.Abs(source.Days)).Append('T').AppendFormat("{0:D2}", System.Math.Abs(source.Hours)).AppendFormat("{0:D2}", System.Math.Abs(source.Minutes)).AppendFormat("{0:D2}", System.Math.Abs(source.Seconds));
    public static System.Text.StringBuilder ToXsdBasicExtendedString(this System.TimeSpan source)
      => new System.Text.StringBuilder(source.Ticks < 0 ? "-" : string.Empty).Append("P0000-00-").AppendFormat("{0:D2}", System.Math.Abs(source.Days)).Append('T').AppendFormat("{0:D2}", System.Math.Abs(source.Hours)).AppendFormat("{0:D2}", System.Math.Abs(source.Minutes)).AppendFormat("{0:D2}", System.Math.Abs(source.Seconds));
  }
}
