namespace Flux
{
  public static partial class ArrayRank1
  {
    /// <summary>Fill <paramref name="source"/> with <paramref name="count"/> of <paramref name="pattern"/> at <paramref name="index"/>.</summary>
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
