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
    {
      var sb = new System.Text.StringBuilder();

      sb.Append('<');

      sb.Append(System.Text.Rune.GetUnicodeCategory(Value));

      sb.Append('=');

      sb.Append('"');
      sb.Append(Value);
      sb.Append('"');

      sb.Append('@');

      sb.Append(Index);

      sb.Append('+');

      sb.Append(Value.Utf16SequenceLength);

      if (Depth.HasValue && Group.HasValue)
      {
        sb.Append('[');
        sb.Append("Depth=");
        sb.Append(Depth);
        sb.Append(',');
        sb.Append("Group=");
        sb.Append(Group);
        sb.Append(']');
      }

      sb.Append('>');

      return sb.ToString();
    }
  }
}
