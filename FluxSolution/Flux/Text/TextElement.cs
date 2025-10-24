namespace Flux.Text
{
  /// <summary>
  /// <para>A text element represents a grapheme cluster, which consists of multiple Unicode code points.</para>
  /// <para><see href="https://learn.microsoft.com/en-us/dotnet/standard/base-types/character-encoding-introduction#grapheme-clusters"/></para>
  /// </summary>
  public readonly record struct TextElement
  {
    private readonly string m_string;

    public TextElement(string textElement)
    {
      if (!textElement.IsTextElement()) throw new System.InvalidOperationException();

      m_string = textElement;
    }

    /// <summary>Non-allocating <see cref="System.Collections.Generic.IReadOnlyList{T}"/> of <see cref="System.Char"/>. The characters of the TextElement was already populated on init.</summary>
    public System.Collections.Generic.IReadOnlyList<char> AsReadOnlyListOfChar => m_string.ToCharArray();

    /// <summary>Non-allocating <see cref="System.ReadOnlySpan{T}"/> of <see cref="System.Char"/>. The characters of the TextElement was already populated on init.</summary>
    public System.ReadOnlySpan<char> AsReadOnlySpan() => m_string.AsSpan();

    public override string ToString() => m_string;
  }
}
