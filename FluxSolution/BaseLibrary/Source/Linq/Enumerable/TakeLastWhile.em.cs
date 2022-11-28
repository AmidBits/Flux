namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Creates a new sequence by taking the last elements of <paramref name="source"/> that satisfies the <paramref name="predicate"/>. This version also passes the source index into the predicate.</summary>
    public static System.Collections.Generic.IEnumerable<T> TakeLastWhile<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, int, bool> predicate)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var buffer = new System.Collections.Generic.List<T>();

      var counter = 0;

      using var e = source.GetEnumerator();

      while (e.MoveNext())
      {
        if (e.Current is var current && predicate(current, counter++))
          buffer.Add(current);
        else
          buffer.Clear();
      }

      return buffer;
    }
  }
}
