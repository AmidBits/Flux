namespace Flux
{
  public static partial class Enumerable
  {
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
