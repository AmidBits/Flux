namespace Flux
{
  public static partial class ReadOnlySpans
  {
    /// <summary>
    /// <para>Slice the <paramref name="source"/> keeping the right-most <paramref name="count"/> of elements if available, otherwise as many as there are.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public static System.ReadOnlySpan<T> RightMost<T>(this System.ReadOnlySpan<T> source, int count)
      => source[int.Max(0, source.Length - count)..];
  }
}
