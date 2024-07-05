namespace Flux
{
  public static partial class Fx
  {
    // Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.

    /// <summary>
    /// <para>Asserts that the <paramref name="source"/> array is symmetrical, i.e. all dimensions are the same length.</para>
    /// </summary>
    /// <param name="source">The array.</param>
    /// <param name="paramName">Optional name of the parameter being asserted.</param>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Array AssertSymmetrical(this System.Array source, string? paramName = null)
      => source.IsSymmetrical() ? source : throw new System.ArgumentException("The array is not symmetrical.", paramName ?? nameof(source));

    /// <summary>
    /// <para>Determines whether the <paramref name="source"/> array is symmetrical, i.e. all dimensions are the same length.</para>
    /// </summary>
    /// <param name="source">The array.</param>
    /// <returns></returns>
    public static bool IsSymmetrical(this System.Array source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var sourceLength0 = source.GetLength(0); // Load the first dimensional length.

      if (IsJaggedArray(source))
      {
        for (var index = sourceLength0 - 1; index > 0; index--)
          if (source.GetValue(index) is System.Array array && (array is null || array.GetLength(0) != sourceLength0))
            return false;
      }
      else
      {
        for (var index = source.Rank - 1; index > 0; index--)
          if (source.GetLength(index) != sourceLength0)
            return false;
      }

      return true;
    }

    /// <summary>
    /// <para>Assert <paramref name="source"/> as a two-dimensional array.</para>
    /// </summary>
    public static System.Array AssertTwoDimensional<T>(this System.Array source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (source.Rank != 2)
        throw new System.ArgumentException($"Invalid rank ({source.Rank}).", nameof(source));

      return source;
    }
  }
}
