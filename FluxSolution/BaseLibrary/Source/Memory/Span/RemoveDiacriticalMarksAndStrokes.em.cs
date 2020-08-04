namespace Flux
{
  public static partial class XtensionsSpan
  {
    /// <summary>Remove diacritical marks and latin strokes (the latter are unaffected by normalization forms in NET).</summary>
    public static System.Span<char> RemoveDiacriticalMarksAndStrokes(this System.Span<char> source)
      => RemoveDiacriticalMarks(source, XtensionsChar.RemoveDiacriticalStroke);
  }
}
