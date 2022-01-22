namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns a shuffled (randomized) sequence. Uses the specified Random.</summary>
    public static void Shuffle<T>(this System.Span<T> source, System.Random rng)
    {
      if (rng is null) throw new System.ArgumentNullException(nameof(rng));

      for (var index = source.Length - 1; index > 0; index--) // Shuffle each element by swapping with a random element of a lower index.
        Swap(source, index, rng.Next(index + 1)); // Since 'Next(max-value-excluded)' we add one.
    }
    /// <summary>Returns a shuffled (randomized) sequence. Uses the cryptographic Random.</summary>
    public static void Shuffle<T>(this System.Span<T> source)
      => Shuffle(source, Randomization.NumberGenerator.Crypto);
  }
}
