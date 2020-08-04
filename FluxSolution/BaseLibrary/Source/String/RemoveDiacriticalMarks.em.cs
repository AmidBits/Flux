namespace Flux
{
  public static partial class XtensionsString
  {
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
  }
}
