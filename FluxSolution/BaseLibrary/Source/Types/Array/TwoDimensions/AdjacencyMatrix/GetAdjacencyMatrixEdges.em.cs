namespace Flux
{
  public static partial class ArrayRank2
  {
    public static System.Collections.Generic.IEnumerable<(int keySource, int keyTarget, T value)> GetAdjacencyMatrixEdges<T>(this T[,] source)
      where T : System.IEquatable<T>
    {
      AssertAdjacencyMatrixProperty(source, out var length);

      for (var si = 0; si < length; si++)
        for (var ti = 0; ti < length; ti++)
          if (source[si, ti] is var m && !m.Equals(default!))
            yield return (si, ti, m);
    }
  }
}
