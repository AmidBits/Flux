namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Modifies <paramref name="source"/> by inserting <paramref name="count"/> of <typeparamref name="T"/> at <paramref name="index"/>.</summary>
    private static void Insert<T>(ref T[] source, int index, int count)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (index < 0 || index > source.Length) throw new System.ArgumentOutOfRangeException(nameof(index));

      var sourceLength = source.Length;

      System.Array.Resize(ref source, sourceLength + count);

      if (sourceLength - index is var moveRight && moveRight > 0) // Move right-side, if needed.
        System.Array.Copy(source, index, source, index + count, moveRight);
    }

    /// <summary>Modifies <paramref name="source"/> by inserting <paramref name="count"/> of <paramref name="value"/> at <paramref name="index"/>.</summary>
    public static void Insert<T>(ref T[] source, int index, int count, T value)
    {
      Insert(ref source, index, count);

      System.Array.Fill(source, value, index, count);
    }

    /// <summary>Modifies <paramref name="source"/> by inserting the elements of <paramref name="values"/> at <paramref name="index"/>.</summary>
    public static void Insert<T>(ref T[] source, int index, params T[] values)
    {
      if (values is null) throw new System.ArgumentNullException(nameof(values));

      Insert(ref source, index, values.Length);

      System.Array.Copy(values, 0, source, index, values.Length);
    }
  }
}
