namespace Flux.Text
{
  // https://en.wikipedia.org/wiki/Lexical_analysis

  public class MathToken
    : IToken<string>
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

    public override string ToString() => $"{this.GetType().Name}.{Name}=\"{Value}\",#{Index}";
  }
}
