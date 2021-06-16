namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string ToStringOptimized(this System.TimeSpan source)
      => System.Text.RegularExpressions.Regex.Replace(source.ToString(), @"^(00.)(00\:)+0+(?=.)", string.Empty);
  }
}
