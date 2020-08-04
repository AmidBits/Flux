namespace Flux
{
  public static partial class XtensionsCollections
  {
    /// <summary>Returns a sub-array from the specified array from the specified offset and count.</summary>
    public static T[] ToArray<T>(this System.Collections.Generic.IList<T> source, int offset, int count)
    {
      var target = new T[count];
      offset += count;
      while (count > 0) target[--count] = source[--offset];
      return target;
    }
  }
}
