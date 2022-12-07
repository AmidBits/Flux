/// <summary>A grapheme is a text element in dot NET.</summary>
namespace Flux
{
  /// <summary>An implementation of a demarcated and classified section of a grapheme.</summary>
  public sealed class TextElementTokenRange
    : IToken<TextElementCluster>
  {
    public int Index { get; }
    public TextElementCluster Value { get; }

    public int? Depth { get; }
    public int? Group { get; }

    public TextElementTokenRange(int index, TextElementCluster value, int depth, int group)
    {
      Index = index;
      Value = value;

      Depth = depth;
      Group = group;
    }

    public override string ToString()
      => $"{GetType().Name} {{ \"{Value.Chars}\", Index = {Index}, Chars = {Value.Chars.Length}:[{string.Join(", ", Value.Chars.Select(c => $"0x{(int)c:x4}"))}], Runes = {Value.Runes.Count}:[{string.Join(", ", Value.Runes.Select(r => r.ToStringEx()))}]{(string.Concat(TextElementToken.GetNormalizationForms(Value.Chars, false).Select((kvp, i) => $"[{kvp.Key}=\"{kvp.Value}\"]")) is var s && s.Length > 0 ? $", {s}" : string.Empty)}, Depth = {Depth}, Group = {Group} }}";
  }
}
