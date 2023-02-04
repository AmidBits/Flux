namespace Flux
{
  public static partial class ExtensionMethodsSpanBuilder
  {
    /// <summary>Replace <paramref name="key"/> with <paramref name="value"/> if it exists at <paramref name="startAt"/> in <paramref name="source"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</summary>
    /// <exception cref="System.ArgumentNullException"></exception>
    public static SpanBuilder<T> ReplaceIfEqualAt<T>(this SpanBuilder<T> source, int startAt, System.ReadOnlySpan<T> key, System.ReadOnlySpan<T> value, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      if (source.AsReadOnlySpan().EqualsAt(startAt, key, 0, key.Length, equalityComparer))
      {
        source.Remove(startAt, key.Length);
        source.Insert(startAt, value);
      }

      return source;
    }
  }
}
