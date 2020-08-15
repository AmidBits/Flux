namespace Flux
{
  public static partial class XtendStringBuilder
  {
    /// <summary>Remove diacritical (latin) strokes which are not covered by the normalization forms in NET.</summary>
    public static System.Text.StringBuilder RemoveDiacriticalLatinStrokes(this System.Text.StringBuilder source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      for (var index = 0; index < source.Length; index++)
      {
        source[index] = XtensionsChar.RemoveDiacriticalLatinStroke(source[index]);
      }

      return source;
    }

    /// <summary>Remove diacritical marks and any optional replacements desired.</summary>
    public static System.Text.StringBuilder RemoveDiacriticalMarks(this System.Text.StringBuilder source, System.Func<char, char> additionalCharacterReplacements)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
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

      return sb;
    }

    /// <summary>Remove diacritical marks and latin strokes (the latter are unaffected by normalization forms in NET).</summary>
    public static System.Text.StringBuilder RemoveDiacriticalMarksAndLatinStrokes(this System.Text.StringBuilder source)
      => RemoveDiacriticalMarks(source, XtensionsChar.RemoveDiacriticalLatinStroke);
  }
}
