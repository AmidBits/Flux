/// <summary>A grapheme is a text element in dot NET.</summary>
namespace Flux
{
  /// <summary>An implementation of a tokenization engine to demarcate and classify sections of an input string.</summary>
  public sealed class TextElementTokenizer
    : ITokenizer<IToken<TextElementCluster>>
  {
    public System.Collections.Generic.IEnumerable<IToken<TextElementCluster>> GetTokens(string expression)
    {
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
        if (te.Chars.Length == 1 && te.Runes.Count == 1)
        {
          switch (System.Text.Rune.GetUnicodeCategory(te.Runes[0]))
          {
            case System.Globalization.UnicodeCategory.OpenPunctuation:
              punctuationBracketGroups.Push(++punctuationBracketGroup);
              yield return new TextElementTokenRange(index, te, ++punctuationBracketDepth, punctuationBracketGroups.Peek());
              break;
            case System.Globalization.UnicodeCategory.InitialQuotePunctuation:
              punctuationQuotationGroups.Push(++punctuationQuotationGroup);
              yield return new TextElementTokenRange(index, te, ++punctuationQuotationDepth, punctuationQuotationGroups.Peek());
              break;
            case System.Globalization.UnicodeCategory.ClosePunctuation:
              yield return new TextElementTokenRange(index, te, punctuationBracketDepth--, punctuationBracketGroups.Count > 0 ? punctuationBracketGroups.Pop() : -1);
              break;
            case System.Globalization.UnicodeCategory.FinalQuotePunctuation:
              yield return new TextElementTokenRange(index, te, punctuationQuotationDepth--, punctuationQuotationGroups.Count > 0 ? punctuationQuotationGroups.Pop() : -1);
              break;
            default:
              yield return new TextElementToken(index, te);
              break;
          }
        }
        else
          yield return new TextElementToken(index, te);

        index += te.Chars.Length;
      }
    }
  }
}
