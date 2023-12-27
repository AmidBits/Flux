namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>
    /// <para>Create a new array with all elements from <paramref name="source"/> and <paramref name="count"/> elements inserted at <paramref name="index"/>.</para>
    /// </summary>
    private static T[] InsertToCopy<T>(this T[] source, int index, int count)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (index < 0 || index > source.Length) throw new System.ArgumentOutOfRangeException(nameof(index));

      var targetLength = source.Length + count; // Pre-compute for multi-use.

      var target = new T[targetLength];

      if (index > 0) // Copy left-side, if needed.
        System.Array.Copy(source, 0, target, 0, index);

      var endIndex = index + count;

      if (endIndex < targetLength) // Copy right-side, if needed.
        System.Array.Copy(source, index, target, index + count, targetLength - endIndex);

      return target;
    }

    /// <summary>
    /// <para>Create a new array with all elements from <paramref name="source"/> and <paramref name="count"/> instances of <paramref name="value"/> inserted at <paramref name="index"/>.</para>
    /// </summary>
    public static T[] InsertToCopy<T>(this T[] source, int index, int count, T value)
    {
      var target = InsertToCopy(source, index, count);
      System.Array.Fill(target, value, index, count);
      return target;
    }
    /// <summary>
    /// <para>Create a new array with all elements from <paramref name="source"/> and the elements of <paramref name="values"/> inserted at <paramref name="index"/>.</para>
    /// </summary>
    public static T[] InsertToCopy<T>(this T[] source, int index, params T[] values)
    {
      if (values is null) throw new System.ArgumentNullException(nameof(values));

      var target = InsertToCopy(source, index, values.Length);
      System.Array.Copy(values, 0, target, index, values.Length);
      return target;
    }
  }
}
