namespace Flux
{
  public static partial class ExtensionMethodsTimeSpan
  {
    public static string ToOptimizedString(this System.TimeSpan source)
      => System.Text.RegularExpressions.Regex.Replace(source.ToString(), @"^(00.)(00\:)+0+(?=.)", string.Empty);
  }
}
