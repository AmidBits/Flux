namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns a new <see cref="System.ReadOnlySpan{T}"/> containing the right-most <paramref name="count"/> of elements if available, otherwise as many as there are.</para>
    /// </summary>
    public static System.ReadOnlySpan<T> RightMost<T>(this System.ReadOnlySpan<T> source, int count)
      => source[int.Max(0, source.Length - count)..];
  }
}
