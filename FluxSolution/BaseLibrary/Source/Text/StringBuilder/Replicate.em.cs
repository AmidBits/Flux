using System.Linq;

namespace Flux
{
  public static partial class XtendStringBuilder
  {
    /// <summary>Returns the string builder with the specified characters replicated by the specified count throughout.</summary>
    public static System.Text.StringBuilder Replicate(this System.Text.StringBuilder source, int count, params char[] characters)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      for (var index = 0; index < source.Length; index++)
      {
        if (characters.Contains(source[index]))
        {
          source.Insert(index, source[index].ToString(), count);

          index += count;
        }
      }

      return source;
    }
    /// <summary>Returns the string builder with the specified characters doubled throughout.</summary>
    public static System.Text.StringBuilder Geminate(this System.Text.StringBuilder source, params char[] characters)
      => Replicate(source, 1, characters);
  }
}
