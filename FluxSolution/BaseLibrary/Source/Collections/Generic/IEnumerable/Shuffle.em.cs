using System.Linq;

namespace Flux
{
  public static partial class XtensionsCollections
  {
    /// <summary>Returns a shuffled sequence by ordering using a new Flux.CryptoRandom() instance.</summary>
    public static System.Collections.Generic.IEnumerable<T> Shuffle<T>(this System.Collections.Generic.IEnumerable<T> source)
      => source.Shuffle(new Flux.Random.Cryptographic());
    /// <summary>Returns a shuffled sequence by ordering using integers from the specified Flux.CryptoRandom instance.</summary>
    public static System.Collections.Generic.IEnumerable<T> Shuffle<T>(this System.Collections.Generic.IEnumerable<T> source, System.Random rng)
      => source.OrderBy(t => rng.Next());
  }
}
