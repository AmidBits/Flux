namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns a random element from <paramref name="source"/>. Uses the specified <paramref name="rng"/> (default if null).</summary>
    /// <exception cref="System.InvalidOperationException"></exception>
    public static T Random<T>(this System.Collections.Generic.IEnumerable<T> source, System.Random? rng = null)
    => RandomOrValue(source, default!, rng) is var random && (random.index > -1) ? random.item : throw new System.InvalidOperationException();

    /// <summary>Attempts to fetch a random element from <paramref name="source"/> into <paramref name="result"/> and indicates whether successful. Uses the specified <paramref name="rng"/> (default if null).</summary>
    /// <seealso cref="http://stackoverflow.com/questions/648196/random-row-from-linq-to-sql/648240#648240"/>
    /// <param name="rng">The random number generator to use.</param>
    public static bool TryRandom<T>(this System.Collections.Generic.IEnumerable<T> source, out T result, System.Random? rng = null)
    {
      try
      {
        var (item, index) = RandomOrValue(source, default!, rng);

        if (index > -1)
        {
          result = item;
          return true;
        }
      }
      catch { }

      result = default!;
      return false;
    }
  }
}
