using System.Linq;

namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>Returns a random element from the sequence, based on the specified RNG.</summary>
    public static T RandomElement<T>(this System.Collections.Generic.IList<T> source, System.Random rng)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (rng is null) throw new System.ArgumentNullException(nameof(rng));

      if (source.Count == 0)
        throw new System.ArgumentException(@"The sequence is empty.");

      return source[rng.Next(source.Count)];
    }
    /// <summary>Returns a random element from the sequence.</summary>
    public static T RandomElement<T>(this System.Collections.Generic.IList<T> source)
      => source.RandomElement(new Flux.Random.Cryptographic());
  }
}
