namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static SequenceBuilder<char> RemoveNonLanguageLetters(this SequenceBuilder<char> source, System.Globalization.CultureInfo? culture = null)
    {
      culture ??= System.Globalization.CultureInfo.CurrentCulture;

      for (var index = source.Length; index >= 0; index--)
        if (!culture.IsLetterOf(source[index]))
          source.Remove(index, 1);

      return source;
    }

    public static SequenceBuilder<System.Text.Rune> RemoveNonLanguageLetters(this SequenceBuilder<System.Text.Rune> source, System.Globalization.CultureInfo? culture = null)
    {
      culture ??= System.Globalization.CultureInfo.CurrentCulture;

      for (var index = source.Length; index >= 0; index--)
        if (!culture.IsLetterOf(source[index]))
          source.Remove(index, 1);

      return source;
    }

    public static System.Text.StringBuilder RemoveNonLanguageLetters(this System.Text.StringBuilder source, System.Globalization.CultureInfo? culture = null)
    {
      culture ??= System.Globalization.CultureInfo.CurrentCulture;

      for (var index = source.Length; index >= 0; index--)
        if (!culture.IsLetterOf(source[index]))
          source.Remove(index, 1);

      return source;
    }
  }
}
