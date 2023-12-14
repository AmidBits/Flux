namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Modifies <paramref name="source"/> by inserting <paramref name="count"/> of <typeparamref name="T"/> at <paramref name="index"/>.</para>
    /// </summary>
    private static void InsertInPlace<T>(ref T[] source, int index, int count)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (index < 0 || index > source.Length) throw new System.ArgumentOutOfRangeException(nameof(index));

      var sourceLength = source.Length;

      System.Array.Resize(ref source, sourceLength + count);

      if (sourceLength - index is var moveRight && moveRight > 0) // Move right-side, if needed.
        System.Array.Copy(source, index, source, index + count, moveRight);
    }

    /// <summary>
    /// <para>Modifies <paramref name="source"/> by inserting <paramref name="count"/> of <paramref name="value"/> at <paramref name="index"/>.</para>
    /// </summary>
    public static void InsertInPlace<T>(ref T[] source, int index, int count, T value)
    {
      InsertInPlace(ref source, index, count);

      System.Array.Fill(source, value, index, count);
    }

    /// <summary>
    /// <para>Modifies <paramref name="source"/> by inserting the elements of <paramref name="values"/> at <paramref name="index"/>.</para>
    /// </summary>
    public static void InsertInPlace<T>(ref T[] source, int index, params T[] values)
    {
      System.ArgumentNullException.ThrowIfNull(values);

      InsertInPlace(ref source, index, values.Length);

      System.Array.Copy(values, 0, source, index, values.Length);
    }
  }
}
