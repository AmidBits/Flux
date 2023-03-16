namespace Flux
{
  public static partial class ExtensionMethodsCollections
  {
    /// <summary>Flux non-allocating conversion (casting) to <see cref="System.Span{T}"/>.</summary>
    public static System.Span<T> AsSpan<T>(this System.Collections.Generic.List<T> source) => System.Runtime.InteropServices.CollectionsMarshal.AsSpan(source);
  }
}
