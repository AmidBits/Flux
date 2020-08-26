/// <summary>A unicode category character.</summary>
namespace Flux.Text.Tokenization.UnicodeCategory
{
  /// <summary>An implementation of a demarcated and classified section of a unicode category.</summary>
  public class Token
    : IToken<char>
  {
    public int Index { get; }
    public char Value { get; }

    public System.Globalization.UnicodeCategory UnicodeCategory
      => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(Value);

    public Token(int index, char value)
    {
      Index = index;
      Value = value;
    }

    public override string ToString()
      => $"<{UnicodeCategory}=\"{Value}\" @{Index}>";
  }

  public class TokenLetter
    : Token
  {
    public TokenLetter(int index, char text)
      : base(index, text)
    {
    }
  }

  public class TokenMark
    : Token
  {
    public TokenMark(int index, char text)
      : base(index, text)
    {
    }
  }

  public class TokenNumber
    : Token
  {
    public TokenNumber(int index, char text)
      : base(index, text)
    {
    }
  }

  public class TokenOther
    : Token
  {
    public TokenOther(int index, char text)
      : base(index, text)
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

    public TokenPunctuation(int index, char text, int depth, int group, int quotationDepth, int quotationGroup)
      : base(index, text)
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
    public TokenSeparator(int index, char text)
      : base(index, text)
    {
    }
  }

  public class TokenSymbol
    : Token
  {
    public TokenSymbol(int index, char text)
      : base(index, text)
    {
    }
  }

  /// <summary>An implementation of a tokenization engine to demarcate and classify sections of an input string.</summary>
  public class Tokenizer
    : ITokenizer<Token>
  {
    public System.Collections.Generic.IEnumerable<Token> GetTokens(string expression)
    {
      if (expression is null) throw new System.ArgumentNullException(nameof(expression));

      var punctuationBracketDepth = 0;
      var punctuationBracketGroup = 0;
      var punctuationBracketGroups = new System.Collections.Generic.Stack<int>();

      var punctuationQuotationDepth = 0;
      var punctuationQuotationGroup = 0;
      var punctuationQuotationGroups = new System.Collections.Generic.Stack<int>();

      for (var index = 0; index < expression.Length; index++)
      {
        var character = expression[index];

        var unicodeCategory = char.GetUnicodeCategory(character);

        switch (unicodeCategory.ToCategoryMajor())
        {
          case Flux.Unicode.CategoryMajor.Letter:
            yield return new TokenLetter(index, character);
            break;
          case Flux.Unicode.CategoryMajor.Mark:
            yield return new TokenMark(index, character);
            break;
          case Flux.Unicode.CategoryMajor.Number:
            yield return new TokenNumber(index, character);
            break;
          case Flux.Unicode.CategoryMajor.Punctuation when unicodeCategory == System.Globalization.UnicodeCategory.OpenPunctuation:
            punctuationBracketGroups.Push(++punctuationBracketGroup);
            yield return new TokenPunctuation(index, character, ++punctuationBracketDepth, punctuationBracketGroups.Peek(), punctuationQuotationDepth, punctuationQuotationGroup);
            break;
          case Flux.Unicode.CategoryMajor.Punctuation when unicodeCategory == System.Globalization.UnicodeCategory.InitialQuotePunctuation:
            punctuationQuotationGroups.Push(++punctuationQuotationGroup);
            yield return new TokenPunctuation(index, character, punctuationBracketDepth, punctuationBracketGroup, ++punctuationQuotationDepth, punctuationQuotationGroups.Peek());
            break;
          case Flux.Unicode.CategoryMajor.Punctuation when unicodeCategory == System.Globalization.UnicodeCategory.ClosePunctuation:
            yield return new TokenPunctuation(index, character, punctuationBracketDepth--, punctuationBracketGroups.Count > 0 ? punctuationBracketGroups.Pop() : -1, punctuationQuotationDepth, punctuationQuotationGroup);
            break;
          case Flux.Unicode.CategoryMajor.Punctuation when unicodeCategory == System.Globalization.UnicodeCategory.FinalQuotePunctuation:
            yield return new TokenPunctuation(index, character, punctuationBracketDepth, punctuationBracketGroup, punctuationQuotationDepth--, punctuationQuotationGroups.Count > 0 ? punctuationQuotationGroups.Pop() : -1);
            break;
          case Flux.Unicode.CategoryMajor.Punctuation:
            yield return new TokenPunctuation(index, character, punctuationBracketDepth, punctuationBracketGroups.Count > 0 ? punctuationBracketGroups.Peek() : 0, punctuationQuotationDepth, punctuationQuotationGroups.Count > 0 ? punctuationQuotationGroups.Peek() : 0);
            break;
          case Flux.Unicode.CategoryMajor.Separator:
            yield return new TokenSeparator(index, character);
            break;
          case Flux.Unicode.CategoryMajor.Symbol:
            yield return new TokenSymbol(index, character);
            break;
          case Flux.Unicode.CategoryMajor.Other:
            yield return new TokenOther(index, character);
            break;
          default:
            throw new System.Exception(new Token(index, character).ToString());
        }
      }
    }
  }
}
