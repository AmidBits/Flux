namespace Flux
{
  public static partial class XtensionsStringBuilder
  {
    /// <summary>Normalize the specified (or all if none specified) consecutive characters in the string. Uses the specfied comparer.</summary>
    public static System.Text.StringBuilder NormalizeAdjacent(this System.Text.StringBuilder source, System.Collections.Generic.IEqualityComparer<char> comparer, params char[] characters)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      comparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var index = 0;
      var previous = '\0';

      for (var indexOfSource = 0; indexOfSource < source.Length; indexOfSource++)
      {
        var current = source[indexOfSource];

        if (!comparer.Equals(current, previous) || (characters.Length > 0 && !System.Array.Exists(characters, character => comparer.Equals(character, current))))
        {
          source[index++] = current;

          previous = current;
        }
      }

      return source.Remove(index, source.Length - index);
    }
    /// <summary>Normalize the specified (or all if none specified) consecutive characters in the string. Uses the default comparer.</summary>
    public static System.Text.StringBuilder NormalizeAdjacent(this System.Text.StringBuilder source, params char[] characters)
      => NormalizeAdjacent(source, System.Collections.Generic.EqualityComparer<char>.Default, characters);
  }
}
