namespace Flux
{
  public static partial class ExtensionMethodsSpan
  {
    /// <summary>Non-allocating conversion (casting) from <see cref="System.Span{T}"/> to <see cref="System.ReadOnlySpan{T}"/>.</summary>
    public static System.ReadOnlySpan<T> AsReadOnlySpan<T>(this System.Span<T> source)
      => source;
  }
}
