namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    public static string CreateBalancedExpression(string reOpen, string reMatch, string reClose)
      => $"^{reMatch}*(?>(?>(?'balance'{reOpen}){reMatch}*)+(?>(?'-balance'{reClose}){reMatch}*)+)+(?(balance)(?!))$";

    /// <summary>Checks whether the string has balanced pairs (e.g. parenthesis).</summary>
    public static bool IsBalanced(this System.ReadOnlySpan<char> source, char open, char close)
      => IsBalancedConstruct(source, $"\\{open}", $"[^\\{open}\\{close}]", $"\\{close}");

    /// <summary>Checks whether the string is a matching balanced construct.</summary>
    /// <see cref="https://www.regular-expressions.info/balancing.html"/>
    /// <example>See below for balanced parenthesis.</example>
    public static bool IsBalancedConstruct(this System.ReadOnlySpan<char> source, string open, string match, string close)
      => System.Text.RegularExpressions.Regex.IsMatch(source, CreateBalancedExpression(open, match, close));
  }
}
