namespace Flux
{
  public static partial class TextElementEnumerators
  {
    public static System.Collections.Generic.IEnumerable<Text.TextElement> EnumerateTextElements(this System.IO.TextReader source) => new Text.TextElementEnumerator(source);
  }
}
