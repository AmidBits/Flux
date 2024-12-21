namespace Flux.Text.Tokenization
{
  // https://en.wikipedia.org/wiki/Lexical_analysis

  public sealed partial class MathTokenWhitespace
    : MathToken
  {
    //public const string Regex = @"^\s+";

    [System.Text.RegularExpressions.GeneratedRegex(@"^\s+")]
    public static partial System.Text.RegularExpressions.Regex Regex();

    public MathTokenWhitespace(string name, string text, int index) : base(name, text, index) { }
  }
}
