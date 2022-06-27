//namespace Flux
//{
//  public static partial class ExtensionMethods
//  {
//    /// <summary>Create a new char array with all diacritical (latin) strokes, which are not covered by the normalization forms in NET, replaced. Can be done simplistically because the diacritical latin stroke characters (and replacements) all fit in a single char.</summary>
//    public static char[] ReplaceDiacriticalLatinStrokes(this System.ReadOnlySpan<char> source)
//    {
//      var target = new char[source.Length];

//      for (var index = source.Length - 1; index >= 0; index--)
//        target[index] = (char)((System.Text.Rune)source[index]).ReplaceDiacriticalLatinStroke().Value;

//      return target;
//    }
//  }
//}

