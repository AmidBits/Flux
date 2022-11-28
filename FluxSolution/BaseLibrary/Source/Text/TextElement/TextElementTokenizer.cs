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

      var index = 0;

      foreach (var te in tee)
      {
        yield return new TextElementToken(index, te);

        index += te.Chars.Length;
      }
    }
  }
}
