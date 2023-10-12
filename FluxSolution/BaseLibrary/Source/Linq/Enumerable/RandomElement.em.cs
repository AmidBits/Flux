namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Returns a random element, as an out parameter, from the sequence. Uses the specified random number generator (default if null).</summary>
    /// <seealso cref="http://stackoverflow.com/questions/648196/random-row-from-linq-to-sql/648240#648240"/>
    /// <param name="rng">The random number generator to use.</param>
    public static T GetRandomElement<T>(this System.Collections.Generic.IEnumerable<T> source, System.Random? rng = null)
    {
      rng ??= new System.Random();

      var value = default(T);

      var count = 0;

      foreach (var item in source.ThrowOnNullOrEmpty())
      {
        if (rng.Next(count) == 0)
          value = item;

        count++;
      }

      return value!;
    }

    /// <summary>Returns a random element, as an out parameter, from the sequence. Uses the specified random number generator (default if null).</summary>
    /// <seealso cref="http://stackoverflow.com/questions/648196/random-row-from-linq-to-sql/648240#648240"/>
    /// <param name="rng">The random number generator to use.</param>
    public static bool TryGetRandomElement<T>(this System.Collections.Generic.IEnumerable<T> source, out T result, System.Random? rng = null)
    {
      try
      {
        result = source.GetRandomElement(rng);
        return true;
      }
      catch { }

      result = default!;
      return false;
    }

    /// <summary>Returns a random element from the sequence. Uses the specified random number generator.</summary>
    public static T RandomElement<T>(this System.Collections.Generic.IEnumerable<T> source, System.Random? rng = null)
    => TryGetRandomElement(source, out var re, rng) ? re : throw new System.InvalidOperationException();
  }
}
