namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Flux non-allocating conversion (casting) to <see cref="System.Span{T}"/>.</summary>
    public static System.ReadOnlySpan<T> AsReadOnlySpan<T>(this System.Collections.Generic.List<T> source)
      => System.Runtime.InteropServices.CollectionsMarshal.AsSpan(source);
  }
}
