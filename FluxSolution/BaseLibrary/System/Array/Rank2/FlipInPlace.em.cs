namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class Fx
  {
    /// <summary>Flip the order of the strands, in-place, along the specified <paramref name="dimension"/> in <paramref name="source"/>.</summary>
    public static void FlipInPlace<T>(this T[,] source, int dimension)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (source.Rank != 2) throw new System.ArgumentException($"Invalid rank ({source.Rank}).", nameof(source));

      var sourceLength0 = source.GetLength(0);
      var sourceLength1 = source.GetLength(1);

      switch (dimension)
      {
        case 0:
          var sl0m1 = sourceLength0 - 1;
          var sl0d2 = sourceLength0 / 2;
          for (var s1 = 0; s1 < sourceLength1; s1++)
            for (var s0 = 0; s0 <= sl0d2; s0++)
              Swap(source, s0, s1, sl0m1 - s0, s1);
          break;
        case 1:
          var sl1m1 = sourceLength1 - 1;
          var sl1d2 = sourceLength1 / 2;
          for (var s0 = 0; s0 < sourceLength0; s0++)
            for (var s1 = 0; s1 < sl1d2; s1++)
              Swap(source, s0, s1, s0, sl1m1 - s1);
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(dimension));
      }
    }
  }
}
