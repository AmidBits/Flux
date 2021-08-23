namespace Flux
{
  public static partial class ExtensionMethods
  {
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
      => SkipLastWhile(source, (t, i) => predicate(t));
  }
}
