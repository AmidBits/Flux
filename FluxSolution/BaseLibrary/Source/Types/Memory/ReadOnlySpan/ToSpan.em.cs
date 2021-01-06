namespace Flux
{
  public static partial class SystemReadOnlySpanEm
  {
    /// <summary>Creates a new <see cref="System.Span<typeparamref name="T"/>"/> from the source.</summary>
    public static System.Span<T> ToSpan<T>(this System.ReadOnlySpan<T> source)
      => source.ToArray();
  }
}
