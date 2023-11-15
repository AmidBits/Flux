namespace Flux
{
  namespace Text
  {
    /// <summary>
    /// <para>A text element represents a grapheme cluster, which consists of multiple Unicode code points.</para>
    /// <para><see cref="https://learn.microsoft.com/en-us/dotnet/standard/base-types/character-encoding-introduction#grapheme-clusters"/></para>
    /// </summary>
    public record class TextElement
    {
      private readonly char[] m_chars;

      public TextElement(System.ReadOnlySpan<char> textElement)
      {
        if (System.Globalization.StringInfo.GetNextTextElementLength(textElement) != textElement.Length) throw new System.ArgumentOutOfRangeException(nameof(textElement));

        m_chars = textElement.ToArray();
      }

      /// <summary>Non-allocating <see cref="System.Collections.Generic.IReadOnlyList{T}"/> of <see cref="System.Char"/>. The characters of the TextElement was already populated on init.</summary>
      public System.Collections.Generic.IReadOnlyList<char> AsReadOnlyListOfChar => m_chars;

      /// <summary>Non-allocating <see cref="System.ReadOnlySpan{T}"/> of <see cref="System.Char"/>. The characters of the TextElement was already populated on init.</summary>
      public System.ReadOnlySpan<char> AsReadOnlySpanOfChar => m_chars;

      public override string ToString() => AsReadOnlySpanOfChar.ToString();
    }
  }
}
