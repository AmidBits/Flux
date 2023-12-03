namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class Fx
  {
    /// <summary>Measures all dimensions, if all equal in length sets the out argument and returns whether they are equal.</summary>
    public static bool TryGetSymmetricalLength<T>(this T[,] source, out int length)
    {
      if (source is null)
      {
        length = -1;
        return false;
      }

      length = source.GetLength(1);
      return System.Linq.Enumerable.Range(0, source.Rank).Select(source.GetLength).ToHashSet().Count == 1;
    }
  }
}
