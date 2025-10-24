namespace Flux
{
  public static partial class IEnumerables
  {
    #region PartitionNgram

    /// <summary>
    /// <para>Partition the sequence as a new sequence of size elements.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/N-gram"/></para>
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="source"></param>
    /// <param name="size"></param>
    /// <param name="resultSelector"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    /// <exception cref="System.ArgumentException"></exception>
    public static System.Collections.Generic.IEnumerable<TResult> PartitionNgram<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, int size, System.Func<System.Collections.Generic.IEnumerable<TSource>, int, TResult> resultSelector)
    {
      System.ArgumentNullException.ThrowIfNull(resultSelector);

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

    #endregion
  }
}
