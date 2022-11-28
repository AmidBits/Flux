namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Returns a random element, as an out parameter, from the sequence. Uses the specified random number generator.</summary>
    /// <seealso cref="http://stackoverflow.com/questions/648196/random-row-from-linq-to-sql/648240#648240"/>
    /// <param name="rng">The random number generator to use.</param>
    public static bool TryGetRandomElement<T>(this System.Collections.Generic.IEnumerable<T> source, out T result, System.Random? rng = null)
    {
      rng ??= new System.Random();

      result = default!;

      if (source is not null)
      {
        using var e = source.GetEnumerator();

        if (e.MoveNext())
        {
          var count = 1;

          do
          {
            if (rng.Next(count++) == 0)
              result = e.Current;
          }
          while (e.MoveNext());

          return true;
        }
      }

      return false;
    }

    /// <summary>Returns a random element from the sequence. Uses the specified random number generator.</summary>
    public static T RandomElement<T>(this System.Collections.Generic.IEnumerable<T> source, System.Random rng)
    => TryGetRandomElement(source, out var re, rng) ? re : throw new System.InvalidOperationException();
  }
}
