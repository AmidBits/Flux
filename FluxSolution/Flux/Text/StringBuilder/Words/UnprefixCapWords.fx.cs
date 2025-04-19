namespace Flux
{
  public static partial class StringBuilders
  {
    /// <summary>Join CamelCase of words separated by the specified predicate. The first character</summary>
    public static System.Text.StringBuilder UnprefixCapWords(this System.Text.StringBuilder source, char prefix = ' ')
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var maxIndex = source.Length - 1;

      for (var index = maxIndex; index > 0; index--)
      {
        if (source[index] != prefix) continue; // If, c is not prefix, then advance.

        if ((index < maxIndex) && !char.IsUpper(source[index + 1])) continue; // If, (ensure next) next is not upper-case, then advance.

        source.Remove(index, 1);
      }

      return source;
    }
  }
}
