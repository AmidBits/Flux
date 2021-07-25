namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Creates a new list from the specified array from the specified offset and count.</summary>
    public static System.Collections.Generic.List<T> ToList<T>(this System.Collections.Generic.IList<T> source, int offset, int count)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (offset < 0 || offset >= source.Count) throw new System.ArgumentOutOfRangeException(nameof(offset));
      if (count < 0 || count + offset >= source.Count) throw new System.ArgumentOutOfRangeException(nameof(count));

      var target = new System.Collections.Generic.List<T>(count);
      while (count-- > 0)
        target.Add(source[offset++]);
      return target;
    }
    /// <summary>Creates a new list from the specified array from the specified offset to the end.</summary>
    public static System.Collections.Generic.List<T> ToList<T>(this System.Collections.Generic.IList<T> source, int offset)
      => ToList(source, offset, (source ?? throw new System.ArgumentNullException(nameof(source))).Count - offset);
  }
}
