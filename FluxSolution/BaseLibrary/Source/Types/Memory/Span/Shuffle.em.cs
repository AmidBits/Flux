namespace Flux
{
  public static partial class SpanEm
  {
    /// <summary>In-place shuffle (randomized) of the span. Uses the specified rng.</summary>
    public static System.Span<T> Shuffle<T>(this System.Span<T> source, System.Random random)
    {
      if (random is null) throw new System.ArgumentNullException(nameof(random));

      for (var index = source.Length - 1; index > 0; index--) // Shuffle each element by swapping with a random element of a lower index.
        Swap(source, index, random.Next(index + 1)); // Since 'Next(max-value-excluded)' we add one.

      return source;
    }
    /// <summary>In-place shuffle (randomized) of the span. Uses a cryptographic rng.</summary>
    public static System.Span<T> Shuffle<T>(this System.Span<T> source)
      => Shuffle(source, Randomization.NumberGenerator.Crypto);
  }
}
