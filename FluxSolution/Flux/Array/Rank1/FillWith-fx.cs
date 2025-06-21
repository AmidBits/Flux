namespace Flux
{
  public static partial class Arrays
  {
    /// <summary>
    /// <para>Fill <paramref name="length"/> elements in <paramref name="source"/> from <paramref name="pattern"/> (repeatingly if necessary) at <paramref name="index"/>.</para>
    /// </summary>
    public static T[] FillWith<T>(this T[] source, int index, int length, params System.ReadOnlySpan<T> pattern)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var copyLength = int.Min(pattern.Length, length);

      pattern.Slice(0, copyLength).CopyTo(source.AsSpan(index, copyLength));

      while ((copyLength << 1) < length)
      {
        System.Array.Copy(source, index, source, index + copyLength, copyLength);

        copyLength <<= 1;
      }

      System.Array.Copy(source, index, source, index + copyLength, length - copyLength);

      return source;
    }
  }
}
