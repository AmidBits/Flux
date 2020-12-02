namespace Flux
{
  public static partial class StringEm
  {
    /// <summary>Matches palindromes of any length.</summary>
    /// <see cref="https://www.regular-expressions.info/balancing.html"/>
    public static bool IsPalindrome(this string text)
      => System.Text.RegularExpressions.Regex.IsMatch(text, @"^(?'letter'\p{L})+\p{L}?(?:\k'letter'(?'-letter'))+(?(letter)(?!))$");
  }
}
