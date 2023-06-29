namespace Flux
{
  public static partial class SpanBuilderExtensionMethods
  {

    /// <summary>Returns the <paramref name="source"/> with the specified <paramref name="values"/> duplicated by the specified <paramref name="count"/> throughout. If no values are specified, all characters are replicated. If the string builder is empty, nothing is replicated. Uses the specified <paramref name="equalityComparer"/>, or default if null.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static SpanBuilder<T> Duplicate<T>(this SpanBuilder<T> source, System.ReadOnlySpan<T> values, int count, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      for (var index = 0; index < source.Count; index++)
      {
        var sourceValue = source[index];

        if (values.Length == 0 || values.IndexOf(sourceValue, equalityComparer) > -1)
        {
          source.Insert(index, sourceValue, count);

          index += count;
        }
      }

      return source;
    }
  }
}
