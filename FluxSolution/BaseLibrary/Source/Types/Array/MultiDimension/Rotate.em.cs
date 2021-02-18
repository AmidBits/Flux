namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class ArrayRank2
  {
    /// <summary>Create a new two dimensional array from the source, with the elements rotated clockwise.</summary>
#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
    public static T[,] RotateClockwise<T>(this T[,] source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var sourceLength0 = source.GetLength(0);
      var sourceLength1 = source.GetLength(1);

      var target = new T[sourceLength1, sourceLength0]; // Swap the length of the dimensions.

      var sl0m1 = sourceLength0 - 1;

      for (var s0 = 0; s0 < sourceLength0; s0++)
        for (var s1 = 0; s1 < sourceLength1; s1++)
          target[s1, sl0m1 - s0] = source[s0, s1];

      return target;
    }
#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional

    /// <summary>Create a new two dimensional array from the source, with the elements rotated counter-clockwise.</summary>
#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
    public static T[,] RotateCounterClockwise<T>(this T[,] source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var sourceLength0 = source.GetLength(0);
      var sourceLength1 = source.GetLength(1);

      var target = new T[sourceLength1, sourceLength0]; // Swap the length of the dimensions.

      var sl1m1 = sourceLength1 - 1;

      for (var s0 = 0; s0 < sourceLength0; s0++)
        for (var s1 = 0; s1 < sourceLength1; s1++)
          target[sl1m1 - s1, s0] = source[s0, s1];

      return target;
    }
#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional
  }
}
