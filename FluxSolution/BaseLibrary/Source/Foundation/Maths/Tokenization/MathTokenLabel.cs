namespace Flux.Text
{
  // https://en.wikipedia.org/wiki/Lexical_analysis

  public sealed class MathTokenLabel
    : MathToken
  {
    public const string Regex = @"^[\p{L}][\p{L}\p{Nd}_]*";

    public MathTokenLabel(string name, string text, int index)
      : base(name, text, index)
    { }
  }
}
