namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Normalize the specified (or all if none specified) consecutive <paramref name="predicate"/> to <paramref name="maxAdjacentLength"/> in the <paramref name="source"/>.</summary>
    public static System.Text.StringBuilder NormalizeAdjacent(this System.Text.StringBuilder source, int maxAdjacentLength, System.Func<char, bool> predicate)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentNullException.ThrowIfNull(predicate);

      if (maxAdjacentLength < 1) throw new System.ArgumentOutOfRangeException(nameof(maxAdjacentLength));

      var updatedIndex = 0;
      var previousIsTarget = false;
      var adjacentLength = 1;

      for (var index = 0; index < source.Length; index++)
      {
        var current = source[index];

        var currentIsTarget = predicate(current);

        var nonAdjacent = !(currentIsTarget && previousIsTarget);

        if (nonAdjacent || adjacentLength < maxAdjacentLength)
        {
          source[updatedIndex++] = current;

          previousIsTarget = currentIsTarget;
        }

        if (nonAdjacent) adjacentLength = 1;
        else adjacentLength++;
      }

      return source.Remove(updatedIndex, source.Length - updatedIndex);
    }

    /// <summary>Normalize the specified (or all if none specified) consecutive <paramref name="characters"/> to <paramref name="maxAdjacentLength"/> in the <paramref name="source"/>.</summary>
    public static System.Text.StringBuilder NormalizeAdjacent(this System.Text.StringBuilder source, int maxAdjacentLength, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, params char[] characters)
      => NormalizeAdjacent(source, maxAdjacentLength, c => characters is null || characters.Length == 0 || characters.Contains(c, equalityComparer));
  }
}
