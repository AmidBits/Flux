namespace Flux.Text.Tokenization
{
  /// <see cref="https://en.wikipedia.org/wiki/Lexical_analysis"/>
  public interface IToken<T>
  {
    public int Index { get; }
    public T Value { get; }
  }
}
