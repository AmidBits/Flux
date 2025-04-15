namespace Flux
{
  public static partial class ReadOnlySpans
  {
    /// <summary>
    /// <para>Creates a new <see cref="SpanMaker{T}"/> with all elements from <paramref name="source"/>.</para>
    /// </summary>
    public static SpanMaker<T> ToSpanMaker<T>(this System.ReadOnlySpan<T> source)
      => new(source);
  }
}
