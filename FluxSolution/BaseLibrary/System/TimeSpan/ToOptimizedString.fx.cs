namespace Flux
{
  public static partial class Fx
  {
    [System.Text.RegularExpressions.GeneratedRegex(@"^(00.)(00\:)+0+(?=.)")]
    private static partial System.Text.RegularExpressions.Regex RegexTimeSpanOptimizedString();

    public static string ToOptimizedString(this System.TimeSpan source)
      => RegexTimeSpanOptimizedString().Replace(source.ToString(), string.Empty);
  }
}
