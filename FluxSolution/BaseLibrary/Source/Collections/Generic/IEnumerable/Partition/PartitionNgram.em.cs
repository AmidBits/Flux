namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Partition the sequence as a new sequence of size elements.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/N-gram"/>
    public static System.Collections.Generic.IEnumerable<TResult> PartitionNgram<TSource, TResult>(this System.Collections.Generic.IEnumerable<TSource> source, int size, System.Func<System.Collections.Generic.IEnumerable<TSource>, int, TResult> resultSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      if (size < 1) throw new System.ArgumentOutOfRangeException(nameof(size), $"Must be greater than or equal to 1 ({size}).");

      using var e = source.GetEnumerator();

      var index = 0;

      var ngram = new System.Collections.Generic.Queue<TSource>(size);

      if (e.MoveNext())
      {
        do
        {
          ngram.Enqueue(e.Current);
        }
        while (ngram.Count < size && e.MoveNext());

        if (ngram.Count == size)
        {
          yield return resultSelector(ngram, index++);

          while (e.MoveNext())
          {
            ngram.Dequeue();
            ngram.Enqueue(e.Current);

            yield return resultSelector(ngram, index++);
          }
        }
        else throw new System.ArgumentException(@"Insufficient number of elements.", nameof(source));
      }
      else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
    }
  }
}
