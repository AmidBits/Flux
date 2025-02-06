namespace Flux
{
  public static partial class Fx
  {
    [System.Text.RegularExpressions.GeneratedRegex(@"(<[^>]+>)+")]
    public static partial System.Text.RegularExpressions.Regex AllMarkupTagRegex();

    /// <summary>Remove all markup tags (i.e. like in HTML, XML, etc.) from the string. Uses regular expressions.</summary>
    public static string RemoveAllMarkupTags(this string source)
      => source.ReplaceAllMarkupTags(string.Empty);

    /// <summary>Replace all markup tags (i.e. like in HTML, XML, etc.) with a chosen string. Uses regular expressions.</summary>	
    public static string ReplaceAllMarkupTags(this string source, string replacement)
      => AllMarkupTagRegex().Replace(source, replacement);
  }
}
