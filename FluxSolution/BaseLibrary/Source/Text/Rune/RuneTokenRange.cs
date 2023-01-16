/// <summary>A rune is a Unicode code point.</summary>
namespace Flux.Text
{
  public readonly record struct RuneTokenRange
    : IToken<System.Text.Rune>
  {
    public int Index { get; }
    public System.Text.Rune Value { get; }

    public int? Depth { get; }
    public int? Group { get; }

    public RuneTokenRange(int index, System.Text.Rune rune, int depth, int group)
    {
      Index = index;
      Value = rune;

      Depth = depth;
      Group = group;
    }

    public override string ToString()
      => $"{GetType().Name} {{ \"{Value}\", Index = {Index}, Chars = {Value.Utf16SequenceLength}:[{string.Join(", ", Value.ToString().Select(c => $"0x{(int)c:x4}"))}], Rune = {Value.ToStringEx()}, Depth = {Depth}, Group = {Group} }}";
  }
}
