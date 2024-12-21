namespace Flux.Text.Tokenization
{
  // https://en.wikipedia.org/wiki/Lexical_analysis

  public sealed partial class MathTokenParenthesis
    : MathToken
  {
    //public const string RegexWithComma = @"^[\(\,\)]";
    //public const string RegexWithoutComma = @"^[\(\)]";

    [System.Text.RegularExpressions.GeneratedRegex(@"^[\(\,\)]")]
    public static partial System.Text.RegularExpressions.Regex RegexWithComma();

    [System.Text.RegularExpressions.GeneratedRegex(@"^[\(\)]")]
    public static partial System.Text.RegularExpressions.Regex RegexWithoutComma();

    public const string SymbolComma = @",";
    public const string SymbolLeft = @"(";
    public const string SymbolRight = @")";

    public int Depth { get; set; }
    public int Group { get; set; }

    public MathTokenParenthesis(string name, string text, int index, int depth, int group)
      : base(name, text, index)
    {
      Depth = depth;
      Group = group;
    }
    public override string ToTokenString() => base.ToTokenString().Replace(" }", $", {nameof(Depth)}=\"{Depth}\", {nameof(Group)}=\"{Group}\" }}");
  }
}
