using System.Linq;

namespace Flux
{
  public static partial class XtensionsString
  {
    /// <summary>Remove diacritical (latin) strokes which are not covered by the normalization forms in NET.</summary>
    public static string RemoveDiacriticalStrokes(this string source)
      => string.Concat(source.Select(XtensionsChar.RemoveDiacriticalStroke));
  }
}
