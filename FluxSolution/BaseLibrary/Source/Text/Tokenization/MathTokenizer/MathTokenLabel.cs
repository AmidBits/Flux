namespace Flux.Text.Tokenization
{
  // https://en.wikipedia.org/wiki/Lexical_analysis

  public sealed partial class MathTokenLabel
    : MathToken
  {
    //public const string Regex = @"^[\p{L}][\p{L}\p{Nd}_]*";

    [System.Text.RegularExpressions.GeneratedRegex(@"^[\p{L}][\p{L}\p{Nd}_]*")]
    public static partial System.Text.RegularExpressions.Regex Regex();

    public MathTokenLabel(string name, string text, int index) : base(name, text, index) { }
  }
}
