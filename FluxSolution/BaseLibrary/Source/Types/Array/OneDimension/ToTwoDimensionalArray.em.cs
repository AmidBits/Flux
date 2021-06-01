namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class ArrayJagged
  {
    /// <summary>Creates a new two-dimensional array with the specified sizes, and then fills the target (from the source) in a 'dimension 0'-major order.</summary>
    public static T[,] ToTwoDimensionalArray<T>(this T[] source, int length0, int length1)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var target = new T[length0, length1];

      for (var i0 = 0; i0 < length0; i0++)
        for (var i1 = 0; i1 < length1; i1++)
          target[i0, i1] = source[i0 * length0 + i1]; // 'Dimension 0'-major order.

      return target;
    }
  }
}
