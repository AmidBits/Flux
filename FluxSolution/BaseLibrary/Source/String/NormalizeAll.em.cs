using System.Linq;

namespace Flux
{
  public static partial class XtendString
  {
    /// <summary>Normalize all sequences of characters satisfying the predicate throughout the string. Normalizing means removing leading/trailing and replacing certain consecutive characters with a single specified character.</summary>
    /// <example>"".NormalizeAll(' ', char.IsWhiteSpace);</example>
    /// <example>"".NormalizeAll(' ', c => c == ' ');</example>
    public static string NormalizeAll(this string source, char with, System.Func<char, bool> predicate)
    {
      var buffer = source.ToCharArray();

      var index = 0;
      var previous = true;

      foreach (var c in buffer)
      {
        if (predicate(c) is var current && (!previous || !current))
        {
          buffer[index++] = current ? with : c;

          previous = current;
        }
      }

      if (previous && index > 0) index--;

      return new string(buffer, 0, index);
    }
    /// <summary>Normalize all sequences of the specified characters throughout the string. Normalizing means removing leading/trailing and replacing sequences of specified characters with a single specified character.</summary>
    public static string NormalizeAll(this string source, char with, params char[] characters)
      => source.NormalizeAll(with, characters.Contains);
  }
}
