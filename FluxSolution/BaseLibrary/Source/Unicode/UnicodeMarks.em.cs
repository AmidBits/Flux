namespace Flux
{
  public static partial class ExtensionMethodsUnicode
  {
    /// <summary>Remove diacritical marks and any optional replacements desired.</summary>
    public static SpanBuilder<char> RemoveUnicodeMarks(this string source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var sb = new SpanBuilder<char>();

      foreach (var c in source.Normalize(System.Text.NormalizationForm.FormKD))
      {
        switch (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c))
        {
          case System.Globalization.UnicodeCategory.NonSpacingMark:
          case System.Globalization.UnicodeCategory.SpacingCombiningMark:
          case System.Globalization.UnicodeCategory.EnclosingMark:
            break;
          default:
            sb.Append(c);
            break;
        }
      }

      return sb;
    }
  }
}
