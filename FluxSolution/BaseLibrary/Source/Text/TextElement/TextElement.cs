namespace Flux.Text
{
  /// <summary>
  /// <para>A text element represents a grapheme cluster, sort of like the .</para>
  /// <para><see cref="https://learn.microsoft.com/en-us/dotnet/standard/base-types/character-encoding-introduction#grapheme-clusters"/></para>
  /// </summary>
  public record class TextElement
  {
    private readonly char[] m_chars;

    private System.Text.Rune[]? m_runes;

    private readonly int m_sourceIndex;

    public TextElement(System.ReadOnlySpan<char> textElement, int index)
    {
      if (System.Globalization.StringInfo.GetNextTextElementLength(textElement) != textElement.Length) throw new System.ArgumentOutOfRangeException(nameof(textElement));

      m_chars = textElement.ToArray();

      m_runes = null;

      m_sourceIndex = index;
    }

    private System.Text.Rune[] EnumeratedRunes()
    {
      if (m_runes is null)
      {
        m_runes = new System.Text.Rune[m_chars.Length];

        var count = 0;
        foreach (var rune in m_chars.AsSpan().EnumerateRunes())
          m_runes[count++] = rune;

        System.Array.Resize(ref m_runes, count);
      }

      return m_runes;
    }

    public System.Collections.Generic.IReadOnlyList<char> ListChar => m_chars;

    public System.Collections.Generic.IReadOnlyList<System.Text.Rune> ListRune => EnumeratedRunes();

    public int SourceIndex => m_sourceIndex;

    public System.ReadOnlySpan<char> SpanChar => m_chars;

    public System.ReadOnlySpan<System.Text.Rune> SpanRune => EnumeratedRunes();

    public override string ToString() => SpanChar.ToString();
  }
}
