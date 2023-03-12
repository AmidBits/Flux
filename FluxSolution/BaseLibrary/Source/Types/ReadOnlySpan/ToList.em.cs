namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    /// <summary>Creates a new list from the specified array from the specified offset and count.</summary>
    public static System.Collections.Generic.List<T> ToList<T>(this System.ReadOnlySpan<T> source, int offset, int count)
    {
      if (offset < 0 || offset >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(offset));
      if (count < 0 || offset + count >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(count));

      var target = new System.Collections.Generic.List<T>(count);

      while (count-- > 0)
        target.Add(source[offset++]);

      return target;
    }
    /// <summary>Creates a new list from the specified array from the specified offset to the end.</summary>
    public static System.Collections.Generic.List<T> ToList<T>(this System.ReadOnlySpan<T> source, int offset) => ToList(source, offset, source.Length - offset);
    /// <summary>Creates a new list from the readonlyspan.</summary>
    public static System.Collections.Generic.List<T> ToList<T>(this System.ReadOnlySpan<T> source) => ToList(source, 0, source.Length);
  }
}
