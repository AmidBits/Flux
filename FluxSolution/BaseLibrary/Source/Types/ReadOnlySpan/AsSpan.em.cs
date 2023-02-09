namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    /// <summary>Non-allocating casting from <see cref="System.ReadOnlySpan{T}"/> to <see cref="System.Span{T}"/>.</summary>
    public static System.Span<T> AsSpan<T>(this System.ReadOnlySpan<T> source)
      => System.Runtime.InteropServices.MemoryMarshal.CreateSpan(ref System.Runtime.InteropServices.MemoryMarshal.GetReference(source), source.Length);
  }
}
