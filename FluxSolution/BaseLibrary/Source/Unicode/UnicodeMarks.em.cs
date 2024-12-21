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
        if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) is not System.Globalization.UnicodeCategory.NonSpacingMark and not System.Globalization.UnicodeCategory.SpacingCombiningMark and not System.Globalization.UnicodeCategory.EnclosingMark)
          sb.Append(c, 1);

      return sb;
    }
  }
}
