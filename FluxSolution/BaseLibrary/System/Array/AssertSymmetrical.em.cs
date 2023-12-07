namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class Fx
  {
    /// <summary>Asserts that the <paramref name="source"/> array is symmetrical, i.e. all dimensions are the same length.</summary>
    /// <param name="source">The array.</param>
    /// <param name="paramName">Optional name of the parameter being asserted.</param>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Array AssertSymmetrical(this System.Array source, string? paramName = null)
      => source.IsSymmetrical() ? source : throw new System.ArgumentException("The array is not symmetrical.", paramName ?? nameof(source));

    /// <summary>Determines whether the <paramref name="source"/> array is symmetrical, i.e. all dimensions are the same length.</summary>
    /// <param name="source">The array.</param>
    /// <returns></returns>
    public static bool IsSymmetrical(this System.Array source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var sourceLength = source.GetLength(0); // Load the first dimensional length.

      if (IsJaggedArray(source))
      {
        for (var index = sourceLength - 1; index > 0; index--)
          if (source.GetValue(index) is System.Array array && (array is null || array.GetLength(0) != sourceLength))
            return false;
      }
      else
      {
        for (var index = source.Rank - 1; index > 0; index--)
          if (source.GetLength(index) != sourceLength)
            return false;
      }

      return true;
    }
  }
}
