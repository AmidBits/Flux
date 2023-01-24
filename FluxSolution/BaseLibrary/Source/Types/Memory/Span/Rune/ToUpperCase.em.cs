namespace Flux
{
  public static partial class ExtensionMethodsSpan
  {
    /// <summary>Convert all characters, in the specified range, to upper case. Uses the specified culture, or the current culture if null.</summary>
    public static void ToUpperCase(this System.Span<System.Text.Rune> source, int startIndex, int length, System.Globalization.CultureInfo? culture = null)
    {
      culture ??= System.Globalization.CultureInfo.CurrentCulture;

      for (var index = startIndex + length - 1; index >= startIndex; index--)
      {
        var sourceChar = source[index];
        var targetChar = System.Text.Rune.ToUpper(sourceChar, culture);

        if (sourceChar != targetChar) source[index] = targetChar;
      }
    }
  }
}
