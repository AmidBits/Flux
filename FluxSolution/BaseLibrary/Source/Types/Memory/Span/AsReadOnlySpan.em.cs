namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Flux non-allocating conversion (casting) to <see cref="System.ReadOnlySpan{T}"/>.</summary>
    public static System.ReadOnlySpan<T> AsReadOnlySpan<T>(this System.Span<T> source)
      => source;
  }
}
