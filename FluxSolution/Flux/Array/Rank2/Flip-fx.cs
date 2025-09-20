namespace Flux
{
  public static partial class Arrays
  {
    /// <summary>
    /// <para>Flip the order of all dimension-0 strands (i.e. rows) from <paramref name="source"/> into <paramref name="target"/>.</para>
    /// <para>If <paramref name="target"/> is different than <paramref name="source"/>, a copy to <paramref name="target"/> is performed. If <paramref name="target"/> is the same as <paramref name="source"/>, or <see langword="null"/>, a <paramref name="source"/> in-place is performed.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <exception cref="System.ArgumentException"></exception>
    /// <remarks>Since an two-dimensional array is arbitrary in terms of its dimensions (e.g. rows and columns) and .NET is row-major order, the concept of [row, column] is adopted, i.e. dimension-0 = row, and dimension-1 = column.</remarks>
    public static void Flip0<T>(this T[,] source, T[,]? target = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      target ??= source;

      var sl0 = source.GetLength(0);
      var sl1 = source.GetLength(1);

      if (target.GetLength(0) < sl1 || target.GetLength(1) < sl0)
        throw new System.ArgumentException($"Array flip requires target minimum dimensional symmetry.");

      var sl0sub1 = sl0 - 1;
      var sl0half = sl0 / 2;

      for (var i = 0; i < sl1; i++)
        for (var j = 0; j < sl0half; j++)
        {
          var tmp = source[j, i];
          target[j, i] = source[sl0sub1 - j, i];
          target[sl0sub1 - j, i] = tmp;
        }
    }

    /// <summary>
    /// <para>Flip the order of all dimension-1 strands (i.e. columns) from <paramref name="source"/> into <paramref name="target"/>.</para>
    /// <para>If <paramref name="target"/> is different than <paramref name="source"/>, a copy to <paramref name="target"/> is performed. If <paramref name="target"/> is the same as <paramref name="source"/>, or <see langword="null"/>, a <paramref name="source"/> in-place is performed.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <exception cref="System.ArgumentException"></exception>
    /// <remarks>Since an two-dimensional array is arbitrary in terms of its dimensions (e.g. rows and columns) and .NET is row-major order, the concept of [row, column] is adopted, i.e. dimension-0 = row, and dimension-1 = column.</remarks>
    public static void Flip1<T>(this T[,] source, T[,]? target = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      target ??= source;

      var sl0 = source.GetLength(0);
      var sl1 = source.GetLength(1);

      if (target.GetLength(0) < sl1 || target.GetLength(1) < sl0)
        throw new System.ArgumentException($"Array flip requires target minimum dimensional symmetry.");

      var sl1sub1 = sl1 - 1;
      var sl1half = sl1 / 2;

      for (var i = 0; i < sl0; i++)
        for (var j = 0; j < sl1half; j++)
        {
          var tmp = source[i, j];
          target[i, j] = source[i, sl1sub1 - j];
          target[i, sl1sub1 - j] = tmp;
        }
    }
  }
}
