namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>Returns a random element from the sequence. Slightly more random than the above, but a bit slower too.</summary>
    /// <seealso cref="http://stackoverflow.com/questions/648196/random-row-from-linq-to-sql/648240#648240"/>
    public static T RandomElement<T>(this System.Collections.Generic.IEnumerable<T> source, System.Random rng)
    {
      if (rng is null) throw new System.ArgumentNullException(nameof(rng));

      var count = 1;
      var current = default(T);

      foreach (var element in source.ThrowOnNullOrEmpty())
        if (rng.Next(count++) == 0)
          current = element;

      return current!;
    }
    /// <summary>Returns a random element from the sequence. Based on a cryptographic randomizer.</summary>
    public static T RandomElement<T>(this System.Collections.Generic.IEnumerable<T> source)
      => RandomElement(source, Flux.Random.NumberGenerator.Crypto);
  }
}
