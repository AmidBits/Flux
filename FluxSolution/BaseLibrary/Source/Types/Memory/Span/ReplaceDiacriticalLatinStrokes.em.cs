namespace Flux
{
  public static partial class SpanEm
  {
    /// <summary>Replace (in-place) diacritical (latin) strokes which are not covered by the normalization forms in NET. Can be done in-place because the diacritical latin stroke characters (and replacements) all fit in a single char.</summary>
    public static System.Span<char> ReplaceDiacriticalLatinStrokes(this System.Span<char> source)
    {
      for (var index = 0; index < source.Length; index++)
      {
        var sc = source[index];

        if ((char)((System.Text.Rune)sc).ReplaceDiacriticalLatinStroke().Value is var tc && tc != sc)
          source[index] = tc;
      }

      return source;
    }
  }
}
