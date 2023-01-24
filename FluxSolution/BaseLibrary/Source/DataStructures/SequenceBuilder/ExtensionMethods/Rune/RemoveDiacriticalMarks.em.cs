namespace Flux
{
  public static partial class ExtensionMethodsSequenceBuilder
  {
    /// <summary>Remove diacritical marks and any optional replacements desired.</summary>
    public static SequenceBuilder<System.Text.Rune> RemoveDiacriticalMarks(this SequenceBuilder<System.Text.Rune> source, System.Func<char, char>? additionalCharacterReplacements = null)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      additionalCharacterReplacements ??= (c) => c;

      var sb = new SequenceBuilder<System.Text.Rune>();

      foreach (var character in source.ToString().Normalize(System.Text.NormalizationForm.FormKD))
      {
        switch (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(character))
        {
          case System.Globalization.UnicodeCategory.NonSpacingMark:
          case System.Globalization.UnicodeCategory.SpacingCombiningMark:
          case System.Globalization.UnicodeCategory.EnclosingMark:
            break;
          default:
            sb.Append((System.Text.Rune)additionalCharacterReplacements(character));
            break;
        }
      }

      return source.Clear().Append(sb);
    }
  }
}
