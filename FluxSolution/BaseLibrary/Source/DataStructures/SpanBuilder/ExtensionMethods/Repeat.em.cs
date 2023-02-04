namespace Flux
{
  public static partial class ExtensionMethodsSpanBuilder
  {
    /// <summary>Repeats the values in the builder <paramref name="count"/> times.</summary>
    public static SpanBuilder<T> Repeat<T>(this SpanBuilder<T> source, int count)
    {
      source.Append(new SpanBuilder<T>().Append(source.AsReadOnlySpan(), count).AsSpan());

      return source;
    }
  }
}
