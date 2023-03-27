/// <summary>A grapheme is a text element in dot NET.</summary>
namespace Flux.Text
{
  /// <summary>An implementation of a tokenization engine to demarcate and classify sections of an input string.</summary>
  public sealed class TextElementTokenizer
    : ITokenizer<IToken<TextElement>>
  {
    public System.Collections.Generic.List<IToken<TextElement>> GetTokens(string expression)
    {
      var list = new System.Collections.Generic.List<IToken<TextElement>>();

      using var sr = new System.IO.StringReader(expression);
      using var tee = new TextElementEnumerator(sr);

      var punctuationBracketDepth = 0;
      var punctuationBracketGroup = 0;
      var punctuationBracketGroups = new System.Collections.Generic.Stack<int>();

      var punctuationQuotationDepth = 0;
      var punctuationQuotationGroup = 0;
      var punctuationQuotationGroups = new System.Collections.Generic.Stack<int>();

      var index = 0;

      foreach (var te in tee)
      {
        if (te.AsReadOnlyListChar.Count == 1 && te.ToReadOnlyListRune().Count == 1)
        {
          switch (System.Text.Rune.GetUnicodeCategory(te.ToReadOnlyListRune()[0]))
          {
            case System.Globalization.UnicodeCategory.OpenPunctuation:
              punctuationBracketGroups.Push(++punctuationBracketGroup);
              list.Add(new TextElementTokenRange(index, te, ++punctuationBracketDepth, punctuationBracketGroups.Peek()));
              break;
            case System.Globalization.UnicodeCategory.InitialQuotePunctuation:
              punctuationQuotationGroups.Push(++punctuationQuotationGroup);
              list.Add(new TextElementTokenRange(index, te, ++punctuationQuotationDepth, punctuationQuotationGroups.Peek()));
              break;
            case System.Globalization.UnicodeCategory.ClosePunctuation:
              list.Add(new TextElementTokenRange(index, te, punctuationBracketDepth--, punctuationBracketGroups.Count > 0 ? punctuationBracketGroups.Pop() : -1));
              break;
            case System.Globalization.UnicodeCategory.FinalQuotePunctuation:
              list.Add(new TextElementTokenRange(index, te, punctuationQuotationDepth--, punctuationQuotationGroups.Count > 0 ? punctuationQuotationGroups.Pop() : -1));
              break;
            default:
              list.Add(new TextElementToken(index, te));
              break;
          }
        }
        else
          list.Add(new TextElementToken(index, te));

        index += te.AsReadOnlyListChar.Count;
      }

      return list;
    }
  }
}
