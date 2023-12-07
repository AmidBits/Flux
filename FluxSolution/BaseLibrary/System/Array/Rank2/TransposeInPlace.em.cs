namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class Fx
  {
    /// <summary>Transpose <paramref name="source"/>, in-place. Both dimensions must be equal in length, i.e. it has to be a square two-dimensional array.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Transpose"/>
    public static void TransposeInPlace<T>(this T[,] source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (source.Rank != 2) throw new System.ArgumentException($"Invalid rank ({source.Rank}).", nameof(source));

      var sourceLength0 = source.GetLength(0);
      var sourceLength1 = source.GetLength(1);

      if (sourceLength0 != sourceLength1) throw new System.ArgumentException("In-place transposition requires dimensions of equal length.");

      var sl0m2 = sourceLength0 - 2;
      var sl1m1 = sourceLength1 - 1;

      for (var s0 = 0; s0 <= sl0m2; s0++)
        for (var s1 = s0 + 1; s1 <= sl1m1; s1++)
          Swap(source, s1, s0, s0, s1);
    }
  }
}
