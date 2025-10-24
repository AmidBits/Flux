namespace Flux
{
  public static partial class IEnumerables
  {
    #region Random

    /// <summary>
    /// <para>Returns approximately the specified percent (<paramref name="probability"/>) of random elements from <paramref name="source"/> up to <paramref name="maxCount"/> elements. Uses the specified <paramref name="rng"/> (shared if null).</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="probability">Probability as a percent value in the range (0, 1].</param>
    /// <param name="rng">The random-number-generator to use, or <see cref="System.Random.Shared"/> if null.</param>
    /// <param name="maxCount"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Collections.Generic.IEnumerable<T> GetRandomElements<T>(this System.Collections.Generic.IEnumerable<T> source, double probability, System.Random? rng = null, int maxCount = int.MaxValue)
    {
      if (maxCount < 1) throw new System.ArgumentOutOfRangeException(nameof(maxCount));

      Units.Probability.AssertMember(probability, IntervalNotation.HalfOpenLeft); // Cannot be zero, but can be one.

      rng ??= System.Random.Shared;

      var count = 0;

      foreach (var item in source)
        if (rng.NextDouble() < probability)
        {
          yield return item;

          if (++count >= maxCount) break;
        }
    }

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
    /// <para>Randomize an element and its index in <paramref name="source"/>, or <paramref name="value"/> if none is found (with index = -1). Uses the specified <paramref name="rng"/> (shared if null).</para>
    /// <para><seealso href="http://stackoverflow.com/questions/648196/random-row-from-linq-to-sql/648240#648240"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="value"></param>
    /// <param name="rng">The random number generator to use.</param>
    /// <returns></returns>
    public static (T Item, int Index) RandomOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, T value, System.Random? rng = null)
    {
      rng ??= System.Random.Shared;

      var result = (value, -1);

      var index = 0;

      foreach (var item in source)
        if (rng.Next(++index) == 0) // Add one to index before the RNG call, because the upper range is exlusive, so no missing any numbers.
          result = (item, index - 1); // And subtract one for correct index reference.

      return result;
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

    #endregion
  }
}
