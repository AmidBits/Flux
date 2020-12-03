using System.Linq;

/// <summary>A rune is a Unicode code point.</summary>
namespace Flux.Text.Tokenization.Rune
{
  /// <summary>An implementation of a demarcated and classified section of a rune.</summary>
  public class Token
    : IToken<System.Text.Rune>
  {
    public int Index { get; }
    public System.Text.Rune Value { get; }

    public System.Globalization.UnicodeCategory UnicodeCategory
      => System.Text.Rune.GetUnicodeCategory(Value);

    public int CategoryOrdinal { get; }

    public Token(int index, System.Text.Rune rune, int categoryOrdinal)
    {
      Index = index;
      Value = rune;

      CategoryOrdinal = categoryOrdinal;
    }

    public override string ToString()
      => $"<{UnicodeCategory}#{CategoryOrdinal}=\"{Value}\"@{Index}+{Value.Utf16SequenceLength}>";
  }

  public class TokenRange
    : Token
  {
    public int? Depth { get; set; }
    public int? Group { get; set; }

    public TokenRange(int index, System.Text.Rune value, int categoryOrdinal)
      : base(index, value, categoryOrdinal)
    {
    }
    public TokenRange(int index, System.Text.Rune value, int categoryOrdinal, int depth, int group)
      : base(index, value, categoryOrdinal)
    {
      Depth = depth;
      Group = group;
    }

    public override string ToString()
    {
      var sb = new System.Text.StringBuilder(base.ToString());

      if (Depth.HasValue && Group.HasValue)
      {
        sb.Remove(sb.Length - 1, 1);

        switch (UnicodeCategory)
        {
          case System.Globalization.UnicodeCategory.ClosePunctuation:
          case System.Globalization.UnicodeCategory.OpenPunctuation:
          case System.Globalization.UnicodeCategory.FinalQuotePunctuation:
          case System.Globalization.UnicodeCategory.InitialQuotePunctuation:
            sb.Append($"[Depth={Depth},Group={Group}]");
            break;
        }

        sb.Append('>');
      }

      return sb.ToString();
    }
  }

  /// <summary>An implementation of a tokenization engine to demarcate and classify sections of an input string.</summary>
  public class Tokenizer
    : ITokenizer<Token>
  {
    public bool Normalize { get; } = true;

    public System.Collections.Generic.IEnumerable<Token> GetTokens(string expression)
    {
      if (expression is null) throw new System.ArgumentNullException(nameof(expression));

      if (Normalize) expression = expression.Normalize();

      var unicodeCategoryCounts = ((System.Globalization.UnicodeCategory[])System.Enum.GetValues(typeof(System.Globalization.UnicodeCategory))).ToDictionary(uc => uc, uc => 0);

      var punctuationBracketDepth = 0;
      var punctuationBracketGroup = 0;
      var punctuationBracketGroups = new System.Collections.Generic.Stack<int>();

      var punctuationQuotationDepth = 0;
      var punctuationQuotationGroup = 0;
      var punctuationQuotationGroups = new System.Collections.Generic.Stack<int>();

      var index = 0;

      foreach (var rune in expression.EnumerateRunes())
      {
        var unicodeCategory = System.Text.Rune.GetUnicodeCategory(rune);

        unicodeCategoryCounts[unicodeCategory]++;

        switch (unicodeCategory.ToMajorCode())
        {
           case UnicodeCategoryMajorCode.Punctuation when unicodeCategory == System.Globalization.UnicodeCategory.OpenPunctuation:
            punctuationBracketGroups.Push(++punctuationBracketGroup);
            yield return new TokenRange(index, rune, unicodeCategoryCounts[unicodeCategory], ++punctuationBracketDepth, punctuationBracketGroups.Peek());
            break;
          case UnicodeCategoryMajorCode.Punctuation when unicodeCategory == System.Globalization.UnicodeCategory.InitialQuotePunctuation:
            punctuationQuotationGroups.Push(++punctuationQuotationGroup);
            yield return new TokenRange(index, rune, unicodeCategoryCounts[unicodeCategory], ++punctuationQuotationDepth, punctuationQuotationGroups.Peek());
            break;
          case UnicodeCategoryMajorCode.Punctuation when unicodeCategory == System.Globalization.UnicodeCategory.ClosePunctuation:
            yield return new TokenRange(index, rune, unicodeCategoryCounts[unicodeCategory], punctuationBracketDepth--, punctuationBracketGroups.Count > 0 ? punctuationBracketGroups.Pop() : -1);
            break;
          case UnicodeCategoryMajorCode.Punctuation when unicodeCategory == System.Globalization.UnicodeCategory.FinalQuotePunctuation:
            yield return new TokenRange(index, rune, unicodeCategoryCounts[unicodeCategory], punctuationQuotationDepth--, punctuationQuotationGroups.Count > 0 ? punctuationQuotationGroups.Pop() : -1);
            break;
          default:
            yield return new Token(index, rune, unicodeCategoryCounts[unicodeCategory]);
            break;
        }

        index += rune.Utf16SequenceLength;
      }
    }
  }
}
