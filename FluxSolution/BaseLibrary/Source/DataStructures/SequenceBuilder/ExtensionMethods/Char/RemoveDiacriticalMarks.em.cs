namespace Flux
{
  public static partial class ExtensionMethodsSequenceBuilder
  {
    /// <summary>Remove diacritical marks and any optional replacements desired.</summary>
    public static SequenceBuilder<char> RemoveDiacriticalMarks(this SequenceBuilder<char> source, System.Func<char, char>? additionalCharacterReplacements = null)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      additionalCharacterReplacements ??= (c) => c;

      var sb = new SequenceBuilder<char>();

      foreach (var character in source.ToString().Normalize(System.Text.NormalizationForm.FormKD))
      {
        switch (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(character))
        {
          case System.Globalization.UnicodeCategory.NonSpacingMark:
          case System.Globalization.UnicodeCategory.SpacingCombiningMark:
          case System.Globalization.UnicodeCategory.EnclosingMark:
            break;
          default:
            sb.Append(additionalCharacterReplacements(character));
            break;
        }
      }

      return source.Clear().Append(sb.AsReadOnlySpan());
    }
  }
}
