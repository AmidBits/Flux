namespace Flux
{
  public static partial class SpanEm
  {
    /// <summary>Replace (in-place) diacritical (latin) strokes which are not covered by the normalization forms in NET. Can be done in-place because the diacritical latin stroke characters (and replacements) all fit in a single char.</summary>
    public static void ReplaceDiacriticalLatinStrokes(this System.Span<System.Text.Rune> source)
    {
      for (var index = 0; index < source.Length; index++)
      {
        var sc = source[index];

        if (sc.ReplaceDiacriticalLatinStroke() is var tc && tc != sc)
          source[index] = tc;
      }
    }
  }
}
