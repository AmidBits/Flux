namespace Flux.Text
{
  /// <see cref="https://en.wikipedia.org/wiki/Lexical_analysis"/>
  public interface ITokenizer<TValue>
  {
    System.Collections.Generic.IEnumerable<TValue> GetTokens(string expression);
  }
}
