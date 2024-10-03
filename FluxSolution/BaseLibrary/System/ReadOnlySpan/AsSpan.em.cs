namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Non-allocating casting from <see cref="System.ReadOnlySpan{T}"/> to <see cref="System.Span{T}"/>.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static System.Span<T> AsSpan<T>(this System.ReadOnlySpan<T> source)
      => System.Runtime.InteropServices.MemoryMarshal.CreateSpan(ref System.Runtime.InteropServices.MemoryMarshal.GetReference(source), source.Length);
  }
}
