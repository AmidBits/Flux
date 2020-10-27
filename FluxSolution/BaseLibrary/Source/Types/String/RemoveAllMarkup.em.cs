namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Remove all markup (i.e. tags, like in HTML, XML, etc.) from the string. Uses regular expressions.</summary>
    public static string RemoveAllMarkup(this string source, string replacement)
      => System.Text.RegularExpressions.Regex.Replace(source, @"(<[^>]+>)+", replacement);
  }
}
