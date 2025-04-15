namespace Flux
{
  public static partial class ReadOnlySpans
  {
    /// <summary>
    /// <para>Returns a random element from the <paramref name="source"/>. Uses the specified <paramref name="rng"/>, or <see cref="System.Random.Shared"/> if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="rng"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"/>
    public static T Random<T>(this System.ReadOnlySpan<T> source, System.Random? rng = null)
      => source[(rng ?? System.Random.Shared).Next(source.Length)];

    /// <summary>
    /// <para>Attempts to fetch a random element from the <paramref name="source"/> into <paramref name="result"/> and returns whether successful. Uses the specified <paramref name="rng"/>, or <see cref="System.Random.Shared"/> if null.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="result"></param>
    /// <param name="rng"></param>
    /// <returns></returns>
    public static bool TryRandom<T>(this System.ReadOnlySpan<T> source, out T result, System.Random? rng = null)
    {
      try
      {
        result = source.Random(rng);
        return true;
      }
      catch { }

      result = default!;
      return false;
    }
  }
}
