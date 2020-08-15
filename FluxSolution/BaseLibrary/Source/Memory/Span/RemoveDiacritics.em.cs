namespace Flux
{
  public static partial class XtendSpan
  {
    /// <summary>Remove diacritical marks and any optional replacements desired.</summary>
    public static System.Span<char> RemoveDiacriticalMarks(this System.Span<char> source, System.Func<char, char> additionalCharacterReplacements)
    {
      if (additionalCharacterReplacements is null) throw new System.ArgumentNullException(nameof(additionalCharacterReplacements));

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

      return sb.ToString().ToCharArray();
    }

    /// <summary>Remove diacritical marks and latin strokes (the latter are unaffected by normalization forms in NET).</summary>
    public static System.Span<char> RemoveDiacriticalMarksAndStrokes(this System.Span<char> source)
      => RemoveDiacriticalMarks(source, XtendChar.RemoveDiacriticalLatinStroke);

    /// <summary>Remove diacritical (latin) strokes which are not covered by the normalization forms in NET.</summary>
    public static void RemoveDiacriticalStrokes(this System.Span<char> source)
    {
      for (var index = source.Length - 1; index >= 0; index--)
      {
        var c = source[index];

        if (XtendChar.RemoveDiacriticalLatinStroke(c) != c)
        {
          source[index] = c;
        }
      }
    }
  }
}
