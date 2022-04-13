namespace Flux.Text
{
  public sealed class TextElementCluster
  {
    public string Chars { get; }
    public System.Collections.Generic.IReadOnlyList<System.Text.Rune> Runes { get; }

    public int SourceIndex { get; }

    public TextElementCluster(string textElement, int sourceIndex)
    {
      if (textElement is null) throw new System.ArgumentNullException(nameof(textElement));
      if (new System.Globalization.StringInfo(textElement).LengthInTextElements != 1) throw new System.ArgumentOutOfRangeException(textElement);

      Chars = textElement;
      Runes = new System.Collections.Generic.List<System.Text.Rune>(textElement.EnumerateRunes());

      SourceIndex = sourceIndex;
    }

    public static System.Collections.Generic.IEnumerable<TextElementCluster> GetAll(string text)
    {
      var e = System.Globalization.StringInfo.GetTextElementEnumerator(text);

      while (e.MoveNext())
        yield return new TextElementCluster((string)e.Current, e.ElementIndex);
    }

    public override string ToString()
      => $"{GetType().Name} {{ @{SourceIndex} \"{new string(Chars)}\" ({Chars.Length} char{(Chars.Length > 1 ? @"s" : string.Empty)}, {Runes.Count} rune{(Runes.Count > 1 ? @"s" : string.Empty)}) }}";
  }
}
