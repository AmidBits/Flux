namespace Flux
{
  public static partial class SpanEm
  {
    /// <summary>Creates a new ReadOnlySpan from the source.</summary>
    public static System.ReadOnlySpan<T> ToReadOnlySpan<T>(this System.Span<T> source)
      => source.ToArray();
  }
}
