namespace Flux
{
  public static partial class SystemCollectionsGenericEm
  {
    /// <summary>Returns the sequence rotated left by the specified count. The left rotation has a proportional cost (the rotation count directly affects the sequence buffer use).</summary>
    public static System.Collections.Generic.IEnumerable<T> RotateLeft<T>(this System.Collections.Generic.IEnumerable<T> source, int count)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (count < 0) throw new System.ArgumentOutOfRangeException(nameof(count));

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

    /// <summary>Returns the sequence rotated right by the specified count. The right rotation has a direct cost (the full sequence is buffered in a list).</summary>
    public static System.Collections.Generic.IEnumerable<T> RotateRight<T>(this System.Collections.Generic.IEnumerable<T> source, int count)
    {
      if (count < 0) throw new System.ArgumentOutOfRangeException(nameof(count));

      var buffer = new System.Collections.Generic.List<T>(source);

      var remainderCount = buffer.Count - count;

      for (var index = remainderCount; index < buffer.Count; index++)
        yield return buffer[index];

      for (var index = 0; index < remainderCount; index++)
        yield return buffer[index];
    }
  }
}
