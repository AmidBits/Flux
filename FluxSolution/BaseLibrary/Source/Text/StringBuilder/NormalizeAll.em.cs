using System.Linq;

namespace Flux
{
  public static partial class XtensionsStringBuilder
  {
    /// <summary>Normalize all sequences of characters satisfying the predicate throughout the string. Normalizing means removing leading/trailing and replacing certain consecutive characters with a single specified character.</summary>
    /// <example>"".NormalizeAll(' ', char.IsWhiteSpace);</example>
    /// <example>"".NormalizeAll(' ', c => c == ' ');</example>
    public static System.Text.StringBuilder NormalizeAll(this System.Text.StringBuilder source, char replacement, System.Func<char, bool> predicate)
    {
      var targetIndex = 0;

      var previous = true;

      for (var sourceIndex = 0; sourceIndex < source.Length; sourceIndex++)
      {
        if (source[sourceIndex] is var c && predicate(c) is var current && (!previous || !current))
        {
          source[targetIndex++] = current ? replacement : c;

          previous = current;
        }
      }

      if (previous) targetIndex--;

      if (targetIndex == source.Length) return source;
      else return source.Remove(targetIndex, source.Length - targetIndex);
    }
    /// <summary>Normalize all sequences of the specified characters throughout the string. Normalizing means removing leading/trailing and replacing sequences of specified characters with a single specified character.</summary>
    public static System.Text.StringBuilder NormalizeAll(this System.Text.StringBuilder source, char with, params char[] characters)
      => source.NormalizeAll(with, characters.Contains);
  }
}
