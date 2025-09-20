namespace Flux
{
  public static partial class Arrays
  {
    /// <summary>
    /// <para>Fill <paramref name="source"/> with the specified <paramref name="pattern"/>, at <paramref name="index0"/>, <paramref name="index1"/> and <paramref name="count0"/> and <paramref name="count1"/>. Using a sort of continuous flow fill.</para>
    /// </summary>
    /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
    public static void Fill<T>(this T[,] source, int index0, int index1, int count0, int count1, params System.ReadOnlySpan<T> pattern)
    {
      source.AssertRank(2);

      var length0 = source.GetLength(0);
      if (index0 < 0 || index0 > length0) throw new System.ArgumentOutOfRangeException(nameof(index0));

      var length1 = source.GetLength(1);
      if (index1 < 0 || index1 > length1) throw new System.ArgumentOutOfRangeException(nameof(index1));

      if (index0 + count0 > length0) throw new System.ArgumentOutOfRangeException(nameof(count0));
      if (index1 + count1 > length1) throw new System.ArgumentOutOfRangeException(nameof(count1));

      var index = 0;

      for (var i = 0; i < count0; i++)
        for (var j = 0; j < count1; j++)
          source[index0 + i, index1 + j] = pattern[index++ % pattern.Length];
    }

    public static void Fill0<T>(this T[,] source, int index, int count, params System.ReadOnlySpan<T> pattern)
      => source.Fill(index, 0, count, source.GetLength(1), pattern);

    public static void Fill1<T>(this T[,] source, int index, int count, params System.ReadOnlySpan<T> pattern)
      => source.Fill(0, index, source.GetLength(0), count, pattern);
  }
}
