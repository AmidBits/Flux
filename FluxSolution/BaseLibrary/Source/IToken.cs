namespace Flux
{
  /// <see cref="https://en.wikipedia.org/wiki/Lexical_analysis"/>
  public interface IToken<TValue>
    where TValue : notnull
  {
    public int Index { get; }
    public TValue Value { get; }
  }
}
