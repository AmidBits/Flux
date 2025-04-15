namespace Flux
{
  public static partial class Arrays
  {
    // Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.

    /// <summary>
    /// <para>Asserts that the <paramref name="source"/> array has symmetrical dimensions, i.e. all dimensions are the same length.</para>
    /// </summary>
    /// <param name="source">The array.</param>
    /// <param name="symmetricalLength"></param>
    /// <param name="paramName"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentException"></exception>
    public static System.Array AssertDimensionallySymmetrical(this System.Array source, out int symmetricalLength, string? paramName = null)
      => source.IsDimensionallySymmetrical(out symmetricalLength) ? source : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(source), "All array dimensions must be equal in length.");

    /// <summary>
    /// <para>Determines whether the <paramref name="source"/> array has symmetrical dimensions, i.e. all dimensions are the same length.</para>
    /// </summary>
    /// <param name="source">The array.</param>
    /// <param name="symmetricalLength"></param>
    /// <returns></returns>
    public static bool IsDimensionallySymmetrical(this System.Array source, out int symmetricalLength)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      symmetricalLength = source.GetLength(0); // Load the first dimensional length.

      if (IsJaggedArray(source))
      {
        for (var index = symmetricalLength - 1; index > 0; index--)
          if (source.GetValue(index) is System.Array array && (array is null || array.GetLength(0) != symmetricalLength))
            return false;
      }
      else
      {
        for (var index = source.Rank - 1; index > 0; index--)
          if (source.GetLength(index) != symmetricalLength)
            return false;
      }

      return true;
    }

    /// <summary>
    /// <para>Measures all dimensions, if all equal in length sets the out argument and returns whether they are equal.</para>
    /// </summary>
    /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
    /// <param name="source"></param>
    /// <param name="symmetricalLength"></param>
    /// <returns></returns>
    public static bool TryGetDimensionallySymmetricalLength(this System.Array source, out int symmetricalLength)
    {
      try
      {
        if (source.IsDimensionallySymmetrical(out symmetricalLength))
          return true;
      }
      catch { }

      symmetricalLength = -1;
      return false;
    }
  }
}
