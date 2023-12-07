namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Fill <paramref name="source"/> with <paramref name="count"/> of <paramref name="pattern"/> at <paramref name="index"/>.</summary>
    public static T[] Fill<T>(this T[] source, int index, int count, params T[] pattern)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentNullException.ThrowIfNull(pattern);

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
