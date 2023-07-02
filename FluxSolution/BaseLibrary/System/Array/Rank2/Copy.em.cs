namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class ArrayRank2
  {
    /// <summary>Copies a selection of (<paramref name="count0"/>, <paramref name="count1"/>) elements from <paramref name="source"/> starting at [<paramref name="sourceIndex0"/>, <paramref name="sourceIndex1"/>] to <paramref name="target"/> starting at [<paramref name="targetIndex0"/>, <paramref name="targetIndex1"/>].</summary>
    public static void Copy<T>(this T[,] source, T[,] target, int sourceIndex0, int sourceIndex1, int targetIndex0, int targetIndex1, int count0, int count1)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (target is null) throw new System.ArgumentNullException(nameof(target));
      if (source.Rank != target.Rank) throw new System.ArgumentException($"Arrays of different rank ({source.Rank} vs {target.Rank}).", nameof(source));

      if (source.GetLength(0) is var sourceLength0 && sourceIndex0 < 0 || sourceIndex0 >= sourceLength0) throw new System.ArgumentOutOfRangeException(nameof(sourceIndex0));
      if (source.GetLength(1) is var sourceLength1 && sourceIndex1 < 0 || sourceIndex1 >= sourceLength1) throw new System.ArgumentOutOfRangeException(nameof(sourceIndex1));
      if (target.GetLength(0) is var targetLength0 && targetIndex0 < 0 || targetIndex0 >= targetLength0) throw new System.ArgumentOutOfRangeException(nameof(targetIndex0));
      if (target.GetLength(1) is var targetLength1 && targetIndex1 < 0 || targetIndex1 >= targetLength1) throw new System.ArgumentOutOfRangeException(nameof(targetIndex1));

      if (sourceIndex0 + count0 > sourceLength0 || targetIndex0 + count0 > targetLength0) throw new System.ArgumentOutOfRangeException(nameof(count0));
      if (sourceIndex1 + count1 > sourceLength1 || targetIndex1 + count1 > targetLength1) throw new System.ArgumentOutOfRangeException(nameof(count1));

      for (var c0 = count0 - 1; c0 >= 0; c0--)
        for (var c1 = count1 - 1; c1 >= 0; c1--)
          target[targetIndex0 + c0, targetIndex1 + c1] = source[sourceIndex0 + c0, sourceIndex1 + c1];
    }
  }
}
