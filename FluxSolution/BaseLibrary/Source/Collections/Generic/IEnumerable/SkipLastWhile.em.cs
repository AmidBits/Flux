namespace Flux
{
  public static partial class XtendCollections
  {
#if !NETCOREAPP
    /// <summary>Returns all elements in a sequence except a specified number of elements at the end.</summary>
    internal static System.Collections.Generic.IEnumerable<T> SkipLast<T>(this System.Collections.Generic.IEnumerable<T> source, int count)
    {
      var buffer = new System.Collections.Generic.Queue<T>(count + 1);

      foreach (var element in source)
      {
        buffer.Enqueue(element);

        if (buffer.Count > count)
        {
          yield return buffer.Dequeue();
        }
      }
    }
#endif

    /// <summary>Creates a new sequence by skipping the last elements that satisfies the predicate. This version also passes the source index into the predicate.</summary>
    public static System.Collections.Generic.IEnumerable<T> SkipLastWhile<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, int, bool> predicate)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var buffer = new System.Collections.Generic.Queue<T>();

      var counter = 0;

      foreach (var element in source)
      {
        if (predicate(element, counter++))
        {
          buffer.Enqueue(element);
        }
        else
        {
          while (buffer.Count > 0)
          {
            yield return buffer.Dequeue();
          }

          yield return element;
        }
      }
    }
    /// <summary>Creates a new sequence by skipping the last elements that satisfies the predicate.</summary>
    public static System.Collections.Generic.IEnumerable<T> SkipLastWhile<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, bool> predicate)
      => source.SkipLastWhile((t, i) => predicate(t));
  }
}
