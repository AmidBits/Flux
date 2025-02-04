namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Creates a new sequence of <paramref name="count"/> elements taken from <paramref name="source"/>. Uses the specified <paramref name="rng"/> (shared if null).</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="count"></param>
    /// <param name="rng"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<T> GetRandomElements<T>(this System.Collections.Generic.IList<T> source, int count, System.Random? rng = null)
    {
      while (--count >= 0)
        yield return source.Random(rng);
    }
  }
}
