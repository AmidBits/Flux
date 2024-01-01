namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Measures all dimensions, if all equal in length sets the out argument and returns whether they are equal.</para>
    /// </summary>
    /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
    public static bool TryGetSymmetricalLength<T>(this T[,] source, out int length)
    {
      try
      {
        if (source is not null && source.Rank > 1)
        {
          var hs = System.Linq.Enumerable.Range(0, source.Rank).Select(source.GetLength).ToHashSet();

          length = hs.Single();
          return true;
        }
      }
      catch { }

      length = -1;
      return false;
    }
  }
}
