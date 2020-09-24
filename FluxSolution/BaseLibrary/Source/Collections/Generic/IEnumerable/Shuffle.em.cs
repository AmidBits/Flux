using System.Linq;

namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Returns a shuffled (randomized) sequence. Uses the specified Random.</summary>
    public static System.Collections.Generic.IEnumerable<T> Shuffle<T>(this System.Collections.Generic.IEnumerable<T> source, System.Random rng)
      => source.OrderBy(t => (rng ?? Flux.Random.NumberGenerator.Crypto).Next());
    /// <summary>Returns a shuffled (randomized) sequence. Uses the cryptographic Random.</summary>
    public static System.Collections.Generic.IEnumerable<T> Shuffle<T>(this System.Collections.Generic.IEnumerable<T> source)
      => source.Shuffle(Flux.Random.NumberGenerator.Crypto);
  }
}
