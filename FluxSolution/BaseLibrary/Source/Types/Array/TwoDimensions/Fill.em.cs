namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class ArrayRank2
  {
    /// <summary>Fill the array with the specified pattern, at the indices and counts.</summary>
    public static void Fill<T>(this T[,] source, int index0, int index1, int count0, int count1, params T[] pattern)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (source.Rank != 2) throw new System.ArgumentException($"Invalid rank ({source.Rank}).", nameof(source));

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
