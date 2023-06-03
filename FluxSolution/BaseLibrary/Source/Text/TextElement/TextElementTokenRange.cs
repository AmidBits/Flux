using System.Linq;

/// <summary>A grapheme is a text element in dot NET.</summary>
namespace Flux
{
  namespace Text
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
        => $"{GetType().Name} {{ \"{Value.AsReadOnlyListChar}\", Index = {Index}, Chars = {Value.AsReadOnlyListChar.Count}:[{string.Join(", ", Value.AsReadOnlyListChar.Select(c => $"0x{(int)c:x4}"))}], Runes = {Value.ToReadOnlyListRune().Count}:[{string.Join(", ", Value.ToReadOnlyListRune().Select(r => r.ToStringEx()))}]{(string.Concat(TextElementToken.GetNormalizationForms(string.Concat(Value.AsReadOnlyListChar), false).Select((kvp, i) => $"[{kvp.Key}=\"{kvp.Value}\"]")) is var s && s.Length > 0 ? $", {s}" : string.Empty)}, Depth = {Depth}, Group = {Group} }}";
    }
  }
}
