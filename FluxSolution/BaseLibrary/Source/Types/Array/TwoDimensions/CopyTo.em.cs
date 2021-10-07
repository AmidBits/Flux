namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class ArrayRank2
  {
    /// <summary>Fill the array with the specified value pattern, at the offset and count.</summary>
    public static void CopyTo<T>(this T[,] source, T[,] target, int sourceIndex0, int sourceIndex1, int targetIndex0, int targetIndex1, int count0, int count1)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (target is null) throw new System.ArgumentNullException(nameof(target));
      if (source.Rank != target.Rank) throw new System.ArgumentException($"Arrays of different rank ({source.Rank} vs {target.Rank}).", nameof(source));

      var sourceLength0 = source.GetLength(0);
      if (sourceIndex0 < 0 || sourceIndex0 >= sourceLength0) throw new System.ArgumentOutOfRangeException(nameof(sourceIndex0));

      var sourceLength1 = source.GetLength(1);
      if (sourceIndex1 < 0 || sourceIndex1 >= sourceLength1) throw new System.ArgumentOutOfRangeException(nameof(sourceIndex1));

      var targetLength0 = target.GetLength(0);
      if (targetIndex0 < 0 || targetIndex0 >= targetLength0) throw new System.ArgumentOutOfRangeException(nameof(targetIndex0));

      var targetLength1 = target.GetLength(1);
      if (targetIndex1 < 0 || targetIndex1 >= targetLength1) throw new System.ArgumentOutOfRangeException(nameof(targetIndex1));

      if (sourceIndex0 + count0 > sourceLength0 || targetIndex0 + count0 > targetLength0) throw new System.ArgumentOutOfRangeException(nameof(count0));
      if (sourceIndex1 + count1 > sourceLength1 || targetIndex1 + count1 > targetLength1) throw new System.ArgumentOutOfRangeException(nameof(count1));

      for (var c0 = count0 - 1; c0 >= 0; c0--)
        for (var c1 = count1 - 1; c1 >= 0; c1--)
          target[targetIndex0 + c0, targetIndex1 + c1] = source[sourceIndex0 + c0, sourceIndex1 + c1];
    }
  }
}
