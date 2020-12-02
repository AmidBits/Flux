namespace Flux
{
  public static partial class SpanEm
  {
    /// <summary>Returns a shuffled (randomized) sequence. Uses the specified Random.</summary>
    public static void Shuffle<T>(this System.Span<T> source, System.Random rng)
    {
      if (rng is null) throw new System.ArgumentNullException(nameof(rng));

      for (var index = source.Length - 1; index > 0; index--)
        source.Swap(index, rng.Next(index));
    }
    /// <summary>Returns a shuffled (randomized) sequence. Uses the cryptographic Random.</summary>
    public static void Shuffle<T>(this System.Span<T> source)
      => Shuffle(source, Flux.Random.NumberGenerator.Crypto);

    /// <summary>Returns a shuffled (randomized) sequence. Uses the specified Random.</summary>
    public static void Shuffle<T>(this System.Collections.Generic.IList<T> source, System.Random rng)
      => Shuffle((System.Span<T>)(T[])source, rng);
    /// <summary>Returns a shuffled (randomized) sequence. Uses the cryptographic Random.</summary>
    public static void Shuffle<T>(this System.Collections.Generic.IList<T> source)
      => Shuffle((System.Span<T>)(T[])source, Flux.Random.NumberGenerator.Crypto);
  }
}
