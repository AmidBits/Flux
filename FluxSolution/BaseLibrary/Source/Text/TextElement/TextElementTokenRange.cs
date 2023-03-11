using System.Linq;

/// <summary>A grapheme is a text element in dot NET.</summary>
namespace Flux.Text
{
  /// <summary>An implementation of a demarcated and classified section of a grapheme.</summary>
  public sealed class TextElementTokenRange
    : IToken<TextElement>
  {
    public int Index { get; }
    public TextElement Value { get; }

    public int? Depth { get; }
    public int? Group { get; }

    public TextElementTokenRange(int index, TextElement value, int depth, int group)
    {
      Index = index;
      Value = value;

      Depth = depth;
      Group = group;
    }

    public override string ToString()
      => $"{GetType().Name} {{ \"{Value.ListChar}\", Index = {Index}, Chars = {Value.ListChar.Count}:[{string.Join(", ", Value.ListChar.Select(c => $"0x{(int)c:x4}"))}], Runes = {Value.ListRune.Count}:[{string.Join(", ", Value.ListRune.Select(r => r.ToStringEx()))}]{(string.Concat(TextElementToken.GetNormalizationForms(string.Concat(Value.ListChar), false).Select((kvp, i) => $"[{kvp.Key}=\"{kvp.Value}\"]")) is var s && s.Length > 0 ? $", {s}" : string.Empty)}, Depth = {Depth}, Group = {Group} }}";
  }
}
