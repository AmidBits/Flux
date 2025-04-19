namespace Flux
{
  public static partial class IEnumerables
  {
    public static System.Collections.Generic.IEnumerable<(TSource, TTarget)> CartesianProductFor2<TSource, TTarget>(this System.Collections.Generic.IEnumerable<TSource> source, System.Collections.Generic.IEnumerable<TTarget> target)
      => source.SelectMany(s => target, (s, t) => (s, t));

    /// <summary>
    /// <para>Creates a new sequence with the cartesian product of all elements in the specified sequences.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="targets"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<T>> CartesianProduct<T>(this System.Collections.Generic.IEnumerable<T> source, params System.Collections.Generic.IEnumerable<T>[] targets)
    {
      System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<T>> emptyProduct = [[]];

      return System.Linq.Enumerable.Aggregate(
        new System.Collections.Generic.IEnumerable<T>[] { source }.Concat(targets)
        , emptyProduct
        , (accumulator, sequence) =>
          from accumulatorSequence
          in accumulator
          from item
          in sequence
          select accumulatorSequence.Concat([item])
      );
    }

    /// <summary>
    /// <para>Creates a new sequence with the cartesian product of all elements in the specified sequences.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="targets"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<T>> CartesianProduct<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<T>> targets)
    {
      System.Collections.Generic.IEnumerable<System.Collections.Generic.IEnumerable<T>> emptyProduct = new[] { System.Linq.Enumerable.Empty<T>() };

      return System.Linq.Enumerable.Aggregate(
        new System.Collections.Generic.IEnumerable<T>[] { source }.Concat(targets)
        , emptyProduct
        , (accumulator, sequence) =>
          from accumulatorSequence
          in accumulator
          from item
          in sequence
          select accumulatorSequence.Concat(new[] { item })
      );
    }
  }
}
