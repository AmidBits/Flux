namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Returns a shuffled (randomized) sequence. Uses the specified Random.</summary>
    public static void Shuffle<T>(this System.Collections.Generic.IList<T> source, System.Random rng)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      rng ??= Flux.Random.NumberGenerator.Crypto;

      for (var index = source.Count - 1; index > 0; index--)
        source.Swap(index, rng.Next(index));
    }
    /// <summary>Returns a shuffled (randomized) sequence. Uses the cryptographic Random.</summary>
    public static void Shuffle<T>(this System.Collections.Generic.IList<T> source)
      => Shuffle(source, Flux.Random.NumberGenerator.Crypto);
  }
}
