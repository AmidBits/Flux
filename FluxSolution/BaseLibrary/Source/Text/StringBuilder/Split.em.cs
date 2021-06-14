using System.Linq;

namespace Flux
{
  public static partial class SystemTextStringBuilderEm
  {
    /// <summary>Creates a sequence of substrings, as a split of the StringBuilder content based on the characters in an array. There is no change to the StringBuilder content.</summary>
    public static System.Collections.Generic.IEnumerable<string> Split(this System.Text.StringBuilder source, System.StringSplitOptions options, System.Collections.Generic.IList<char> separators, System.Collections.Generic.IEqualityComparer<char> comparer)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var startIndex = 0;

      var sourceLength = source.Length;

      for (var index = startIndex; index < sourceLength; index++)
      {
        if (separators.Any(c => comparer.Equals(c, source[index])))
        {
          if (index != startIndex || options != System.StringSplitOptions.RemoveEmptyEntries) yield return source.ToString(startIndex, index - startIndex);

          startIndex = index + 1;
        }
      }

      if (startIndex < sourceLength)
        yield return source.ToString(startIndex, sourceLength - startIndex);
    }
    /// <summary>Creates a sequence of substrings, as a split of the StringBuilder content based on the characters in an array. There is no change to the StringBuilder content. Uses the default comparer.</summary>
    public static System.Collections.Generic.IEnumerable<string> Split(this System.Text.StringBuilder source, System.StringSplitOptions options, System.Collections.Generic.IList<char> separators)
      => Split(source, options, separators, System.Collections.Generic.EqualityComparer<char>.Default);
  }
}
