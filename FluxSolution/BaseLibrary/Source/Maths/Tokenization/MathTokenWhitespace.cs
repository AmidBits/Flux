namespace Flux.Text
{
  // https://en.wikipedia.org/wiki/Lexical_analysis

  public sealed class MathTokenWhitespace
    : MathToken
  {
    public const string Regex = @"^\s+";

    public MathTokenWhitespace(string name, string text, int index) : base(name, text, index) { }
  }
}
