namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Normalize the specified (or all if none specified) consecutive characters in the string. Uses the specfied comparer.</summary>
    public static System.Text.StringBuilder NormalizeAdjacent(this System.Text.StringBuilder source, int maxAdjacentLength, System.Collections.Generic.IEqualityComparer<char> equalityComparer, System.ReadOnlySpan<char> characters)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (maxAdjacentLength < 1) throw new System.ArgumentNullException(nameof(maxAdjacentLength));
      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

      var index = 0;
      var previous = '\0';
      var adjacentLength = 1;

      for (var indexOfSource = 0; indexOfSource < source.Length; indexOfSource++)
      {
        var current = source[indexOfSource];

        var isEqual = characters.Length > 0 // Use list or just characters?
          ? (characters.IndexOf(current, equalityComparer) > -1 && characters.IndexOf(previous, equalityComparer) > -1) // Is both current and previous in characters?
          : equalityComparer.Equals(current, previous); // Is current and previous character equal?

        if (!isEqual || adjacentLength < maxAdjacentLength)
        {
          source[index++] = current;

          previous = current;
        }

        adjacentLength = !isEqual ? 1 : adjacentLength + 1;
      }

      return source.Remove(index, source.Length - index);
    }
    /// <summary>Normalize the specified (or all if none specified) consecutive characters in the string. Uses the default comparer.</summary>
    public static System.Text.StringBuilder NormalizeAdjacent(this System.Text.StringBuilder source, int maxAdjacentLength, params char[] characters)
      => NormalizeAdjacent(source, maxAdjacentLength, System.Collections.Generic.EqualityComparer<char>.Default, characters);
  }
}
