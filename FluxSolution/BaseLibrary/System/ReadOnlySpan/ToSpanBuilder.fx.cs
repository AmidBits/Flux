namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Creates a new <see cref="SpanBuilder{T}"/> with all elements from <paramref name="source"/>.</para>
    /// </summary>
    public static SpanBuilder<T> ToSpanBuilder<T>(this System.ReadOnlySpan<T> source)
      => new(source);
  }
}
