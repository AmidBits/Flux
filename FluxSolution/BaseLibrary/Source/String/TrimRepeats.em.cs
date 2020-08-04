using System.Linq;

namespace Flux
{
  public static partial class XtensionsString
  {
    /// <summary>Trim all repeated characters in the string.</summary>
    public static string TrimRepeats(this string source)
    {
      var buffer = source.ToCharArray();

      var index = 0;
      var previous = '\0';

      foreach (var current in buffer)
      {
        if (current != previous)
        {
          buffer[index++] = current;

          previous = current;
        }
      }

      return new string(buffer, 0, index > 0 && (index - 1) is var indexM1 && buffer[indexM1] == ' ' ? indexM1 : index);
    }

    /// <summary>Trim all repeated characters in the string.</summary>
    public static string TrimRepeats(this string source, params char[] characters)
    {
      var buffer = source.ToCharArray();

      var index = 0;
      var previous = '\0';

      foreach (var current in buffer)
      {
        if (current != previous || !characters.Contains(current))
        {
          buffer[index++] = current;

          previous = current;
        }
      }

      return new string(buffer, 0, index > 0 && (index - 1) is var indexM1 && buffer[indexM1] == ' ' ? indexM1 : index);
    }
  }
}
