namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Inserts a space in front of any single upper case character, except the first one in the string.</summary>
    public static SpanMaker<char> PrefixCapWords(this SpanMaker<char> source, char prefix = ' ')
    {
      var maxIndex = source.Length - 1;

      for (var index = maxIndex; index >= 0; index--)
      {
        if (index == 0 || !char.IsUpper(source[index])) continue; // If, on first or c is not upper-case, then advance.

        if (!char.IsLower(source[index - 1]) && (index < maxIndex) && !char.IsLower(source[index + 1])) continue; // If, (above ensured previous) previous is not lower-case and (ensure next) next is not lower-case, then advance.

        source = source.Insert(index, 1, prefix);
      }

      return source;
    }
  }
}
