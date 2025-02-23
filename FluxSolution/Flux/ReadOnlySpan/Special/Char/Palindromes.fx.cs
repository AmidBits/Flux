namespace Flux
{
  public static partial class Fx
  {
    [System.Text.RegularExpressions.GeneratedRegex(@"^(?'letter'\p{L})+\p{L}?(?:\k'letter'(?'-letter'))+(?(letter)(?!))$")]
    public static partial System.Text.RegularExpressions.Regex RegexPalindrome();

    /// <summary>Matches palindromes of any length.</summary>
    /// <see href="https://www.regular-expressions.info/balancing.html"/>
    public static bool IsPalindrome(this System.ReadOnlySpan<char> text)
      => RegexPalindrome().IsMatch(text);
  }
}
