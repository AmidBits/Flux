namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Creates a new sequence by skipping the last elements that satisfies the predicate. This version also passes the source index into the predicate.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<T> SkipLastWhile<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, int, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var buffer = new System.Collections.Generic.Queue<T>();

      var counter = 0;

      foreach (var item in source)
      {
        if (predicate(item, counter++))
          buffer.Enqueue(item);
        else
        {
          while (buffer.Count > 0)
            yield return buffer.Dequeue();

          yield return item;
        }
      }
    }
  }
}
