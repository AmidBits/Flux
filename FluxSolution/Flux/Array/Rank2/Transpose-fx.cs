namespace Flux
{
  public static partial class Arrays
  {
    /// <summary>
    /// <para>Transpose all elements in <paramref name="source"/> into target.</para>
    /// <para>If <paramref name="target"/> is different than <paramref name="source"/>, a copy to <paramref name="target"/> is performed. If <paramref name="target"/> is the same as <paramref name="source"/>, or <see langword="null"/>, a <paramref name="source"/> in-place is performed.</para>
    /// <see href="https://en.wikipedia.org/wiki/Transpose"/>
    /// </summary>
    /// <remarks>Since an two-dimensional array is arbitrary in terms of its dimensions (e.g. rows and columns) and .NET is row-major order, the concept of [row, column] is adopted, i.e. dimension-0 = row, and dimension-1 = column.</remarks>
    public static void Transpose<T>(this T[,] source, T[,]? target = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      target ??= source;

      var sl0 = source.GetLength(0);
      var sl1 = source.GetLength(1);

      if (target.GetLength(0) < sl1 || target.GetLength(1) < sl0)
        throw new System.ArgumentException($"Array transposition requires target minimum dimensional symmetry.");

      for (var i = 0; i < sl0; i++)
        for (var j = i; j < sl1; j++)
        {
          var tmp = source[i, j];
          target[i, j] = source[j, i];
          target[j, i] = tmp;
        }
    }
  }
}
