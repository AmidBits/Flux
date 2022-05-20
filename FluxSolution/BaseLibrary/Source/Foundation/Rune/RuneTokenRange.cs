/// <summary>A rune is a Unicode code point.</summary>
namespace Flux
{
  public struct RuneTokenRange
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
      => $"{GetType().Name} {{ \"{Value}\", Index = {Index}, Length = {Value.Utf16SequenceLength}, Depth = {Depth}, Group = {Group}, {System.Text.Rune.GetUnicodeCategory(Value)} = {Value.ToUnotationString()} }}";
  }
}
