namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>Returns a random element from the sequence. Slightly more random than the above, but a bit slower too. Uses the specified random number generator (the .NET cryptographic if null).</summary>
    /// <seealso cref="http://stackoverflow.com/questions/648196/random-row-from-linq-to-sql/648240#648240"/>
    /// <param name="rng">The random number generator to use.</param>
    public static bool RandomElement<T>(this System.Collections.Generic.IEnumerable<T> source, out T result, System.Random rng)
    {
      rng ??= Flux.Random.NumberGenerator.Crypto;

      result = default!;

      var count = 1;

      using (var e = source.ThrowOnNull().GetEnumerator())
      {
        if (e.MoveNext())
        {
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
    /// <summary>Returns a random element from the sequence. Based on a cryptographic randomizer. Uses the .NET cryptographic random number generator.</summary>
    public static bool RandomElement<T>(this System.Collections.Generic.IEnumerable<T> source, out T result)
      => RandomElement(source, out result, Flux.Random.NumberGenerator.Crypto);

    /// <summary>Returns the specified percent of random elements from the sequence. Uses the specified random number generator (the .NET cryptographic if null).</summary>
    /// <param name="percent">Percent (or probability) as a value in the range [0, 1].</param>
    /// <param name="rng">The random number generator to use.</param>
    public static System.Collections.Generic.IEnumerable<T> RandomElements<T>(this System.Collections.Generic.IEnumerable<T> source, double percent, System.Random rng)
    {
      rng ??= Flux.Random.NumberGenerator.Crypto;

      foreach (var element in source.EmptyOnNull())
        if (rng.NextDouble() < percent)
          yield return element;
    }
    /// <summary>Returns the specified percent of random elements from the sequence. Uses the .NET cryptographic random number generator.</summary>
    /// <param name="percent"></param>
    public static System.Collections.Generic.IEnumerable<T> RandomElements<T>(this System.Collections.Generic.IEnumerable<T> source, double percent)
      => RandomElements(source, percent, Flux.Random.NumberGenerator.Crypto);
  }
}
