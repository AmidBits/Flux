namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Returns the sequence rotated left by the specified count.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<T> RotateLeft<T>(this System.Collections.Generic.IEnumerable<T> source, int count)
    {
      if (count < 0) throw new System.ArgumentOutOfRangeException(nameof(count));

      using var e = source.ThrowOnNull().GetEnumerator();

      var rotate = new System.Collections.Generic.Queue<T>(count);

      while (e.MoveNext())
      {
        if (rotate.Count < count)
          rotate.Enqueue(e.Current);
        else
          yield return e.Current;
      }

      while (rotate.Any())
        yield return rotate.Dequeue();
    }
  }
}
