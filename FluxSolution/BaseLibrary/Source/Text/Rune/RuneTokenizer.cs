/// <summary>A rune is a Unicode code point.</summary>
namespace Flux.Text
{
  /// <summary>An implementation of a tokenization engine to demarcate and classify sections of an input string.</summary>
  public readonly record struct RuneTokenizer
    : ITokenizer<IToken<System.Text.Rune>>
  {
    public System.Text.NormalizationForm? NormalizationForm { get; }

    public RuneTokenizer(System.Text.NormalizationForm normalizationForm)
      => NormalizationForm = normalizationForm;

    public System.Collections.Generic.List<IToken<System.Text.Rune>> GetTokens(string expression)
    {
      var list = new System.Collections.Generic.List<IToken<System.Text.Rune>>();

      if (expression is null) throw new System.ArgumentNullException(nameof(expression));

      if (NormalizationForm.HasValue)
        expression = expression.Normalize(NormalizationForm.Value);

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
            list.Add(new RuneTokenRange(index, rune, ++punctuationBracketDepth, punctuationBracketGroups.Peek()));
            break;
          case System.Globalization.UnicodeCategory.InitialQuotePunctuation:
            punctuationQuotationGroups.Push(++punctuationQuotationGroup);
            list.Add(new RuneTokenRange(index, rune, ++punctuationQuotationDepth, punctuationQuotationGroups.Peek()));
            break;
          case System.Globalization.UnicodeCategory.ClosePunctuation:
            list.Add(new RuneTokenRange(index, rune, punctuationBracketDepth--, punctuationBracketGroups.Count > 0 ? punctuationBracketGroups.Pop() : -1));
            break;
          case System.Globalization.UnicodeCategory.FinalQuotePunctuation:
            list.Add(new RuneTokenRange(index, rune, punctuationQuotationDepth--, punctuationQuotationGroups.Count > 0 ? punctuationQuotationGroups.Pop() : -1));
            break;
          default:
            list.Add(new RuneToken(index, rune));
            break;
        }

        index += rune.Utf16SequenceLength;
      }

      return list;
    }
  }
}
