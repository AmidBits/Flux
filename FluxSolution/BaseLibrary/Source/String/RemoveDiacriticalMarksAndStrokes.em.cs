namespace Flux
{
  public static partial class XtensionsString
  {
    /// <summary>Remove diacritical marks and latin strokes (the latter are unaffected by normalization forms in NET).</summary>
    public static string RemoveDiacriticalMarksAndStrokes(this string source)
      => source.RemoveDiacriticalMarks(XtensionsChar.RemoveDiacriticalStroke);
  }
}
