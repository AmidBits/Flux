using System.Linq;

/// <summary>A grapheme is a text element in dot NET.</summary>
namespace Flux.Text
{
  /// <summary>An implementation of a tokenization engine to demarcate and classify sections of an input string.</summary>
  public sealed class TextElementTokenizer
    : ITokenizer<IToken<TextElementCluster>>
  {
    public System.Collections.Generic.IEnumerable<IToken<TextElementCluster>> GetTokens(string expression)
    {
      using var sr = new System.IO.StringReader(expression);
      using var trtee = new TextElementEnumerator(sr);
      
      foreach (var (grapheme, index) in trtee.Select((e, i) => (e, i)))
      {
        yield return new TextElementToken(index, grapheme);
      }
    }
  }
}
