namespace Flux.Text
{
  /// <summary>
  /// <para>A text element represents a grapheme cluster, sort of like the .</para>
  /// <para><see cref="https://learn.microsoft.com/en-us/dotnet/standard/base-types/character-encoding-introduction#grapheme-clusters"/></para>
  /// </summary>
  public record class TextElement
  {
    private readonly char[] m_chars;

    private readonly System.Text.Rune[] m_runes;

    private readonly int m_sourceIndex;

    public TextElement(System.ReadOnlySpan<char> textElement, int index)
    {
      if (System.Globalization.StringInfo.GetNextTextElementLength(textElement) != textElement.Length) throw new System.ArgumentOutOfRangeException(nameof(textElement));

      m_chars = textElement.ToArray();

      m_runes = new System.Text.Rune[textElement.Length];

      var count = 0;
      foreach (var rune in textElement.EnumerateRunes())
        m_runes[count++] = rune;

      System.Array.Resize(ref m_runes, count);

      m_sourceIndex = index;
    }

    public System.Collections.Generic.IReadOnlyList<char> ListChar => m_chars;

    public System.Collections.Generic.IReadOnlyList<System.Text.Rune> ListRune => m_runes;

    public int SourceIndex => m_sourceIndex;

    public System.ReadOnlySpan<char> SpanChar => m_chars;

    public System.ReadOnlySpan<System.Text.Rune> SpanRune => m_runes;

    //public static System.Collections.Generic.IEnumerable<TextElementCluster> ExtractAll(System.ReadOnlySpan<char> textElements)
    //{
    //  var index = 0;

    //  while (textElements.Length > 0)
    //  {
    //    var length = System.Globalization.StringInfo.GetNextTextElementLength(textElements);

    //    yield return new TextElementCluster(textElements.Slice(0, length), index);

    //    index += length;

    //    textElements = textElements.Slice(length);
    //  }
    //}

    //public static System.Collections.Generic.IEnumerable<TextElementCluster> ExtractAll(string text)
    //{
    //  var e = System.Globalization.StringInfo.GetTextElementEnumerator(text);

    //  while (e.MoveNext())
    //    yield return new TextElementCluster((string)e.Current, e.ElementIndex);
    //}

    public override string ToString() => SpanChar.ToString();

    //=> $"{GetType().Name} {{ @{m_sourceIndex} \"{new string(m_chars)}\" ({m_chars.Length} char{(m_chars.Length > 1 ? @"s" : string.Empty)}, {m_runes.Length} rune{(m_runes.Length > 1 ? @"s" : string.Empty)}) }}";
  }
}
