namespace Flux
{
  public static partial class ArrayRank1
  {
    private static void InsertImpl<T>(ref T[] source, int index, int count)
    {
      var sourceLength = source.Length;

      System.Array.Resize(ref source, sourceLength + count);

      if (sourceLength - index is var moveRight && moveRight > 0) // Move right-side, if needed.
        System.Array.Copy(source, index, source, index + count, moveRight);
    }

    /// <summary>Modifies the <paramref name="source"/> by inserting the specified number (<paramref name="count"/>) of default elements at the <paramref name="index"/>.</summary>
    public static void Insert<T>(ref T[] source, int index, int count)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (index < 0 || index > source.Length) throw new System.ArgumentOutOfRangeException(nameof(index));
      InsertImpl(ref source, index, count);
      System.Array.Clear(source, index, count);
    }
    /// <summary>Modifies the <paramref name="source"/> by inserting the specified number (<paramref name="count"/>) of <paramref name="value"/> elements at the <paramref name="index"/>.</summary>
    public static void Insert<T>(ref T[] source, int index, int count, T value)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (index < 0 || index > source.Length) throw new System.ArgumentOutOfRangeException(nameof(index));
      InsertImpl(ref source, index, count);
      System.Array.Fill(source, value, index, count);
    }
    /// <summary>Modifies the <paramref name="source"/> by inserting the <paramref name="insert"/> array at the <paramref name="index"/>.</summary>
    public static void Insert<T>(ref T[] source, int index, params T[] insert)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (index < 0 || index > source.Length) throw new System.ArgumentOutOfRangeException(nameof(index));
      if (insert is null) throw new System.ArgumentNullException(nameof(insert));
      InsertImpl(ref source, index, insert.Length);
      System.Array.Copy(insert, 0, source, index, insert.Length);
    }
  }
}
