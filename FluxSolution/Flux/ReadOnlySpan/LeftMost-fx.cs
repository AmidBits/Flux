namespace Flux
{
  public static partial class ReadOnlySpans
  {
    /// <summary>
    /// <para>Slice the <paramref name="source"/> keeping the left-most <paramref name="count"/> of elements if available, otherwise as many as there are.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public static System.ReadOnlySpan<T> LeftMost<T>(this System.ReadOnlySpan<T> source, int count)
      => source[..int.Min(source.Length, count)];
  }
}
