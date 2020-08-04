using System.Linq;

namespace Flux
{
  public static partial class XtensionsCollections
  {
    /// <summary>Returns a random element from the sequence. Based on a cryptographic randomizer.</summary>
    public static T RandomElement<T>(this System.Collections.Generic.IEnumerable<T> source)
      => source.RandomElement(new Flux.Random.Cryptographic());
    /// <summary>Returns a random element from the sequence. Slightly more random than the above, but a bit slower too.</summary>
    /// <seealso cref="http://stackoverflow.com/questions/648196/random-row-from-linq-to-sql/648240#648240"/>
    public static T RandomElement<T>(this System.Collections.Generic.IEnumerable<T> source, System.Random rng)
    {
      var candidates = new System.Collections.Generic.List<T>();

      var counter = 1;

      if (!(source ?? throw new System.ArgumentNullException(nameof(source))).Any()) throw new System.InvalidOperationException(@"The sequence is empty.");

      foreach (var item in source)
      {
        if (rng.Next(counter++) == 0)
        {
          candidates.Add(item);
        }
      }

      return candidates.RandomElement(rng);
    }
  }
}
