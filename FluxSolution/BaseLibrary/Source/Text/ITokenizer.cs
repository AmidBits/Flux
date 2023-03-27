namespace Flux.Text
{
  /// <see cref="https://en.wikipedia.org/wiki/Lexical_analysis"/>
  public interface ITokenizer<TValue>
  {
    /// <summary>Creates a new list of <typeparamref name="TValue"/> from the <paramref name="expression"/>.</summary>
    /// <param name="expression">The expression to parse into tokens.</param>
    /// <returns>A new list of <typeparamref name="TValue"/>.</returns>
    System.Collections.Generic.List<TValue> GetTokens(string expression);
  }
}
