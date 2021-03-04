namespace Flux
{
  public static partial class SystemTextStringBuilderEm
  {
    /// <summary>Normalize the specified (or all if none specified) consecutive characters in the string. Uses the specfied comparer.</summary>
    public static System.Text.StringBuilder NormalizeAdjacent(this System.Text.StringBuilder source, int ofLength, System.Collections.Generic.IEqualityComparer<char> comparer, System.ReadOnlySpan<char> characters)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

      var index = 0;
      var previous = '\0';
      var adjacentLength = 0;

      for (var indexOfSource = 0; indexOfSource < source.Length; indexOfSource++)
      {
        var current = source[indexOfSource];

        if ((!comparer.Equals(current, previous) || (characters.Length > 0 && characters.IndexOf(current, comparer) == -1)) && adjacentLength <= ofLength)
        {
          source[index++] = current;

          previous = current;

          adjacentLength = 0;
        }
        else adjacentLength++;
      }

      return source.Remove(index, source.Length - index);
    }
    /// <summary>Normalize the specified (or all if none specified) consecutive characters in the string. Uses the default comparer.</summary>
    public static System.Text.StringBuilder NormalizeAdjacent(this System.Text.StringBuilder source, int ofLength, params char[] characters)
      => NormalizeAdjacent(source, ofLength, System.Collections.Generic.EqualityComparer<char>.Default, characters);
  }
}
