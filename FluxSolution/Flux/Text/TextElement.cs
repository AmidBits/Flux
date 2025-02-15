namespace Flux.Text
{
  /// <summary>
  /// <para>A text element represents a grapheme cluster, which consists of multiple Unicode code points.</para>
  /// <para><see href="https://learn.microsoft.com/en-us/dotnet/standard/base-types/character-encoding-introduction#grapheme-clusters"/></para>
  /// </summary>
  public readonly record struct TextElement
  {
    private readonly System.Collections.Generic.List<char> m_chars;

    public TextElement(System.ReadOnlySpan<char> textElement)
    {
      if (!textElement.IsTextElement()) throw new System.InvalidOperationException();

      m_chars = textElement.ToList();
    }

    /// <summary>Non-allocating <see cref="System.Collections.Generic.IReadOnlyList{T}"/> of <see cref="System.Char"/>. The characters of the TextElement was already populated on init.</summary>
    public System.Collections.Generic.IReadOnlyList<char> AsReadOnlyListOfChar => m_chars;

    /// <summary>Non-allocating <see cref="System.ReadOnlySpan{T}"/> of <see cref="System.Char"/>. The characters of the TextElement was already populated on init.</summary>
    public System.ReadOnlySpan<char> AsReadOnlySpan() => m_chars.AsReadOnlySpan();

    public override string ToString() => AsReadOnlySpan().ToString();
  }

  //public readonly record struct TextElement
  //{
  //  private readonly string m_value;

  //  public TextElement(string value)
  //  {
  //    if (System.Globalization.StringInfo.GetNextTextElementLength(value) != value.Length) throw new System.ArgumentOutOfRangeException(nameof(value));

  //    m_value = value;
  //  }

  //  public string Value => m_value;

  //  public System.Collections.Generic.IReadOnlyList<char> AsReadOnlyListOfChar => m_value.ToCharArray();

  //  public System.ReadOnlySpan<char> AsReadOnlySpan() => m_value.AsSpan();

  //  public override string ToString() => m_value;
  //}
}
