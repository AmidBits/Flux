using System.Security.Cryptography.X509Certificates;
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

    public Token(int index, System.Text.Rune value, int categoryOrdinal)
    {
      Index = index;
      Value = value;

      CategoryOrdinal = categoryOrdinal;
    }

    public override string ToString()
      => $"<{UnicodeCategory}({CategoryOrdinal})=\"{Value}\" @{Index} +{Value.Utf16SequenceLength}>";
  }

  public class TokenLetter
  : Token
  {
    public int LetterIndex { get; set; }

    public TokenLetter(int index, System.Text.Rune value, int categoryOrdinal)
      : base(index, value, categoryOrdinal)
    {
    }
  }

  public class TokenMark
    : Token
  {
    public TokenMark(int index, System.Text.Rune value, int categoryOrdinal)
      : base(index, value, categoryOrdinal)
    {
    }
  }

  public class TokenNumber
    : Token
  {
    public TokenNumber(int index, System.Text.Rune value, int categoryOrdinal)
      : base(index, value, categoryOrdinal)
    {
    }
  }

  public class TokenOther
    : Token
  {
    public TokenOther(int index, System.Text.Rune value, int categoryOrdinal)
      : base(index, value, categoryOrdinal)
    {
    }
  }

  public class TokenPunctuation
    : Token
  {
    public int BracketDepth { get; set; }
    public int BracketGroup { get; set; }

    public int QuotationDepth { get; set; }
    public int QuotationGroup { get; set; }

    public TokenPunctuation(int index, System.Text.Rune value, int categoryOrdinal, int depth, int group, int quotationDepth, int quotationGroup)
      : base(index, value, categoryOrdinal)
    {
      BracketDepth = depth;
      BracketGroup = group;

      QuotationDepth = quotationDepth;
      QuotationGroup = quotationGroup;
    }

    public override string ToString()
    {
      var sb = new System.Text.StringBuilder();

      if (BracketDepth > 0 || BracketGroup > 0)
        sb.Append($"[BD={BracketDepth}, BG={BracketGroup}]");

      if (QuotationDepth > 0 || QuotationGroup > 0)
        sb.Append($"[QD={QuotationDepth}, QG={QuotationGroup}]");

      sb.Append('>');

      return base.ToString().Replace(@">", sb.ToString(), System.StringComparison.Ordinal);
    }
  }

  public class TokenSeparator
    : Token
  {
    public TokenSeparator(int index, System.Text.Rune value, int categoryOrdinal)
      : base(index, value, categoryOrdinal)
    {
    }
  }

  public class TokenSymbol
    : Token
  {
    public TokenSymbol(int index, System.Text.Rune value, int categoryOrdinal)
      : base(index, value, categoryOrdinal)
    {
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

      if (Normalize) expression = expression.Normalize() ?? throw new System.NullReferenceException(nameof(expression.Normalize));

      var ordinalLetter = 0;
      var ordinalMark = 0;
      var ordinalNumber = 0;
      var ordinalPunctuation = 0;
      var ordinalSeparator = 0;
      var ordinalSymbol = 0;
      var ordinalOther = 0;
      var ordinalToken = 0;

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

        switch (unicodeCategory.ToCategoryMajor())
        {
          case Flux.Unicode.CategoryMajor.Letter:
            ordinalLetter++;
            yield return new TokenLetter(index, rune, ordinalLetter);
            break;
          case Flux.Unicode.CategoryMajor.Mark:
            ordinalMark++;
            yield return new TokenMark(index, rune, ordinalMark);
            break;
          case Flux.Unicode.CategoryMajor.Number:
            ordinalNumber++;
            yield return new TokenNumber(index, rune, ordinalNumber);
            break;
          case Flux.Unicode.CategoryMajor.Other:
            ordinalOther++;
            yield return new TokenOther(index, rune, ordinalOther);
            break;
          case Flux.Unicode.CategoryMajor.Punctuation when unicodeCategory == System.Globalization.UnicodeCategory.OpenPunctuation:
            ordinalPunctuation++;
            punctuationBracketGroups.Push(++punctuationBracketGroup);
            yield return new TokenPunctuation(index, rune, ordinalPunctuation, ++punctuationBracketDepth, punctuationBracketGroups.Peek(), punctuationQuotationDepth, punctuationQuotationGroup);
            break;
          case Flux.Unicode.CategoryMajor.Punctuation when unicodeCategory == System.Globalization.UnicodeCategory.InitialQuotePunctuation:
            ordinalPunctuation++;
            punctuationQuotationGroups.Push(++punctuationQuotationGroup);
            yield return new TokenPunctuation(index, rune, ordinalPunctuation, punctuationBracketDepth, punctuationBracketGroup, ++punctuationQuotationDepth, punctuationQuotationGroups.Peek());
            break;
          case Flux.Unicode.CategoryMajor.Punctuation when unicodeCategory == System.Globalization.UnicodeCategory.ClosePunctuation:
            ordinalPunctuation++;
            yield return new TokenPunctuation(index, rune, ordinalPunctuation, punctuationBracketDepth--, punctuationBracketGroups.Count > 0 ? punctuationBracketGroups.Pop() : -1, punctuationQuotationDepth, punctuationQuotationGroup);
            break;
          case Flux.Unicode.CategoryMajor.Punctuation when unicodeCategory == System.Globalization.UnicodeCategory.FinalQuotePunctuation:
            ordinalPunctuation++;
            yield return new TokenPunctuation(index, rune, ordinalPunctuation, punctuationBracketDepth, punctuationBracketGroup, punctuationQuotationDepth--, punctuationQuotationGroups.Count > 0 ? punctuationQuotationGroups.Pop() : -1);
            break;
          case Flux.Unicode.CategoryMajor.Punctuation:
            ordinalPunctuation++;
            yield return new TokenPunctuation(index, rune, ordinalPunctuation, punctuationBracketDepth, punctuationBracketGroups.Count > 0 ? punctuationBracketGroups.Peek() : 0, punctuationQuotationDepth, punctuationQuotationGroups.Count > 0 ? punctuationQuotationGroups.Peek() : 0);
            break;
          case Flux.Unicode.CategoryMajor.Separator:
            ordinalSeparator++;
            yield return new TokenSeparator(index, rune, ordinalSeparator);
            break;
          case Flux.Unicode.CategoryMajor.Symbol:
            ordinalSymbol++;
            yield return new TokenSymbol(index, rune, ordinalSymbol);
            break;
          default:
            ordinalToken++;
            yield return new Token(index, rune, ordinalToken);
            break;
        }

        index += rune.Utf16SequenceLength;
      }
    }
  }
}
