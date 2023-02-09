namespace Flux
{
  public static partial class ExtensionMethodsUnicode
  {
    /// <summary>Remove diacritical marks.</summary>
    public static System.Text.StringBuilder RemoveUnicodeMarks(this string source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

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
            sb.Append(c);
            break;
        }
      }

      return sb;
    }

    /// <summary>Remove diacritical marks.</summary>
    public static SequenceBuilder<char> RemoveUnicodeMarks(this System.ReadOnlySpan<char> source)
    {
      var sb = new SequenceBuilder<char>();

      for (var index = 0; index < source.Length; index++)
      {
        var c = source[index];

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

    /// <summary>Remove diacritical marks.</summary>
    public static SequenceBuilder<System.Text.Rune> RemoveUnicodeMarks(this System.ReadOnlySpan<System.Text.Rune> source)
    {
      var sb = new SequenceBuilder<System.Text.Rune>();

      for (var index = 0; index < source.Length; index++)
      {
        var rune = source[index];

        switch (System.Text.Rune.GetUnicodeCategory(rune))
        {
          case System.Globalization.UnicodeCategory.NonSpacingMark:
          case System.Globalization.UnicodeCategory.SpacingCombiningMark:
          case System.Globalization.UnicodeCategory.EnclosingMark:
            break;
          default:
            sb.Append(rune);
            break;
        }
      }

      return sb;
    }
  }
}
