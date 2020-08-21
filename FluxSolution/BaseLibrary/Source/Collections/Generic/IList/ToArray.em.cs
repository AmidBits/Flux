namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>Returns a sub-array from the specified array from the specified offset and count.</summary>
    public static T[] ToArray<T>(this System.Collections.Generic.IList<T> source, int offset, int count)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (offset < 0 || offset >= source.Count) throw new System.ArgumentOutOfRangeException(nameof(offset));
      if (count < 0 || count + offset >= source.Count) throw new System.ArgumentOutOfRangeException(nameof(count));

      var target = new T[count];
      if (source.GetType().IsArray) System.Array.Copy((System.Array)source, offset, target, 0, count);
      else
      {
        offset += count;
        while (count > 0) target[--count] = source[--offset];
      }
      return target;
    }
  }
}
