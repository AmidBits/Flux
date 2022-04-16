namespace Flux
{
  public static partial class TimeSpanEm
  {
    public static string ToOptimizedString(this System.TimeSpan source)
      => System.Text.RegularExpressions.Regex.Replace(source.ToString(), @"^(00.)(00\:)+0+(?=.)", string.Empty);
  }
}
