namespace Flux.Text
{
  // https://en.wikipedia.org/wiki/Lexical_analysis

  public sealed class MathTokenUnrecognized
    : MathToken
  {
    public const string Regex = @"^.";

    public MathTokenUnrecognized(string name, string text, int index) : base(name, text, index) { }
  }
}
