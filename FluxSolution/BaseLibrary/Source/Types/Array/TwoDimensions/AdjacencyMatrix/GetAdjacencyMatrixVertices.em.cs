namespace Flux
{
  public static partial class ArrayRank2
  {
    public static System.Collections.Generic.IEnumerable<int> GetAdjacencyMatrixVertices<T>(this T[,] source)
      where T : System.IEquatable<T>
    {
      AssertAdjacencyMatrixProperty(source, out var length);

      for (var index = 0; index < length; index++)
        yield return index;
    }
  }
}
