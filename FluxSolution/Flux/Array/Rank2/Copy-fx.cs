namespace Flux
{
  public static partial class Arrays
  {
    /// <summary>
    /// <para>Copies <paramref name="count0"/> rows (dimension-0 elements) by <paramref name="count1"/> columns (dimension-1 elements), i.e. a block, from <paramref name="source"/> starting at [<paramref name="sourceIndex0"/>, <paramref name="sourceIndex1"/>] into <paramref name="target"/> starting at [<paramref name="targetIndex0"/>, <paramref name="targetIndex1"/>].</para>
    /// </summary>
    /// <remarks>Since an two-dimensional array is arbitrary in terms of its dimensions (e.g. rows and columns) and .NET is row-major order, the concept of [row, column] is adopted, i.e. dimension-0 = row, and dimension-1 = column.</remarks>
    public static void Copy<T>(this T[,] source, T[,] target, int sourceIndex0, int sourceIndex1, int targetIndex0, int targetIndex1, int count0, int count1)
    {
      for (var i = count0 - 1; i >= 0; i--)
        for (var j = count1 - 1; j >= 0; j--)
          target[targetIndex0 + i, targetIndex1 + j] = source[sourceIndex0 + i, sourceIndex1 + j];
    }

    /// <remarks>Since an two-dimensional array is arbitrary in terms of its dimensions (e.g. rows and columns) and .NET is row-major order, the concept of [row, column] is adopted, i.e. dimension-0 = row, and dimension-1 = column.</remarks>
    public static T[,] Copy0Insert<T>(this T[,] source, T[,]? target = null, params int[] indices0)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      target ??= source;

      var sl0 = source.GetLength(0);
      var sl1 = source.GetLength(1);

      var hs = indices0.Where(r => r < sl0).ToHashSet();

      for (int i = 0, k = 0; i < sl0; i++)
      {
        if (indices0.Contains(i + k))
          k++;

        for (var j = 0; j < sl1; j++) // All dimension 1 elements are always copied.
          target[i + k, j] = source[i, j];
      }

      return target;
    }

    /// <remarks>Since an two-dimensional array is arbitrary in terms of its dimensions (e.g. rows and columns) and .NET is row-major order, the concept of [row, column] is adopted, i.e. dimension-0 = row, and dimension-1 = column.</remarks>
    public static T[,] Copy1Insert<T>(this T[,] source, T[,]? target = null, params int[] indices1)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      target ??= source;

      var sl0 = source.GetLength(0);
      var sl1 = source.GetLength(1);

      var hs = indices1.Where(r => r < sl1).ToHashSet();

      for (var i = 0; i < sl0; i++) // All dimension 0 elements are always copied.
      {
        for (int j = 0, k = 0; j < sl1; j++)
        {
          if (indices1.Contains(j + k))
            k++;

          target[i, j + k] = source[i, j];
        }
      }

      return target;
    }

    /// <summary>
    /// <para>Copy all dimension-0 elements (i.e. rows) from <paramref name="source"/> into <paramref name="target"/> except the specified <paramref name="indices0"/> (rows).</para>
    /// </summary>
    /// <remarks>Since an two-dimensional array is arbitrary in terms of its dimensions (e.g. rows and columns) and .NET is row-major order, the concept of [row, column] is adopted, i.e. dimension-0 = row, and dimension-1 = column.</remarks>
    public static T[,] Copy0Remove<T>(this T[,] source, T[,]? target = null, params int[] indices0)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      target ??= source;

      var sl0 = source.GetLength(0);
      var sl1 = source.GetLength(1);

      var hs = indices0.Where(r => r < sl0).ToHashSet();

      for (int i = 0, k = 0; i < sl0; i++)
        if (!indices0.Contains(i))
        {
          for (var j = 0; j < sl1; j++) // All dimension 1 elements are always copied.
            target[k, j] = source[i, j];

          k++;
        }

      return target;
    }

    /// <summary>
    /// <para>Copy all dimension-1 elements (i.e. columns) from <paramref name="source"/> into <paramref name="target"/> except the specified <paramref name="indices1"/> (columns).</para>
    /// <para></para>
    /// </summary>
    /// <remarks>Since an two-dimensional array is arbitrary in terms of its dimensions (e.g. rows and columns) and .NET is row-major order, the concept of [row, column] is adopted, i.e. dimension-0 = row, and dimension-1 = column.</remarks>
    public static T[,] Copy1Remove<T>(this T[,] source, T[,]? target = null, params int[] indices1)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      target ??= source;

      var sl0 = source.GetLength(0);
      var sl1 = source.GetLength(1);

      var hs = indices1.Where(r => r < sl1).ToHashSet();

      for (var i = 0; i < sl0; i++) // All dimension 0 elements are always copied.
        for (int j = 0, k = 0; j < sl1; j++)
          if (!indices1.Contains(j))
            target[i, k++] = source[i, j];

      return target;
    }

    /// <summary>
    /// <para>Copies <paramref name="count"/> rows (dimension-0 strands) from <paramref name="source"/> starting at <paramref name="sourceIndex"/> into <paramref name="target"/> starting at <paramref name="targetIndex"/>.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="sourceIndex"></param>
    /// <param name="target"></param>
    /// <param name="targetIndex"></param>
    /// <param name="count"></param>
    /// <remarks>Since an two-dimensional array is arbitrary in terms of its dimensions (e.g. rows and columns) and .NET is row-major order, the concept of [row, column] is adopted, i.e. dimension-0 = row, and dimension-1 = column.</remarks>
    public static void Copy0Range<T>(this T[,] source, int sourceIndex, T[,] target, int targetIndex, int count)
      => source.Copy(target, sourceIndex, 0, targetIndex, 0, count, source.GetLength(1));

    /// <summary>
    /// <para>Copies <paramref name="count"/> columns (dimension-1 strands) from <paramref name="source"/> starting at <paramref name="sourceIndex"/> into <paramref name="target"/> starting at <paramref name="targetIndex"/>.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="sourceIndex"></param>
    /// <param name="target"></param>
    /// <param name="targetIndex"></param>
    /// <param name="count"></param>
    /// <remarks>Since an two-dimensional array is arbitrary in terms of its dimensions (e.g. rows and columns) and .NET is row-major order, the concept of [row, column] is adopted, i.e. dimension-0 = row, and dimension-1 = column.</remarks>
    public static void Copy1Range<T>(this T[,] source, int sourceIndex, T[,] target, int targetIndex, int count)
      => source.Copy(target, 0, sourceIndex, 0, targetIndex, source.GetLength(0), count);
  }
}
