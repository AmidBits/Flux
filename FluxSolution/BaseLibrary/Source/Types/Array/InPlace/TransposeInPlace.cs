namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class ArrayEm
  {
    /// <summary>Transpose the two-dimensional array, in-place. Both dimensions must be equal in length, i.e. it has to be a square two-dimensional array. The array is also returned.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Transpose"/>
#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
    public static void TransposeInPlace<T>(ref T[,] source)
#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var sourceLength0 = source.GetLength(0);
      var sourceLength1 = source.GetLength(1);

      if (sourceLength0 != sourceLength1) throw new System.ArgumentException("An in-place transpose require two dimensions of equal length.");

      for (int s0 = 0, sl0m2 = sourceLength0 - 2; s0 <= sl0m2; s0++)
      {
        for (int s1 = s0 + 1, sl1m1 = sourceLength1 - 1; s1 <= sl1m1; s1++)
        {
          var tmp = source[s1, s0];
          source[s1, s0] = source[s0, s1];
          source[s0, s1] = tmp;
        }
      }
    }
  }
}
