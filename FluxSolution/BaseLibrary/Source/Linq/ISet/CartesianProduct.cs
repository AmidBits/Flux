namespace Flux
{
  public static partial class ISet
  {
    /// <summary>Creates a new sequence with the cartesian product of all elements in the specified sequences.</summary>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<T>> CartesianProduct<T>(this System.Collections.Generic.ISet<T> source, System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<T>> targets)
    {
      System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<T>> emptyProduct = new[] { System.Linq.Enumerable.Empty<T>() };

      return System.Linq.Enumerable.Aggregate(new System.Collections.Generic.IEnumerable<T>[] { source }.Concat(targets), emptyProduct, (accumulator, sequence) => from accumulatorSequence in accumulator from item in sequence select accumulatorSequence.Concat(new[] { item }));
    }
  }
}