using System.Linq;

namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>Returns a random element from the sequence, based on the specified RNG.</summary>
    public static T RandomElement<T>(this System.Collections.Generic.IList<T> source, System.Random rng)
      => source.ThrowOnNull().Any() ? source[(rng ?? throw new System.ArgumentNullException(nameof(rng))).Next(source.Count)] : throw new System.ArgumentException(@"The sequence is empty.");
    /// <summary>Returns a random element from the sequence.</summary>
    public static T RandomElement<T>(this System.Collections.Generic.IList<T> source)
      => RandomElement(source,Flux.Random.NumberGenerator.Crypto);
  }
}
