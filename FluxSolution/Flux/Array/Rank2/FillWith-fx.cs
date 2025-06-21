namespace Flux
{
  public static partial class Arrays
  {
    /// <summary>
    /// <para>Fill <paramref name="source"/> with the specified <paramref name="pattern"/>, at <paramref name="index0"/>, <paramref name="index1"/> and <paramref name="count0"/> and <paramref name="count1"/>. Using a sort of continuous flow fill.</para>
    /// </summary>
    /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
    public static void FillWith<T>(this T[,] source, int index0, int index1, int count0, int count1, params System.ReadOnlySpan<T> pattern)
    {
      source.AssertRank(2);

      var length0 = source.GetLength(0);
      if (index0 < 0 || index0 > length0) throw new System.ArgumentOutOfRangeException(nameof(index0));

      var length1 = source.GetLength(1);
      if (index1 < 0 || index1 > length1) throw new System.ArgumentOutOfRangeException(nameof(index1));

      if (index0 + count0 > length0) throw new System.ArgumentOutOfRangeException(nameof(count0));
      if (index1 + count1 > length1) throw new System.ArgumentOutOfRangeException(nameof(count1));

      var index = 0;

      for (var c0 = 0; c0 < count0; c0++)
        for (var c1 = 0; c1 < count1; c1++)
          source[index0 + c0, index1 + c1] = pattern[index++ % pattern.Length];
    }
  }
}
