namespace Flux
{
  public static partial class Unicode
  {
    /// <summary>Remove diacritical marks.</summary>
    public static System.Text.StringBuilder RemoveUnicodeMarks(this string source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

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
            sb.Append(c, 1);
            break;
        }
      }

      return sb;
    }
  }
}
