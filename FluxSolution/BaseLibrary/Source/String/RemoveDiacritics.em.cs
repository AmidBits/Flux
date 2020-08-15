using System.Linq;

namespace Flux
{
  public static partial class XtendString
  {
    /// <summary>Remove diacritical (latin) strokes which are not covered by the normalization forms in NET.</summary>
    public static string RemoveDiacriticalLatinStrokes(this string source)
      => string.Concat(source.Select(XtendChar.RemoveDiacriticalLatinStroke));

    /// <summary>Remove diacritical marks and any optional replacements desired.</summary>
    public static string RemoveDiacriticalMarks(this string source, System.Func<char, char> additionalCharacterReplacements)
    {
      var sb = new System.Text.StringBuilder();

      foreach (var c in source.Normalize(System.Text.NormalizationForm.FormKD))
      {
        switch (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c))
        {
          case System.Globalization.UnicodeCategory.NonSpacingMark:
          case System.Globalization.UnicodeCategory.SpacingCombiningMark:
          case System.Globalization.UnicodeCategory.EnclosingMark:
            break;
          default:
            sb.Append(additionalCharacterReplacements(c));
            break;
        }
      }

      return sb.ToString();
    }

    /// <summary>Remove diacritical marks and latin strokes (the latter are unaffected by normalization forms in NET).</summary>
    public static string RemoveDiacriticalMarksAndStrokes(this string source)
      => source.RemoveDiacriticalMarks(XtendChar.RemoveDiacriticalLatinStroke);
  }
}
