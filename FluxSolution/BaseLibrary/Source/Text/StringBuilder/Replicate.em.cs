using System.Linq;

namespace Flux
{
  public static partial class StringBuilderEm
  {
    /// <summary>Replicates the string a specified number of times.</summary>
    public static System.Text.StringBuilder Replicate(this System.Text.StringBuilder source, int count)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      else if (source.Length == 0) throw new System.ArgumentOutOfRangeException(nameof(source));

      var original = source.ToString();

      while (count-- > 0)
        source.Append(original);

      return source;
    }

    /// <summary>Returns the string builder with the specified characters replicated by the specified count throughout. If no characters are specified, all characters are replicated. If the string builder is empty, nothing is replicated.</summary>
    public static System.Text.StringBuilder Replicate(this System.Text.StringBuilder source, int count, params char[] characters)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      for (var index = 0; index < source.Length; index++)
      {
        var sourceChar = source[index];

        if (characters.Length == 0 || characters.Contains(sourceChar))
        {
          source.Insert(index, sourceChar.ToString(), count);

          index += count;
        }
      }

      return source;
    }
    /// <summary>Returns the string builder with the specified characters doubled throughout. If no characters are specified, all characters are doubled.</summary>
    public static System.Text.StringBuilder Geminate(this System.Text.StringBuilder source, params char[] characters)
      => Replicate(source, 1, characters);
  }
}
