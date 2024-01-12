namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Randomize an element and its index in <paramref name="source"/>, or <paramref name="value"/> if none is found (with index = -1). Uses the specified <paramref name="rng"/> (default if null).</summary>
    /// <seealso cref="http://stackoverflow.com/questions/648196/random-row-from-linq-to-sql/648240#648240"/>
    /// <param name="rng">The random number generator to use.</param>
    public static (T item, int index) RandomOrValue<T>(this System.Collections.Generic.IEnumerable<T> source, T value, System.Random? rng = null)
    {
      rng ??= System.Random.Shared;

      var result = (value, -1);

      var index = 0;

      foreach (var item in source)
        if (rng.Next(++index) == 0)
          result = (item, index - 1);

      return result;
    }
  }
}
