namespace Flux
{
  public static partial class SpanBuilderExtensionMethods
  {
    /// <summary>Repeats the values in the builder <paramref name="count"/> times.</summary>
    public static SpanBuilder<T> Repeat<T>(this SpanBuilder<T> source, int count) => source.Append(new SpanBuilder<T>(source.AsReadOnlySpan(), count).AsSpan(), 1);
  }
}
