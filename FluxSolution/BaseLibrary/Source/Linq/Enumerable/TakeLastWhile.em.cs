namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Creates a new sequence by taking the last elements of <paramref name="source"/> that satisfies the <paramref name="predicate"/>. This version also passes the source index into the predicate.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<T> TakeLastWhile<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, int, bool> predicate)
    {
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

      var buffer = new System.Collections.Generic.List<T>();

      var index = 0;

      foreach (var item in source)
      {
        if (predicate(item, index++))
          buffer.Add(item);
        else
          buffer.Clear();
      }

      return buffer;
    }
  }
}
