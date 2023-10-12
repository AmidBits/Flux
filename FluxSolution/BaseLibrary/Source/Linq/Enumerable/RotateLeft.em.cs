namespace Flux
{
  public static partial class Enumerable
  {
    /// <summary>Returns the sequence rotated left by the specified count.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<T> RotateLeft<T>(this System.Collections.Generic.IEnumerable<T> source, int count)
    {
      if (count < 0) throw new System.ArgumentOutOfRangeException(nameof(count));

      var rotate = new System.Collections.Generic.Queue<T>(count);

      foreach (var item in source.ThrowOnNull())
      {
        if (rotate.Count < count)
          rotate.Enqueue(item);
        else
          yield return item;
      }

      while (rotate.Any())
        yield return rotate.Dequeue();
    }
  }
}
