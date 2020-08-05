namespace Flux
{
  public static partial class XtensionsSpan
  {
    /// <summary>Remove diacritical (latin) strokes which are not covered by the normalization forms in NET.</summary>
    public static char[] RemoveDiacriticalLatinStrokes(this System.ReadOnlySpan<char> source)
    {
      var buffer = new char[source.Length];

      for (var index = source.Length - 1; index >= 0; index--)
      {
        buffer[index] = XtensionsChar.RemoveDiacriticalLatinStroke(source[index]);
      }

      return buffer;
    }

    /// <summary>Remove diacritical marks and any optional replacements desired.</summary>
    public static System.ReadOnlySpan<char> RemoveDiacriticalMarks(this System.ReadOnlySpan<char> source, System.Func<char, char> additionalCharacterReplacements)
    {
      var sb = new System.Text.StringBuilder();

      foreach (var c in source.ToString().Normalize(System.Text.NormalizationForm.FormKD))
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
    public static System.ReadOnlySpan<char> RemoveDiacriticalMarksAndLatinStrokes(this System.ReadOnlySpan<char> source)
      => RemoveDiacriticalMarks(source, XtensionsChar.RemoveDiacriticalLatinStroke);
  }
}
