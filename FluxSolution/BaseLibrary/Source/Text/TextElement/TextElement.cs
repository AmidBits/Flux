namespace Flux.Text
{
  /// <summary>
  /// <para>A text element represents a grapheme cluster, sort of like the .</para>
  /// <para><see cref="https://learn.microsoft.com/en-us/dotnet/standard/base-types/character-encoding-introduction#grapheme-clusters"/></para>
  /// </summary>
  public record class TextElement
  {
    private readonly char[] m_chars;

    private System.Collections.Generic.List<System.Text.Rune>? m_runes;

    private readonly int m_sourceIndex;

    public TextElement(System.ReadOnlySpan<char> textElement, int index)
    {
      if (System.Globalization.StringInfo.GetNextTextElementLength(textElement) != textElement.Length) throw new System.ArgumentOutOfRangeException(nameof(textElement));

      m_chars = textElement.ToArray();

      m_runes = null;

      m_sourceIndex = index;
    }

    /// <summary>Non-allocating <see cref="System.Collections.Generic.IReadOnlyList{T}"/> of <see cref="System.Char"/>. The characters of the TextElement was already populated on init.</summary>
    public System.Collections.Generic.IReadOnlyList<char> AsReadOnlyListChar => m_chars;

    /// <summary>Non-allocating <see cref="System.ReadOnlySpan{T}"/> of <see cref="System.Char"/>. The characters of the TextElement was already populated on init.</summary>
    public System.ReadOnlySpan<char> AsReadOnlySpanChar => m_chars;

    public int SourceIndex => m_sourceIndex;

    /// <summary>Creates a one time allocating <see cref="System.Collections.Generic.List{T}"/> of <see cref="System.Text.Rune"/>.</summary>
    /// <remarks>IMPORTANT! The inital call and any subsequent calls, all receive the same allocated list.</remarks>
    public System.Collections.Generic.List<System.Text.Rune> ToListRune()
    {
      if (m_runes is null)
      {
        m_runes = new System.Collections.Generic.List<System.Text.Rune>();

        foreach (var rune in m_chars.AsSpan().EnumerateRunes())
          m_runes.Add(rune);
      }

      return m_runes;
    }

    /// <summary>One-time-allocating <see cref="System.Collections.Generic.IReadOnlyList{T}"/> of <see cref="System.Text.Rune"/>. The runes of the TextElement is populated on first use only, therefor subsequent calls are non-allocating.</summary>
    public System.Collections.Generic.IReadOnlyList<System.Text.Rune> ToReadOnlyListRune() => ToListRune();

    /// <summary>One-time-allocating <see cref="System.ReadOnlySpan{T}"/> of <see cref="System.Text.Rune"/>. The runes of the TextElement is populated on first use only, therefor subsequent calls are non-allocating.</summary>
    public System.ReadOnlySpan<System.Text.Rune> ToReadOnlySpanRune() => ToListRune().AsSpan();

    public override string ToString() => AsReadOnlySpanChar.ToString();
  }
}
