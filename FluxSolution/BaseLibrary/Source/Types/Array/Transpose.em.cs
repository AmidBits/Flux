namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class ArrayEm
  {
    /// <summary>Create a new transposed two dimensional array from the source, i.e. with rows as columns and columns as rows.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Transpose"/>
#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
    public static T[,] Transpose<T>(this T[,] source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var sourceLength0 = source.GetLength(0);
      var sourceLength1 = source.GetLength(1);

      var target = new T[source.GetLength(1), source.GetLength(0)]; // Swap the length of the dimensions.

      for (var s0 = 0; s0 < sourceLength0; s0++)
        for (var s1 = 0; s1 < sourceLength1; s1++)
          target[s1, s0] = source[s0, s1];

      return target;
    }
#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional
  }
}
