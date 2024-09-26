namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Normalize all sequences of characters satisfying the predicate throughout the string. Normalizing means removing leading/trailing and replacing certain consecutive characters with a single specified character.</summary>
    /// <example>"".NormalizeAll(' ', char.IsWhiteSpace);</example>
    /// <example>"".NormalizeAll(' ', c => c == ' ');</example>
    public static System.Text.StringBuilder NormalizeAll(this System.Text.StringBuilder source, char replacement, System.Func<char, bool> predicate)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentNullException.ThrowIfNull(predicate);

      var normalizedIndex = 0;

      var isPrevious = true;

      for (var sourceIndex = 0; sourceIndex < source.Length; sourceIndex++)
      {
        var character = source[sourceIndex];

        var isCurrent = predicate(character);

        if (!(isPrevious && isCurrent))
        {
          source[normalizedIndex++] = isCurrent ? replacement : character;

          isPrevious = isCurrent;
        }
      }

      if (isPrevious) normalizedIndex--;

      return normalizedIndex == source.Length ? source : source.Remove(normalizedIndex, source.Length - normalizedIndex);
    }
    /// <summary>Normalize all sequences of the specified characters throughout the string. Normalizing means removing leading/trailing and replacing sequences of specified characters with a single specified character.</summary>
    public static System.Text.StringBuilder NormalizeAll(this System.Text.StringBuilder source, char replacement, System.Collections.Generic.IEqualityComparer<char>? equalityComparer, params char[] characters)
      => source.NormalizeAll(replacement, t => characters.Contains(t, equalityComparer));
    /// <summary>Normalize all sequences of the specified characters throughout the string. Normalizing means removing leading/trailing and replacing sequences of specified characters with a single specified character.</summary>
    public static System.Text.StringBuilder NormalizeAll(this System.Text.StringBuilder source, char replacement, params char[] characters)
      => source.NormalizeAll(replacement, characters.Contains);
  }
}
