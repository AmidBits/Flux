namespace Flux
{
  namespace Text
  {
    /// <see cref="https://en.wikipedia.org/wiki/Lexical_analysis"/>
    public interface IToken<TValue>
      where TValue : notnull
    {
      int Index { get; }
      TValue Value { get; }

      string ToTokenString() => $"{GetType().Name} {{ \"{Value}\", Index = {Index} }}";
    }
  }
}
