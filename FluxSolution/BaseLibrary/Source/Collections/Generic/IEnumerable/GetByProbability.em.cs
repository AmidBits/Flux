namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>Returns elements from the sequence by probability, hence approximately the percent of the total number of elements are returned.</summary>
    /// <seealso cref="http://stackoverflow.com/questions/648196/random-row-from-linq-to-sql/648240#648240"/>
    public static System.Collections.Generic.IEnumerable<T> GetByProbability<T>(this System.Collections.Generic.IEnumerable<T> source, double probability, System.Random rng)
    {
      if (rng is null) throw new System.ArgumentNullException(nameof(rng));

      foreach (var element in source.EmptyOnNull())
        if (rng.NextDouble() < probability)
          yield return element;
    }
    /// <summary>Returns elements from the sequence by probability, hence approximately the percent of the total number of elements are returned.</summary>
    public static System.Collections.Generic.IEnumerable<T> GetByProbability<T>(this System.Collections.Generic.IEnumerable<T> source, double probability)
      => GetByProbability(source, probability, Flux.Random.NumberGenerator.Crypto);
  }
}
