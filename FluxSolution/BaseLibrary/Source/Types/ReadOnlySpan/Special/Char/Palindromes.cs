namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
#if NET7_0_OR_GREATER
    [System.Text.RegularExpressions.GeneratedRegex(@"^(?'letter'\p{L})+\p{L}?(?:\k'letter'(?'-letter'))+(?(letter)(?!))$")]
    public static partial System.Text.RegularExpressions.Regex PalindromeRegex();
#else
    private static System.Text.RegularExpressions.Regex PalindromeRegex() => new(@"^(?'letter'\p{L})+\p{L}?(?:\k'letter'(?'-letter'))+(?(letter)(?!))$");
#endif

    /// <summary>Matches palindromes of any length.</summary>
    /// <see cref="https://www.regular-expressions.info/balancing.html"/>
    public static bool IsPalindrome(this System.ReadOnlySpan<char> text)
      => PalindromeRegex().IsMatch(text.ToString());
  }
}
