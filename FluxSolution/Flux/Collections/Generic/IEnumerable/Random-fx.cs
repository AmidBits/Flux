namespace Flux
{
  public static partial class IEnumerables
  {
    /// <summary>
    /// <para>Returns a random element from <paramref name="source"/>. Uses the specified <paramref name="rng"/> (shared if null).</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="rng"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentNullException"></exception>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static T Random<T>(this System.Collections.Generic.IEnumerable<T> source, System.Random? rng = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var (item, index) = source.RandomOrValue(default!, rng);

      if (index > -1)
        return item;

      throw new System.ArgumentOutOfRangeException(nameof(source));
    }

    /// <summary>
    /// <para>Attempts to fetch a random element from <paramref name="source"/> into <paramref name="result"/> and indicates whether successful. Uses the specified <paramref name="rng"/> (shared if null).</para>
    /// <para><seealso cref="http://stackoverflow.com/questions/648196/random-row-from-linq-to-sql/648240#648240"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="result"></param>
    /// <param name="rng">The random number generator to use.</param>
    /// <returns></returns>
    public static bool TryRandom<T>(this System.Collections.Generic.IEnumerable<T> source, out T result, System.Random? rng = null)
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
