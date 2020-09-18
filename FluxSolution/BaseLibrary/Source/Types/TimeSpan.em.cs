namespace Flux
{
  public static partial class XtendTimeSpan
  {
    public static string ToStringOptimized(this System.TimeSpan source)
      => System.Text.RegularExpressions.Regex.Replace(source.ToString(), @"^(00.)(00\:)+0+(?=.)", string.Empty);

    public static string ToStringDays(this System.TimeSpan source)
      => source.TotalDays > 0 ? $"{source.TotalDays} day{(source.TotalDays > 1 ? "s" : string.Empty)}" : string.Empty;
    public static string ToStringOfHours(this System.TimeSpan source)
      => source.TotalHours > 0 ? $"{source.TotalHours} hour{(source.TotalHours > 1 ? "s" : string.Empty)}" : string.Empty;
    public static string ToStringOfMinutes(this System.TimeSpan source)
      => source.TotalMinutes > 0 ? $"{source.TotalMinutes} min{(source.TotalMinutes > 1 ? "s" : string.Empty)}" : string.Empty;
    public static string ToStringOfSeconds(this System.TimeSpan source)
      => source.TotalSeconds > 0 ? $"{source.TotalSeconds} sec{(source.TotalSeconds > 1 ? "s" : string.Empty)}" : string.Empty;
    public static string ToStringDaysHoursMinsSecs(this System.TimeSpan source)
      => string.Join(' ', new string[] {
        source.Days > 0 ? $"{source.Days} day{(source.Days > 1 ? "s" : string.Empty)}" : string.Empty,
        source.Hours > 0 ? $"{source.Hours} hour{(source.Hours > 1 ? "s" : string.Empty)}" : string.Empty,
        source.Minutes > 0 ? $"{source.Minutes} min{(source.Minutes > 1 ? "s" : string.Empty)}" : string.Empty,
        source.Seconds > 0 ? $"{source.Seconds} sec{(source.Seconds > 1 ? "s" : string.Empty)}" : string.Empty
      });

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
