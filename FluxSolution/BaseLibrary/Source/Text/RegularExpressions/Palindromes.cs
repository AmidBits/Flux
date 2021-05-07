namespace Flux
{
  public static partial class Regex
  {
    public const string Palindrome = @"^(?'letter'\p{L})+\p{L}?(?:\k'letter'(?'-letter'))+(?(letter)(?!))$";

    /// <summary>Matches palindromes of any length.</summary>
    /// <see cref="https://www.regular-expressions.info/balancing.html"/>
    public static bool IsPalindrome(this string text)
      => System.Text.RegularExpressions.Regex.IsMatch(text, Palindrome);
  }
}
