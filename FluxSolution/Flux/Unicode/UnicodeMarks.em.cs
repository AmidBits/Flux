namespace Flux
{
  public static partial class Em
  {
    /// <summary>Remove diacritical marks.</summary>
    public static SpanMaker<char> RemoveUnicodeMarks(this string source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var sm = new SpanMaker<char>();

      foreach (var c in source.Normalize(System.Text.NormalizationForm.FormKD))
        if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) is not System.Globalization.UnicodeCategory.NonSpacingMark and not System.Globalization.UnicodeCategory.SpacingCombiningMark and not System.Globalization.UnicodeCategory.EnclosingMark)
          sm = sm.Append(c);

      return sm;
    }
  }
}
