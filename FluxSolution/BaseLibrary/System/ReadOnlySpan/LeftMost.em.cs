namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns a new <see cref="System.ReadOnlySpan{T}"/> containing the left-most <paramref name="count"/> of elements if available, otherwise as many as there are.</para>
    /// </summary>
    public static System.ReadOnlySpan<T> LeftMost<T>(this System.ReadOnlySpan<T> source, int count)
      => source[..System.Math.Min(source.Length, count)];
  }
}
