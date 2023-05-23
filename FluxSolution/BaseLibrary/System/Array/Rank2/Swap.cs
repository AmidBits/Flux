namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class ArrayRank2
  {
    /// <summary>Swap two values in the two-dimensional array.</summary>
    public static void Swap<T>(T[,] source, int a0, int a1, int b0, int b1) 
      => (source[b0, b1], source[a0, a1]) = (source[a0, a1], source[b0, b1]);
  }
}
