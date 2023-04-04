namespace Flux.Text
{
  // https://en.wikipedia.org/wiki/Lexical_analysis

  public sealed class MathTokenParenthesis
    : MathToken
  {
    public const string RegexWithComma = @"^[\(\,\)]";
    public const string RegexWithoutComma = @"^[\(\)]";

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

    public override string ToTokenString() => $"{base.ToString()},{nameof(Depth)}=\"{Depth}\",{nameof(Group)}=\"{Group}\"";

    public override string ToString() => ToString(null, null);
  }
}
