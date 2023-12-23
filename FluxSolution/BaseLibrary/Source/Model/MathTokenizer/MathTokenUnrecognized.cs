namespace Flux.Text
{
  // https://en.wikipedia.org/wiki/Lexical_analysis

  public sealed partial class MathTokenUnrecognized
    : MathToken
  {
    //public const string Regex = @"^.";

    [System.Text.RegularExpressions.GeneratedRegex(@"^.")]
    public static partial System.Text.RegularExpressions.Regex Regex();

    public MathTokenUnrecognized(string name, string text, int index) : base(name, text, index) { }
  }
}
