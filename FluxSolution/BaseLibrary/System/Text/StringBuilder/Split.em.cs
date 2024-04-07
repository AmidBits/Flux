namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Creates a sequence of substrings, as a split of the StringBuilder content based on the characters in an array. There is no change to the StringBuilder content.</summary>
    public static System.Collections.Generic.List<string> Split(this System.Text.StringBuilder source, System.StringSplitOptions options, System.ReadOnlySpan<char> separators)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var list = new System.Collections.Generic.List<string>();

      var startIndex = 0;

      var sourceLength = source.Length;

      for (var index = startIndex; index < sourceLength; index++)
      {
        if (separators.Contains(source[index]))
        {
          if (index != startIndex || options != System.StringSplitOptions.RemoveEmptyEntries)
            list.Add(source.ToString(startIndex, index - startIndex));

          startIndex = index + 1;
        }
      }

      if (startIndex < sourceLength)
        list.Add(source.ToString(startIndex, sourceLength - startIndex));

      return list;
    }
  }
}
