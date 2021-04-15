namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class ArrayRank2
  {
    /// <summary>Reverse the order of all elements, in-place, along the specified dimension of the two-dimensional array. I.e. like mirror.</summary>
    public static void FlipInPlace<T>(ref T[,] source, int dimension)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (source.Rank != 2) throw new System.ArgumentException($"Invalid rank ({source.Rank}).", nameof(source));
      if (dimension < 0 || dimension > 1) throw new System.ArgumentOutOfRangeException(nameof(dimension));

      var sourceLength0 = source.GetLength(0);
      var sourceLength1 = source.GetLength(1);

      switch (dimension)
      {
        case 0: // Reverse dimension 0.
          int sl1m1 = sourceLength1 - 1, sl1d2 = sourceLength1 / 2;
          for (var s0 = 0; s0 < sourceLength0; s0++)
          {
            for (var s1 = 0; s1 < sl1d2; s1++)
            {
              var tmp = source[s0, s1];
              source[s0, s1] = source[s0, sl1m1 - s1];
              source[s0, sl1m1 - s1] = tmp;
            }
          }
          break;
        case 1: // Reverse dimension 1.
          int sl0m1 = sourceLength0 - 1, sl0d2 = sourceLength0 / 2;
          for (var s1 = 0; s1 < sourceLength1; s1++)
          {
            for (var s0 = 0; s0 <= sl0d2; s0++)
            {
              var tmp = source[s0, s1];
              source[s0, s1] = source[sl0m1 - s0, s1];
              source[sl0m1 - s0, s1] = tmp;
            }
          }
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(dimension));
      }
    }
  }
}
