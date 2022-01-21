namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class ArrayRank2
  {
    /// <summary>Create a new transposed two dimensional array from the source, i.e. switch rows for columns.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Transpose"/>
    public static T[,] TransposeToCopy<T>(this T[,] source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (source.Rank != 2) throw new System.ArgumentException($"Invalid rank ({source.Rank}).", nameof(source));

      var sourceLength0 = source.GetLength(0);
      var sourceLength1 = source.GetLength(1);

      var target = new T[sourceLength1, sourceLength0]; // Swap the length of the dimensions.

      for (var s0 = 0; s0 < sourceLength0; s0++)
        for (var s1 = 0; s1 < sourceLength1; s1++)
          target[s1, s0] = source[s0, s1];

      return target;
    }
  }
}
