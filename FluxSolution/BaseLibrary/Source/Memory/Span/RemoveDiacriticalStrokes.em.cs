namespace Flux
{
  public static partial class XtensionsSpan
  {
    /// <summary>Remove diacritical (latin) strokes which are not covered by the normalization forms in NET.</summary>
    public static void RemoveDiacriticalStrokes(this System.Span<char> source)
    {
      for (var index = source.Length - 1; index >= 0; index--)
      {
        var c = source[index];

        if (XtensionsChar.RemoveDiacriticalStroke(c) != c)
        {
          source[index] = c;
        }
      }
    }
  }
}
