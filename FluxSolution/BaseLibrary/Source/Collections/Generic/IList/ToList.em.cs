namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>Returns a sub-array from the specified array from the specified offset and count.</summary>
    public static System.Collections.Generic.List<T> GetList<T>(this System.Collections.Generic.IList<T> source, int offset, int count)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var target = new System.Collections.Generic.List<T>(count);
      while (count-- > 0) target.Add(source[offset++]);
      return target;
    }
  }
}
