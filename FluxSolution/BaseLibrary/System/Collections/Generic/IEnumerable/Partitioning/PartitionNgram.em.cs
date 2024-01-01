namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Partition the sequence as a new sequence of size elements.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    /// <exception cref="System.ArgumentOutOfRangeException"/>
    /// <see href="https://en.wikipedia.org/wiki/N-gram"/>
    public static System.Collections.Generic.IEnumerable<TResult> PartitionNgram<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, int size, System.Func<System.Collections.Generic.IEnumerable<TSource>, int, TResult> resultSelector)
    {
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      if (size < 1) throw new System.ArgumentOutOfRangeException(nameof(size), $"Must be greater than or equal to 1 ({size}).");

      using var e = source.ThrowOnNull().GetEnumerator();

      var index = 0;

      var ngram = System.Collections.Immutable.ImmutableQueue<TSource>.Empty;

      while (ngram.Count() < size && e.MoveNext())
        ngram = ngram.Enqueue(e.Current);

      if (ngram.Count() == size)
      {
        yield return resultSelector(ngram, index++);

        while (e.MoveNext())
        {
          ngram = ngram.Dequeue();
          ngram = ngram.Enqueue(e.Current);

          yield return resultSelector(ngram, index++);
        }
      }
      else throw new System.ArgumentException(@"Insufficient number of elements.", nameof(source));
    }
  }
}
