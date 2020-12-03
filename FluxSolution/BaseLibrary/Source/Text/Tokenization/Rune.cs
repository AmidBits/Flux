/// <summary>A rune is a Unicode code point.</summary>
namespace Flux.Text.Tokenization.Rune
{
  /// <summary>An implementation of a demarcated and classified section of a rune.</summary>
  public class Token
    : IToken<System.Text.Rune>
  {
    public int Index { get; }
    public System.Text.Rune Value { get; }

    public Token(int index, System.Text.Rune rune)
    {
      Index = index;
      Value = rune;
    }

    public override string ToString()
      => $"<{System.Text.Rune.GetUnicodeCategory(Value)}=\"{Value}\"@{Index}+{Value.Utf16SequenceLength}>";
  }

  public class TokenRange
    : Token
  {
    public int? Depth { get; set; }
    public int? Group { get; set; }

    public TokenRange(int index, System.Text.Rune rune, int depth, int group)
      : base(index, rune)
    {
      Depth = depth;
      Group = group;
    }

    public override string ToString()
      => base.ToString() is var s && Depth.HasValue && Group.HasValue ? s.Insert(s.Length - 1, $"[Depth={Depth},Group={Group}]") : s;
  }

  /// <summary>An implementation of a tokenization engine to demarcate and classify sections of an input string.</summary>
  public class Tokenizer
    : ITokenizer<IToken<System.Text.Rune>>
  {
    public bool Normalize { get; } = true;

    public System.Collections.Generic.IEnumerable<IToken<System.Text.Rune>> GetTokens(string expression)
    {
      if (expression is null) throw new System.ArgumentNullException(nameof(expression));

      if (Normalize)
        expression = expression.Normalize();

      var punctuationBracketDepth = 0;
      var punctuationBracketGroup = 0;
      var punctuationBracketGroups = new System.Collections.Generic.Stack<int>();

      var punctuationQuotationDepth = 0;
      var punctuationQuotationGroup = 0;
      var punctuationQuotationGroups = new System.Collections.Generic.Stack<int>();

      var index = 0;

      foreach (var rune in expression.EnumerateRunes())
      {
        switch (System.Text.Rune.GetUnicodeCategory(rune))
        {
          case System.Globalization.UnicodeCategory.OpenPunctuation:
            punctuationBracketGroups.Push(++punctuationBracketGroup);
            yield return new TokenRange(index, rune, ++punctuationBracketDepth, punctuationBracketGroups.Peek());
            break;
          case System.Globalization.UnicodeCategory.InitialQuotePunctuation:
            punctuationQuotationGroups.Push(++punctuationQuotationGroup);
            yield return new TokenRange(index, rune, ++punctuationQuotationDepth, punctuationQuotationGroups.Peek());
            break;
          case System.Globalization.UnicodeCategory.ClosePunctuation:
            yield return new TokenRange(index, rune, punctuationBracketDepth--, punctuationBracketGroups.Count > 0 ? punctuationBracketGroups.Pop() : -1);
            break;
          case System.Globalization.UnicodeCategory.FinalQuotePunctuation:
            yield return new TokenRange(index, rune, punctuationQuotationDepth--, punctuationQuotationGroups.Count > 0 ? punctuationQuotationGroups.Pop() : -1);
            break;
          default:
            yield return new Token(index, rune);
            break;
        }

        index += rune.Utf16SequenceLength;
      }
    }
  }
}
