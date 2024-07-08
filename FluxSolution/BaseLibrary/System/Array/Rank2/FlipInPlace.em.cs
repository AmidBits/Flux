namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Flip the order of the strands, in-place, along the specified <paramref name="dimension"/> in <paramref name="source"/>.</para>
    /// </summary>
    /// <remarks>An array is arbitrary in terms of rows and columns, we simply adopt the concept of considering dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
    public static void FlipInPlace<T>(this T[,] source, int dimension)
    {
      source.ThrowIfUnequalRank(2);

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

    /// <summary>
    /// <para>Flip the order of the strands, in-place, along the specified <paramref name="dimension"/> in <paramref name="source"/>.</para>
    /// </summary>
    /// <remarks>An array is arbitrary in terms of e.g. rows and columns. We simply adopt a view based on <see cref="Flux.ArrayDimension" /> (i.e. 0 = row, and 1 = column).</remarks>
    public static void FlipInPlace<T>(this T[,] source, ArrayDimension dimension) => source.FlipInPlace((int)dimension);
  }
}
