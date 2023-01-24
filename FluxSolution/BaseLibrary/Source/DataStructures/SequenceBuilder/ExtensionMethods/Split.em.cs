namespace Flux
{
  public static partial class ExtensionMethodsSequenceBuilder
  {
    /// <summary>Creates a sequence of substrings, as a split of the StringBuilder content based on the characters in an array. There is no change to the StringBuilder content.</summary>
    public static System.Collections.Generic.IEnumerable<SequenceBuilder<T>> Split<T>(this SequenceBuilder<T> source, System.StringSplitOptions options, System.Collections.Generic.IList<T> separators, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var startIndex = 0;

      var sourceLength = source.Length;

      for (var index = startIndex; index < sourceLength; index++)
      {
        if (separators.Any(c => equalityComparer.Equals(c, source[index])))
        {
          if (index != startIndex || options != System.StringSplitOptions.RemoveEmptyEntries)
            yield return source.AsReadOnlySpan().Slice(startIndex, index - startIndex).ToSequenceBuilder();

          startIndex = index + 1;
        }
      }

      if (startIndex < sourceLength)
        yield return source.AsReadOnlySpan().Slice(startIndex, sourceLength - startIndex).ToSequenceBuilder();
    }
  }
}
