namespace Flux
{
  /// <summary></summary>
  /// <remarks>A code point is a Unicode number representing a defined meaning.</remarks>
  /// <remarks>In .NET, the <see cref="System.Text.Rune"/> represents a Unicode code point.</remarks>
  /// <remarks>A code unit is a unit of storage for encoding code points. E.g. UTF-16 is a 16-bit code unit. One or more code units may be used to represented a code point.</remarks>
  /// <remarks>In .NET, the type <see cref="char"/> is a code unit identified as UTF-16.</remarks>
  /// <remarks>A grapheme is one or more code points representing an element of writing.</remarks>
  /// <remarks>In .NET, a <see cref="string"/> represents graphemes enumerated by .NET tools.</remarks>
  /// <remarks>A glyph is a visual "image", typically in a font, used to represent visual "symbols". One or more glyphs may be used to represent a grapheme.</remarks>
  public static partial class Unicode
  {
    public static System.Collections.Generic.IDictionary<System.Globalization.UnicodeCategory, System.Collections.Generic.List<char>> GetCharactersByUnicodeCategory()
    {
      var unicodeCategoryCharacters = new System.Collections.Generic.Dictionary<System.Globalization.UnicodeCategory, System.Collections.Generic.List<char>>();

      foreach (var unicodeCategoryValue in System.Linq.Enumerable.Cast<System.Globalization.UnicodeCategory>(System.Enum.GetValues(typeof(System.Globalization.UnicodeCategory))))
        unicodeCategoryCharacters.Add(unicodeCategoryValue, new System.Collections.Generic.List<char>());

      var charValue = char.MinValue;

      while (true)
      {
        unicodeCategoryCharacters[System.Globalization.CharUnicodeInfo.GetUnicodeCategory(charValue)].Add(charValue);

        if (charValue++ == char.MaxValue)
          break;
      }

      return unicodeCategoryCharacters;
    }

    /// <summary>Returns a new sequence of Unicode code points from the string.</summary>
    public static System.Collections.Generic.IEnumerable<int> GetCodePoints(string characters)
    {
      if (characters is null) throw new System.ArgumentNullException(nameof(characters));

      for (var index = 0; index < characters.Length; index += char.IsSurrogatePair(characters, index) ? 2 : 1)
        yield return char.ConvertToUtf32(characters, index);
    }
  }
}
