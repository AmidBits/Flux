namespace Flux.Text
{
  // https://en.wikipedia.org/wiki/Lexical_analysis

  public class MathToken
    : IToken<string>, IFormattable
  {
    public int Index { get; }
    public string Name { get; }
    public string Value { get; }

    public MathToken(string name, string textElement, int textIndex = -1)
    {
      Name = name;
      Value = textElement;
      Index = textIndex;
    }

    public virtual string ToString(string? format, IFormatProvider? formatProvider) => Value;

    public virtual string ToTokenString() => $"{GetType().Name}=\"{Value}\",#{Index}";

    public override string ToString() => ToString(null, null);

  }
}
