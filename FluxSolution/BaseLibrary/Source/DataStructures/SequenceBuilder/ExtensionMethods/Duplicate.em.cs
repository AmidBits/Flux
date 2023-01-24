namespace Flux
{
  public static partial class ExtensionMethodsSequenceBuilder
  {
    /// <summary>Returns the <paramref name="source"/> with the specified <paramref name="values"/> duplicated by the specified <paramref name="count"/> throughout. If no values are specified, all characters are replicated. If the string builder is empty, nothing is replicated. Uses the specified <paramref name="equalityComparer"/>, or default if null.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static SequenceBuilder<T> Duplicate<T>(this SequenceBuilder<T> source, System.ReadOnlySpan<T> values, int count, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      for (var index = 0; index < source.Length; index++)
      {
        var sourceValue = source[index];

        if (values.Length == 0 || values.IndexOf(sourceValue, equalityComparer ?? System.Collections.Generic.EqualityComparer<T>.Default) > -1)
        {
          source.Insert(index, sourceValue, count);

          index += count;
        }
      }

      return source;
    }
  }
}
