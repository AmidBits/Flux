namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Returns the sequence rotated left by the specified count. The left rotation has a proportional cost (the rotation count directly affects the sequence buffer use).</summary>
    public static System.Collections.Generic.IEnumerable<T> RotateLeft<T>(this System.Collections.Generic.IEnumerable<T> source, int count)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var buffer = new System.Collections.Generic.Queue<T>(count);

      using var e = source.GetEnumerator();

      while (e.MoveNext())
      {
        if (buffer.Count < count)
          buffer.Enqueue(e.Current);
        else
          yield return e.Current;
      }

      while (buffer.Count > 0)
        yield return buffer.Dequeue();
    }
  }
}
