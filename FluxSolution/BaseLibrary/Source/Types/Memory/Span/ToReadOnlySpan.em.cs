namespace Flux
{
  public static partial class SystemSpanEm
  {
    /// <summary>Creates a new ReadOnlySpan from the source.</summary>
    public static System.ReadOnlySpan<T> ToReadOnlySpan<T>(this System.Span<T> source)
      => source.ToArray();
  }
}
