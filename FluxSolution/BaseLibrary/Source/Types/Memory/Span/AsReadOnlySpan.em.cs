namespace Flux
{
  public static partial class ExtensionMethodsSpan
  {
    /// <summary>Flux non-allocating conversion (casting) from <typeparamref name="T"/>[] to <see cref="System.ReadOnlySpan{T}"/>.</summary>
    public static System.ReadOnlySpan<T> AsReadOnlySpan<T>(this T[] source)
      => source.AsSpan();

    /// <summary>Flux non-allocating conversion (casting) from <see cref="System.Span{T}"/> to <see cref="System.ReadOnlySpan{T}"/>.</summary>
    public static System.ReadOnlySpan<T> AsReadOnlySpan<T>(this System.Span<T> source)
      => source;
  }
}
