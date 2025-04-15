namespace Flux
{
  public static partial class Arrays
  {
    /// <summary>Non-allocating conversion (casting) from <paramref name="source"/> T[] to <see cref="System.ReadOnlySpan{T}"/>.</summary>
    public static System.ReadOnlySpan<T> AsReadOnlySpan<T>(this T[] source) => source;

    /// <summary>Non-allocating conversion (casting) from <paramref name="source"/> T[] to <see cref="System.ReadOnlySpan{T}"/>.</summary>
    public static System.ReadOnlySpan<T> AsReadOnlySpan<T>(this T[] source, int start) => new(source, start, source.Length - start);

    /// <summary>Non-allocating conversion (casting) from <paramref name="source"/> T[] to <see cref="System.ReadOnlySpan{T}"/>.</summary>
    public static System.ReadOnlySpan<T> AsReadOnlySpan<T>(this T[] source, int start, int length) => new(source, start, length);
  }
}
