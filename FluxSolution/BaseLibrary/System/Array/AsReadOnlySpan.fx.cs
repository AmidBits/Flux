namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Non-allocating conversion (casting) from <paramref name="source"/> T[] to <see cref="System.ReadOnlySpan{T}"/>.</summary>
    public static System.ReadOnlySpan<T> AsReadOnlySpan<T>(this T[] source) => source;
  }
}
