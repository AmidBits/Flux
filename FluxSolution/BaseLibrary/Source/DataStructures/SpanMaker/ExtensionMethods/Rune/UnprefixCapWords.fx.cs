namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Join CamelCase of words separated by the specified predicate. The first character</summary>
    public static SpanMaker<System.Text.Rune> UnprefixCapWords(this SpanMaker<System.Text.Rune> source, System.Text.Rune prefix)
    {
      var maxIndex = source.Length - 1;

      for (var index = maxIndex; index > 0; index--)
      {
        if (source[index] != prefix) continue; // If, c is not prefix, then advance.

        if ((index < maxIndex) && !System.Text.Rune.IsUpper(source[index + 1])) continue; // If, (ensure next) next is not upper-case, then advance.

        source = source.Remove(index, 1);
      }

      return source;
    }
  }
}
