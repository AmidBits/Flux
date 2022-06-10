namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Returns a random element, as an out parameter, from the sequence. Uses the specified random number generator.</summary>
    /// <seealso cref="http://stackoverflow.com/questions/648196/random-row-from-linq-to-sql/648240#648240"/>
    /// <param name="random">The random number generator to use.</param>
    public static bool TryGetRandomElement<T>(this System.Collections.Generic.IEnumerable<T> source, out T result, System.Random random)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (random is null) throw new System.ArgumentNullException(nameof(random));

      result = default!;

      using var e = source.GetEnumerator();

      if (e.MoveNext())
      {
        var count = 1;

        do
        {
          if (random.Next(count++) == 0)
            result = e.Current;
        }
        while (e.MoveNext());

        return true;
      }

      return false;
    }
    /// <summary>Returns a random element, as an out parameter, from the sequence. Uses the .NET cryptographic random number generator.</summary>
    public static bool TryGetRandomElement<T>(this System.Collections.Generic.IEnumerable<T> source, out T result)
      => TryGetRandomElement(source, out result, Randomization.NumberGenerator.Crypto);

    /// <summary>Returns a random element from the sequence. Uses the specified random number generator.</summary>
    public static T RandomElement<T>(this System.Collections.Generic.IEnumerable<T> source, System.Random random)
      => TryGetRandomElement(source, out var re, random) ? re : throw new System.InvalidOperationException();
    /// <summary>Returns a random element from the sequence. Uses the .NET cryptographic random number generator.</summary>
    public static T RandomElement<T>(this System.Collections.Generic.IEnumerable<T> source)
      => TryGetRandomElement(source, out var re, Randomization.NumberGenerator.Crypto) ? re : throw new System.InvalidOperationException();
  }
}
