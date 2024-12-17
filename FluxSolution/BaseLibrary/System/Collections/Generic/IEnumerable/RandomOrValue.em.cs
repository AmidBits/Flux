namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Randomize an element and its index in <paramref name="source"/>, or <paramref name="value"/> if none is found (with index = -1). Uses the specified <paramref name="rng"/> (shared if null).</summary>
    /// <seealso cref="http://stackoverflow.com/questions/648196/random-row-from-linq-to-sql/648240#648240"/>
    /// <param name="rng">The random number generator to use.</param>
    public static (T Item, int Index) RandomOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, T value, System.Random? rng = null)
    {
      rng ??= System.Random.Shared;

      var result = (value, -1);

      var index = 0;

      foreach (var item in source)
      {
        if (rng.Next(index + 1) == 0) // Add one to index for the RNG call, because the upper range is exlusive, so no missing any numbers.
          result = (item, index);

        index++;
      }

      return result;
    }
  }
}
