namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Creates a new sequence by taking the last elements of the sequence that satisfies the predicate. This version also passes the source index into the predicate.</summary>
    public static System.Collections.Generic.IEnumerable<T> TakeLastWhile<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, int, bool> predicate)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var buffer = new System.Collections.Generic.List<T>();

      var counter = 0;

      foreach (var element in source)
      {
        if (predicate(element, counter++))
        {
          buffer.Add(element);
        }
        else
        {
          buffer.Clear();
        }
      }

      return buffer;
    }
    /// <summary>Creates a new sequence by taking the last elements of the sequence that satisfies the predicate.</summary>
    public static System.Collections.Generic.IEnumerable<T> TakeLastWhile<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, bool> predicate)
      => TakeLastWhile(source, (t, i) => predicate(t));
  }
}
