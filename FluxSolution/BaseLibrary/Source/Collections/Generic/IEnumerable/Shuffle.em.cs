using System.Linq;

namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>Returns a shuffled (randomized) sequence. Uses the specified Random.</summary>
    public static System.Collections.Generic.IEnumerable<T> Randomize<T>(this System.Collections.Generic.IEnumerable<T> source, System.Random rng)
      => source.OrderBy(t => rng.Next());
    /// <summary>Returns a shuffled (randomized) sequence. Uses the cryptographic Random.</summary>
    public static System.Collections.Generic.IEnumerable<T> Randomize<T>(this System.Collections.Generic.IEnumerable<T> source)
      => source.Randomize(Flux.Random.NumberGenerator.Crypto);

    /// <summary>Returns a shuffled (randomized) ilist. Uses the specified Random.</summary>
    public static System.Collections.Generic.IList<T> RandomizeToList<T>(this System.Collections.Generic.IEnumerable<T> source, System.Random rng)
    {
      rng ??= Flux.Random.NumberGenerator.Crypto;

      var list = new System.Collections.Generic.List<T>();

      foreach (var element in source.ThrowOnNull())
        list.Insert(rng.Next(list.Count + 1), element);

      return list;
    }
    /// <summary>Returns a shuffled (randomized) list. Uses the specified Random.</summary>
    public static System.Collections.Generic.IList<T> RandomizeToList<T>(this System.Collections.Generic.IEnumerable<T> source)
      => RandomizeToList(source, Flux.Random.NumberGenerator.Crypto);
  }
}
