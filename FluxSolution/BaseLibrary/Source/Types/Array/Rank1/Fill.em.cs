namespace Flux
{
  public static partial class ArrayRank1
  {
    /// <summary>Fill the array with the specified value pattern, at the offset and count.</summary>
    public static T[] Fill<T>(this T[] source, int index, int count, params T[] pattern)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (pattern is null) throw new System.ArgumentNullException(nameof(pattern));

      var copyLength = System.Math.Min(pattern.Length, count);

      System.Array.Copy(pattern, 0, source, index, copyLength);

      while ((copyLength << 1) < count)
      {
        System.Array.Copy(source, index, source, index + copyLength, copyLength);

        copyLength <<= 1;
      }

      System.Array.Copy(source, index, source, index + copyLength, count - copyLength);

      return source;
    }
  }
}