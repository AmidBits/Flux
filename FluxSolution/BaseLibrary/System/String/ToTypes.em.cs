namespace Flux
{
  public static partial class Fx
  {
    public static SpanBuilder<T> ToSpanBuilder<T>(this System.ReadOnlySpan<T> source)
      => new(source, 1);
  }
}
