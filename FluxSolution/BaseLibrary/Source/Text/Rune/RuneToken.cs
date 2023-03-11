using System.Linq;

/// <summary>A rune is a Unicode code point.</summary>
namespace Flux.Text
{
  /// <summary>An implementation of a demarcated and classified section of a rune.</summary>
  public readonly record struct RuneToken
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
      => $"{GetType().Name} {{ \"{Value}\", Index = {Index}, Chars = {Value.Utf16SequenceLength}:[{string.Join(", ", Value.ToString().Select(c => $"0x{(int)c:x4}"))}], Rune = {Value.ToStringEx()} }}";
  }
}
