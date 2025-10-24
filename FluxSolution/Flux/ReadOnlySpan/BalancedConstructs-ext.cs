namespace Flux
{
  public static partial class XtensionReadOnlySpan
  {
    public static string CreateBalancedExpression(System.ReadOnlySpan<char> reOpen, System.ReadOnlySpan<char> reMatch, System.ReadOnlySpan<char> reClose)
    => $"^{reMatch}*(?>(?>(?'balance'{reOpen}){reMatch}*)+(?>(?'-balance'{reClose}){reMatch}*)+)+(?(balance)(?!))$";

    extension(System.ReadOnlySpan<char> source)
    {
      /// <summary>Checks whether the string has balanced pairs (e.g. parenthesis).</summary>
      public bool IsBalanced(char open, char close)
        => IsBalancedConstruct(source, $"\\{open}", $"[^\\{open}\\{close}]", $"\\{close}");

      /// <summary>Checks whether the string is a matching balanced construct.</summary>
      /// <see href="https://www.regular-expressions.info/balancing.html"/>
      /// <example>See below for balanced parenthesis.</example>
      public bool IsBalancedConstruct(System.ReadOnlySpan<char> open, System.ReadOnlySpan<char> match, System.ReadOnlySpan<char> close)
        => System.Text.RegularExpressions.Regex.IsMatch(source, CreateBalancedExpression(open, match, close));
    }
  }
}
