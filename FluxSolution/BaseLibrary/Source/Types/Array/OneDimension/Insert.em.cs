namespace Flux
{
  public static partial class ArrayRank1
  {
    private static T[] InsertImpl<T>(this T[] source, int index, int count)
    {
      var sourceLength = source.Length;

      var targetLength = sourceLength + count;
      var target = new T[targetLength];

      if (index > 0) // Copy left-side, if needed.
        System.Array.Copy(source, 0, target, 0, index);

      var endIndex = index + count;

      if (endIndex < targetLength) // Copy right-side, if needed.
        System.Array.Copy(source, index, target, index + count, targetLength - endIndex);

      return target;
    }

    /// <summary>Create a new array with the specified number (<paramref name="count"/>) of elements inserted at the <paramref name="index"/>.</summary>
    public static T[] Insert<T>(this T[] source, int index, int count)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (index < 0 || index > source.Length) throw new System.ArgumentOutOfRangeException(nameof(index));
      var target = InsertImpl(source, index, count);
      System.Array.Clear(target, index, count);
      return target;
    }
    /// <summary>Create a new array with the specified number (<paramref name="count"/>) of <paramref name="value"/> inserted at the <paramref name="index"/>.</summary>
    public static T[] Insert<T>(this T[] source, int index, int count, T value)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (index < 0 || index > source.Length) throw new System.ArgumentOutOfRangeException(nameof(index));
      var target = InsertImpl(source, index, count);
      System.Array.Fill(target, value, index, count);
      return target;
    }
    /// <summary>Create a new array with the specified <paramref name="insert"/> array inserted at the <paramref name="index"/>.</summary>
    public static T[] Insert<T>(this T[] source, int index, params T[] insert)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (index < 0 || index > source.Length) throw new System.ArgumentOutOfRangeException(nameof(index));
      if (insert is null) throw new System.ArgumentNullException(nameof(insert));
      var target = InsertImpl(source, index, insert.Length);
      System.Array.Copy(insert, 0, target, index, insert.Length);
      return target;
    }
  }
}
