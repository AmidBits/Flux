using System.Linq;

namespace Flux
{
  public static partial class XtensionsStringBuilder
  {
    /// <summary>Returns a new string with the specified characters doubled in the string.</summary>
    public static System.Text.StringBuilder Geminate(this System.Text.StringBuilder source, params char[] character)
    {
      for (var index = 0; index < source.Length; index++)
      {
        if (character.Contains(source[index]))
        {
          source.Insert(index, source[index]);

          index++;
        }
      }

      return source;
    }

    public static System.Text.StringBuilder Geminate(this System.Text.StringBuilder source, int count, params char[] character)
    {
      for (var index = 0; index < source.Length; index++)
      {
        if (character.Contains(source[index]))
        {
          source.Insert(index, source[index].ToString(), count);

          index++;
        }
      }

      return source;
    }
  }
}
