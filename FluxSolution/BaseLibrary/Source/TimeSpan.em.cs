namespace Flux
{
  public static partial class XtensionsTimeSpan
  {
    public static string ToStringOptimized(this System.TimeSpan source)
      => System.Text.RegularExpressions.Regex.Replace(source.ToString(), @"^(00.)(00\:)+0+(?=.)", string.Empty);

    //public static string ToStringDays(this System.TimeSpan source, string suffix)
    //  => source.Days > 0 ? $"{source.Days}{suffix}" : string.Empty;

    //public static string ToStringHours(this System.TimeSpan source, string suffix)
    //  => source.Hours > 0 ? $"{source.Hours}{suffix}" : string.Empty;

    //public static string ToStringMinutes(this System.TimeSpan source, string suffix)
    //  => source.Minutes > 0 ? $"{source.Minutes}{suffix}" : string.Empty;

    //public static string ToStringSeconds(this System.TimeSpan source, string suffix, bool includeMillisecond)
    //  => source.Seconds > 0 || source.Milliseconds > 0 ? $"{source.Seconds}{(includeMillisecond && source.Milliseconds > 0 ? $".{source.Milliseconds}" : string.Empty)}{suffix}" : string.Empty;

    public static string ToStringXml(this System.TimeSpan source)
    {
      var sb = new System.Text.StringBuilder(24);

      if (source.Days > 0)
      {
        sb.Append('P');
        sb.Append(source.Days);
        sb.Append('D');
      }

      if (source.Hours > 0 || source.Minutes > 0 || source.Seconds > 0 || source.Milliseconds > 0)
      {
        sb.Append('T');

        if (source.Hours > 0)
        {
          sb.Append(source.Hours);
          sb.Append('H');
        }

        if (source.Minutes > 0)
        {
          sb.Append(source.Minutes);
          sb.Append('M');
        }

        if (source.Seconds > 0 || source.Milliseconds > 0)
        {
          sb.Append(source.Seconds);

          if (source.Milliseconds > 0)
          {
            sb.Append('.');
            sb.Append(source.Milliseconds);
          }

          sb.Append('S');
        }
      }

      return sb.ToString();
    }
  }
}
