namespace Flux.Text.Tokenization
{
  /// <see cref="https://en.wikipedia.org/wiki/Lexical_analysis"/>
  public interface ITokenizer<T>
  {
    System.Collections.Generic.IEnumerable<T> GetTokens(string expression);
  }
}
