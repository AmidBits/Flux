namespace Flux
{
  public static partial class XtensionsSpan
  {
    /// <summary>Remove diacritical marks and any optional replacements desired.</summary>
    public static System.Span<char> RemoveDiacriticalMarks(this System.Span<char> source, System.Func<char, char> additionalCharacterReplacements)
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

      return sb.ToString().ToCharArray();
    }
  }
}
