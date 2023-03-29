namespace Flux
{
  ///// <summary></summary>
  ///// <remarks>
  ///// <para>A code point is a Unicode number representing a defined meaning. One or more code points may be used to represent higher order constructs, e.g. a grapheme.</para>
  ///// <para>In .NET, the <see cref="System.Text.Rune"/> represents a Unicode code point.</para>
  ///// <para>A code unit is a unit of storage for encoding code points. E.g. UTF-16 is a 16-bit code unit. One or more code units may be used to represent a code point.</para>
  ///// <para>In .NET, the type <see cref="char"/> is a code unit identified as UTF-16. Multiple <see cref="char"/>s are used to represent larger constructs, e.g. code points (<see cref="System.Text.Rune"/>) and graphemes (text elements).</para>
  ///// <para>A grapheme is one or more code points representing an element of writing.</para>
  ///// <para>In .NET, a grapheme is a text element represented as a sequence of <see cref="char"/> instances, e.g. in a <see cref="string"/>.</para>
  ///// <para>A glyph is a visual "image", e.g. in a font, used to represent visual "symbols". One or more glyphs may be used to represent a grapheme.</para>
  ///// </remarks>

  public static partial class UnicodeExtensionMethods
  {
    /// <summary>Determines whether the character is a latin diacritical stroke.</summary>
    public static bool IsUnicodeLatinStroke(this char source)
      => source != ReplaceUnicodeLatinStroke(source);

    /// <summary>Determines whether the character is a latin diacritical stroke.</summary>
    public static bool IsUnicodeLatinStroke(this char source, out char replacementChar)
      => source != (replacementChar = ReplaceUnicodeLatinStroke(source));

    /// <summary>Determines whether the character is a latin diacritical stroke.</summary>
    public static bool IsUnicodeLatinStroke(this System.Text.Rune source)
      => source != ReplaceUnicodeLatinStroke(source);

    /// <summary>Determines whether the character is a latin diacritical stroke.</summary>
    public static bool IsUnicodeLatinStroke(this System.Text.Rune source, out System.Text.Rune replacementRune)
      => source != (replacementRune = ReplaceUnicodeLatinStroke(source));

    /// <summary>Replaces a character with diacritical latin stroke with the closest 'plain' character, i.e. a character without a diacritic is returned in its place. Characters without latin strokes are returned as-is.</summary>
    /// <remarks>These are characters that are not (necessarily) identified in .NET.</remarks>
    public static char ReplaceUnicodeLatinStroke(this char source)
      => source switch
      {
        '\u023A' => 'A', // Latin Capital Letter A with stroke
        '\u0243' => 'B', // Latin Capital Letter B with stroke
        '\u0180' => 'b', // Latin Small Letter B with stroke
        '\u023B' => 'C', // Latin Capital Letter C with stroke
        '\u023C' => 'c', // Latin Small Letter C with stroke
        '\u0110' => 'D', // Latin Capital Letter D with stroke
        '\u0111' => 'd', // Latin Small Letter D with stroke
        '\u0246' => 'E', // Latin Capital Letter E with stroke
        '\u0247' => 'e', // Latin Small Letter E with stroke
        '\u01E4' => 'G', // Latin Capital Letter G with stroke
        '\u01E5' => 'g', // Latin Small Letter G with stroke
        '\u0126' => 'H', // Latin Capital Letter H with stroke
        '\u0127' => 'h', // Latin Small Letter H with stroke
        '\u0197' => 'I', // Latin Capital Letter I with stroke
        '\u0248' => 'J', // Latin Capital Letter J with stroke
        '\u0249' => 'j', // Latin Small Letter J with stroke
        '\u0141' => 'L', // Latin Capital Letter L with stroke
        '\u0142' => 'l', // Latin Small Letter L with stroke
        '\u00D8' => 'O', // Latin Capital letter O with stroke
        '\u01FE' => 'O', // Latin Capital letter O with stroke and acute
        '\u00F8' => 'o', // Latin Small Letter O with stroke
        '\u01FF' => 'o', // Latin Small Letter O with stroke and acute
        '\u024C' => 'R', // Latin Capital letter R with stroke
        '\u024D' => 'r', // Latin Small Letter R with stroke
        '\u0166' => 'T', // Latin Capital Letter T with stroke
        '\u023E' => 'T', // Latin Capital Letter T with diagonal stroke
        '\u0167' => 't', // Latin Small Letter T with stroke
        '\u024E' => 'Y', // Latin Capital letter Y with stroke
        '\u024F' => 'y', // Latin Small Letter Y with stroke
        '\u01B5' => 'Z', // Latin Capital Letter Z with stroke
        '\u01B6' => 'z', // Latin Small Letter Z with stroke
        _ => source,
      };

    /// <summary>Replaces a character with diacritical latin stroke with the closest 'plain' character, i.e. a character without a diacritic is returned in its place. Characters without latin strokes are returned as-is.</summary>
    /// <remarks>These are characters that are not (necessarily) identified in .NET.</remarks>
    public static System.Text.Rune ReplaceUnicodeLatinStroke(this System.Text.Rune source)
      => (System.Text.Rune)ReplaceUnicodeLatinStroke((char)source.Value);

    /// <summary>In-place replacement of diacritical latin strokes which are not covered by the normalization forms in NET. Can be done in-place because the diacritical latin stroke characters and their replacements are all exactly a single char.</summary>
    public static System.Span<char> ReplaceUnicodeLatinStrokes(this System.Span<char> source)
    {
      for (var index = 0; index < source.Length; index++)
        if (IsUnicodeLatinStroke(source[index], out var rc))
          source[index] = rc;

      return source;
    }

    /// <summary>In-place replacement of diacritical latin strokes which are not covered by the normalization forms in NET. Can be done in-place because the diacritical latin stroke characters and their replacements are all exactly a single char.</summary>
    public static System.Span<System.Text.Rune> ReplaceUnicodeLatinStrokes(this System.Span<System.Text.Rune> source)
    {
      for (var index = 0; index < source.Length; index++)
        if (IsUnicodeLatinStroke(source[index], out var rc))
          source[index] = rc;

      return source;
    }

    /// <summary>In-place replacement of diacritical latin strokes which are not covered by the normalization forms in NET. Can be done in-place because the diacritical latin stroke characters and their replacements are all exactly a single char.</summary>
    public static SpanBuilder<char> ReplaceUnicodeLatinStrokes(this SpanBuilder<char> source)
    {
      for (var index = 0; index < source.Length; index++)
        if (IsUnicodeLatinStroke(source[index], out var rc))
          source[index] = rc;

      return source;
    }

    /// <summary>In-place replacement of diacritical latin strokes which are not covered by the normalization forms in NET. Can be done in-place because the diacritical latin stroke characters and their replacements are all exactly a single char.</summary>
    public static SpanBuilder<System.Text.Rune> ReplaceUnicodeLatinStrokes(this SpanBuilder<System.Text.Rune> source)
    {
      for (var index = 0; index < source.Length; index++)
        if (IsUnicodeLatinStroke(source[index], out var rc))
          source[index] = rc;

      return source;
    }

    /// <summary>In-place replacement of diacritical latin strokes which are not covered by the normalization forms in NET. Can be done in-place because the diacritical latin stroke characters and their replacements are all exactly a single char.</summary>
    public static System.Text.StringBuilder ReplaceUnicodeLatinStrokes(this System.Text.StringBuilder source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      for (var index = 0; index < source.Length; index++)
        if (IsUnicodeLatinStroke(source[index], out var rc))
          source[index] = rc;

      return source;
    }
  }
}
