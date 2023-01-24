namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    /// <summary>Creates a new array from the source sequence, adding the number of specified pre-slots and post-slots.</summary>
    public static T[] ToArray<T>(this System.ReadOnlySpan<T> source, int offset, int count, int preLength, int postLength)
    {
      if (offset < 0 || offset >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(offset));
      if (count < 0 || offset + count >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(count));

      var target = new T[preLength + count + postLength];
      source.Slice(offset, count).CopyTo(new System.Span<T>(target).Slice(preLength, count));
      return target;
    }
    /// <summary>Creates a new list from the specified array from the specified offset and count.</summary>
    public static T[] ToArray<T>(this System.ReadOnlySpan<T> source, int offset, int count)
      => ToArray(source, offset, count, 0, 0);
    /// <summary>Creates a new list from the specified array from the specified offset to the end.</summary>
    public static T[] ToArray<T>(this System.ReadOnlySpan<T> source, int offset)
      => ToArray(source, offset, source.Length - offset, 0, 0);
    /// <summary>Creates a new list from the specified span builder.</summary>
    public static T[] ToArray<T>(this System.ReadOnlySpan<T> source)
      => ToArray(source, 0, source.Length, 0, 0);
  }
}
