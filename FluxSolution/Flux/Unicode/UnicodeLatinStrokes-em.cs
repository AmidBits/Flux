//namespace Flux
//{
//  public static partial class Unicode
//  {
//    #region LatinStrokes

//    private static System.Collections.Generic.Dictionary<char, char> LatinStrokes = new()
//    {
//      { '\u023A', 'A' }, // Latin Capital Letter A with stroke
//      { '\u0243', 'B' }, // Latin Capital Letter B with stroke
//      { '\u0180', 'b' }, // Latin Small Letter B with stroke
//      { '\u023B', 'C' }, // Latin Capital Letter C with stroke
//      { '\u023C', 'c' }, // Latin Small Letter C with stroke
//      { '\u0110', 'D' }, // Latin Capital Letter D with stroke
//      { '\u0111', 'd' }, // Latin Small Letter D with stroke
//      { '\u0246', 'E' }, // Latin Capital Letter E with stroke
//      { '\u0247', 'e' }, // Latin Small Letter E with stroke
//      { '\u01E4', 'G' }, // Latin Capital Letter G with stroke
//      { '\u01E5', 'g' }, // Latin Small Letter G with stroke
//      { '\u0126', 'H' }, // Latin Capital Letter H with stroke
//      { '\u0127', 'h' }, // Latin Small Letter H with stroke
//      { '\u0197', 'I' }, // Latin Capital Letter I with stroke
//      { '\u0248', 'J' }, // Latin Capital Letter J with stroke
//      { '\u0249', 'j' }, // Latin Small Letter J with stroke
//      { '\u0141', 'L' }, // Latin Capital Letter L with stroke
//      { '\u0142', 'l' }, // Latin Small Letter L with stroke
//      { '\u00D8', 'O' }, // Latin Capital letter O with stroke
//      { '\u01FE', 'O' }, // Latin Capital letter O with stroke and acute
//      { '\u00F8', 'o' }, // Latin Small Letter O with stroke
//      { '\u01FF', 'o' }, // Latin Small Letter O with stroke and acute
//      { '\u024C', 'R' }, // Latin Capital letter R with stroke
//      { '\u024D', 'r' }, // Latin Small Letter R with stroke
//      { '\u0166', 'T' }, // Latin Capital Letter T with stroke
//      { '\u023E', 'T' }, // Latin Capital Letter T with diagonal stroke
//      { '\u0167', 't' }, // Latin Small Letter T with stroke
//      { '\u024E', 'Y' }, // Latin Capital letter Y with stroke
//      { '\u024F', 'y' }, // Latin Small Letter Y with stroke
//      { '\u01B5', 'Z' }, // Latin Capital Letter Z with stroke
//      { '\u01B6', 'z' }, // Latin Small Letter Z with stroke
//    };

//    /// <summary>Determines whether the <paramref name="source"/> is a latin diacritical stroke, and outputs the <paramref name="replacementChar"/>.</summary>
//    public static bool IsUnicodeLatinStroke(this char source, out char replacement)
//      => LatinStrokes.TryGetValue(source, out replacement);

//    /// <summary>Determines whether the character is a latin diacritical stroke.</summary>
//    public static bool IsUnicodeLatinStroke(this System.Text.Rune source, out System.Text.Rune replacement)
//    {
//      var iuls = LatinStrokes.TryGetValue((char)source.Value, out var replacementChar);

//      replacement = iuls ? (System.Text.Rune)replacementChar : source;

//      return iuls;
//    }

//    ///// <summary>Replaces a character with diacritical latin stroke with the closest 'plain' character, i.e. a character without a diacritic is returned in its place. Characters without latin strokes are returned as-is.</summary>
//    ///// <remarks>These are characters that are not (necessarily) identified in .NET.</remarks>
//    //public static char ReplaceUnicodeLatinStroke(this char source, out bool replaced)
//    //  => (replaced = LatinStrokes.TryGetValue(source, out var replacement)) ? replacement : source;

//    ///// <summary>Replaces a character with diacritical latin stroke with the closest 'plain' character, i.e. a character without a diacritic is returned in its place. Characters without latin strokes are returned as-is.</summary>
//    ///// <remarks>These are characters that are not (necessarily) identified in .NET.</remarks>
//    //public static System.Text.Rune ReplaceUnicodeLatinStroke(this System.Text.Rune source, out bool replaced)
//    //  => (replaced = LatinStrokes.TryGetValue((char)source.Value, out var replacement)) ? (System.Text.Rune)replacement : source;

//    ///// <summary>In-place replacement of diacritical latin strokes which are not covered by the normalization forms in NET. Can be done in-place because the diacritical latin stroke characters and their replacements are all exactly a single char.</summary>
//    //public static System.Span<char> ReplaceUnicodeLatinStrokes(this System.Span<char> source)
//    //{
//    //  for (var index = 0; index < source.Length; index++)
//    //    if (LatinStrokes.TryGetValue(source[index], out var replacement))
//    //      source[index] = replacement;

//    //  return source;
//    //}

//    ///// <summary>In-place replacement of diacritical latin strokes which are not covered by the normalization forms in NET. Can be done in-place because the diacritical latin stroke characters and their replacements are all exactly a single char.</summary>
//    //public static System.Span<System.Text.Rune> ReplaceUnicodeLatinStrokes(this System.Span<System.Text.Rune> source)
//    //{
//    //  for (var index = 0; index < source.Length; index++)
//    //    if (LatinStrokes.TryGetValue((char)(source[index].Value), out var replacement))
//    //      source[index] = (System.Text.Rune)replacement;

//    //  return source;
//    //}

//    ///// <summary>In-place replacement of diacritical latin strokes which are not covered by the normalization forms in NET. Can be done in-place because the diacritical latin stroke characters and their replacements are all exactly a single char.</summary>
//    //public static System.Text.StringBuilder ReplaceUnicodeLatinStrokes(this System.Text.StringBuilder source)
//    //{
//    //  System.ArgumentNullException.ThrowIfNull(source);

//    //  for (var index = source.Length - 1; index >= 0; index--)
//    //    if (LatinStrokes.TryGetValue(source[index], out var replacement))
//    //      source[index] = replacement;

//    //  return source;
//    //}

//    ///// <summary>In-place replacement of diacritical latin strokes which are not covered by the normalization forms in NET. Can be done in-place because the diacritical latin stroke characters and their replacements are all exactly a single char.</summary>
//    //public static SpanMaker<char> ReplaceUnicodeLatinStrokes(this SpanMaker<char> source)
//    //{
//    //  for (var index = source.Length - 1; index >= 0; index--)
//    //    if (LatinStrokes.TryGetValue(source[index], out var replacement))
//    //      source[index] = replacement;

//    //  return source;
//    //}

//    #endregion
//  }
//}
