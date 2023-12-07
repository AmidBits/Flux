namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class Fx
  {
    /// <summary>Create a new jagged array (a single-dimension array of one-dimensional arrays) with all elements from <paramref name="source"/> in <paramref name="dimension"/>-major order (by rows or by column).</summary>
    public static T[][] ToJaggedArray<T>(this T[,] source, int dimension)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (source.Rank != 2) throw new System.ArgumentException($"Invalid rank ({source.Rank}).", nameof(source));
      if (dimension < 0 || dimension > 1) throw new System.ArgumentOutOfRangeException(nameof(dimension));

      var target = new T[source.GetLength(dimension)][];

      var sourceLength0 = source.GetLength(0);
      var sourceLength1 = source.GetLength(1);

      switch (dimension)
      {
        case 0:
          for (int s0 = 0; s0 < sourceLength0; s0++)
          {
            var jaggedDimension = new T[sourceLength1];
            for (var s1 = 0; s1 < sourceLength1; s1++)
              jaggedDimension[s1] = source[s0, s1];
            target[s0] = jaggedDimension;
          }
          break;
        case 1:
          for (var s1 = 0; s1 < sourceLength1; s1++)
          {
            var jaggedDimension = new T[sourceLength0];
            for (var s0 = 0; s0 < sourceLength0; s0++)
              jaggedDimension[s0] = source[s0, s1];
            target[s1] = jaggedDimension;
          }
          break;
        default:
          throw new System.ArgumentOutOfRangeException(nameof(dimension));
      }

      return target;
    }
  }
}
