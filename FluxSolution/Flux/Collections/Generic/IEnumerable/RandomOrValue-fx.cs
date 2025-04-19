namespace Flux
{
  public static partial class IEnumerables
  {
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
  }
}
