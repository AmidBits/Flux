namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Apportions the sequence into lists of specified size with the specified stepping (or gap) interleave (0 means next in line and a positive number below size means skip that many, from the start of the previous list). Optionally include trailing lists, i.e. lists that could not be filled to size.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    /// <exception cref="System.ArgumentOutOfRangeException"/>
    public static System.Collections.Generic.IEnumerable<TResult> PartitionWindowed<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, int size, int step, bool includeTrailing, System.Func<System.Collections.Generic.List<TSource>, TResult> resultSelector)
    {
      System.ArgumentNullException.ThrowIfNull(resultSelector);

      if (size <= 0) throw new System.ArgumentOutOfRangeException(nameof(size), "Must be greater-than zero.");
      if (step <= 0 || step > size) throw new System.ArgumentOutOfRangeException(nameof(step), "Must be greater-than zero and less-than-or-equal to size.");

      var queue = new System.Collections.Generic.Queue<System.Collections.Generic.List<TSource>>();

      var e = source.ThrowOnNull().GetEnumerator();

      var index = step;

      while (e.MoveNext())
      {
        if (index++ >= step)
        {
          index = 1;

          queue.Enqueue(new System.Collections.Generic.List<TSource>());
        }

        foreach (var list in queue)
          list.Add(e.Current);

        if (queue.Peek().Count == size)
          yield return resultSelector(queue.Dequeue());
      };

      if (includeTrailing)
        while (queue.Count > 0 && queue.Peek().Count > 0)
          yield return resultSelector(queue.Dequeue());
    }
  }
}
