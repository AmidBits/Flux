namespace Flux
{
  public static partial class ExtensionMethodsSequenceBuilder
  {
    /// <summary>Create a new char array with all diacritical (latin) strokes, which are not covered by the normalization forms in NET, replaced. Can be done simplistically because the diacritical latin stroke characters (and replacements) all fit in a single char.</summary>
    public static void ReplaceDiacriticalLatinStrokes(this SequenceBuilder<char> source)
    {
      for (var index = source.Length - 1; index >= 0; index--)
        source[index] = (char)((System.Text.Rune)source[index]).ReplaceDiacriticalLatinStroke().Value;
    }
  }
}
