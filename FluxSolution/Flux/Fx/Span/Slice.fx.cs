namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Creates a new span sliced using <paramref name="slice"/>.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="slice"></param>
    /// <returns></returns>
    public static System.Span<T> Slice<T>(this System.Span<T> source, Slice slice)
      => source.Slice(slice.Index, slice.Length);
  }
}
