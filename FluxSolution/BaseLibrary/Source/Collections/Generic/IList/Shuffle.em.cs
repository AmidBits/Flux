namespace Flux
{
  public static partial class IListEm
  {
    // https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle

    /// <summary>Returns a shuffled (randomized) sequence, implementing the standard Knuth-Fisher-Yates algorithm. Uses the specified Random.</summary>
    public static void Shuffle<T>(this System.Collections.Generic.IList<T> source, System.Random random)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (random is null) throw new System.ArgumentNullException(nameof(random));

      for (var index = source.Count - 1; index > 0; index--)
        Swap(source, index, random.Next(index + 1)); // Since 'Next(max-value-excluded)' we add one.
    }
    /// <summary>Returns a shuffled (randomized) sequence, implementing the standard Knuth-Fisher-Yates algorithm. Uses the cryptographic Random.</summary>
    public static void Shuffle<T>(this System.Collections.Generic.IList<T> source)
      => Shuffle(source, Randomization.NumberGenerator.Crypto);
  }
}
