/// <summary>A rune is a Unicode code point.</summary>
namespace Flux
{
  /// <summary>An implementation of a demarcated and classified section of a rune.</summary>
  public struct RuneToken
    : IToken<System.Text.Rune>
  {
    public int Index { get; }
    public System.Text.Rune Value { get; }

    public RuneToken(int index, System.Text.Rune rune)
    {
      Index = index;
      Value = rune;
    }

    public override string ToString()
      => $"{GetType().Name} {{ \"{Value}\", Index = {Index}, Length = {Value.Utf16SequenceLength}, {Value.ToStringEx()} }}";
  }
}
