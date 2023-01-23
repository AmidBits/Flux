namespace Flux
{
  public static partial class ExtensionMethodsSequenceBuilder
  {
    /// <summary>Convert all characters, in the specified range, to lower case. Uses the specified culture, or the current culture if null.</summary>
    public static SequenceBuilder<System.Text.Rune> ToLowerCase(this SequenceBuilder<System.Text.Rune> source, int startIndex, int length, System.Globalization.CultureInfo? culture = null)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      culture ??= System.Globalization.CultureInfo.CurrentCulture;

      for (var index = startIndex + length - 1; index >= startIndex; index--)
      {
        var sourceChar = source[index];
        var targetChar = System.Text.Rune.ToLower(sourceChar, culture);

        if (sourceChar != targetChar) source[index] = targetChar;
      }

      return source;
    }
  }
}
