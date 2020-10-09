namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Returns a random element from the sequence. Slightly more random than the above, but a bit slower too. Uses the specified random number generator (the .NET cryptographic if null).</summary>
    /// <seealso cref="http://stackoverflow.com/questions/648196/random-row-from-linq-to-sql/648240#648240"/>
    /// <param name="rng">The random number generator to use.</param>
    public static bool RandomElement<T>(this System.Collections.Generic.IEnumerable<T> source, out T result, System.Random rng)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      rng ??= Flux.Random.NumberGenerator.Crypto;

      result = default!;

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
      if (percent <= 0 && percent > 1) throw new System.ArgumentOutOfRangeException(nameof(percent));

      rng ??= Flux.Random.NumberGenerator.Crypto;

      foreach (var element in source ?? System.Linq.Enumerable.Empty<T>())
        if (rng.NextDouble() < percent)
          yield return element;
    }
    /// <summary>Returns the specified percent of random elements from the sequence. Uses the .NET cryptographic random number generator.</summary>
    /// <param name="percent"></param>
    public static System.Collections.Generic.IEnumerable<T> RandomElements<T>(this System.Collections.Generic.IEnumerable<T> source, double percent)
      => RandomElements(source, percent, Flux.Random.NumberGenerator.Crypto);
  }
}
