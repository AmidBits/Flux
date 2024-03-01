namespace Flux.Text
{
  // https://en.wikipedia.org/wiki/Lexical_analysis

  public class MathToken
    : IToken<string>, System.IFormattable
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

    public virtual string ToString(string? format, System.IFormatProvider? formatProvider) => string.Format(formatProvider, $"{{0}}{(format is null ? string.Empty : $":{format}")}", Value);

    public virtual string ToTokenString() => $"{GetType().Name} {{ \"{ToString()}\", Index = {Index} }}";

    public override string ToString() => ToString(null, null);

  }
}
