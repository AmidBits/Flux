using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethodsTimeSpan
  {
    public static string ToStringOf(this System.TimeSpan source)
      => string.Join(' ', new string[] { ToStringOfDays(source), ToStringOfHours(source), ToStringOfMinutes(source), ToStringOfSeconds(source) }.Where(s => s.Length > 0)).Trim();
    public static string ToStringOfDays(this System.TimeSpan source, string symbol = "d")
      => source.Days > 0 ? $"{source.Days}{symbol}" : string.Empty;
    public static string ToStringOfHours(this System.TimeSpan source, string symbol = "h")
      => source.Hours > 0 ? $"{source.Hours}{symbol}" : string.Empty;
    public static string ToStringOfMinutes(this System.TimeSpan source, string symbol = "min")
      => source.Minutes > 0 ? $"{source.Minutes}{symbol}" : string.Empty;
    public static string ToStringOfSeconds(this System.TimeSpan source, string symbol = "s")
      => source.Seconds > 0 ? $"{source.Seconds}{symbol}" : string.Empty;
  }
}
