namespace Flux
{
  public static partial class ArrayRank2
  {
    public static System.Collections.Generic.IEnumerable<int> GetAdjacencyMatrixNeighbors<T>(this T[,] source, int key)
      where T : System.IEquatable<T>
    {
      AssertAdjacencyMatrixProperty(source, out var length);

      if (key < 0 || key >= length) throw new System.ArgumentOutOfRangeException(nameof(key));

      for (var index = 0; index < length; index++)
        if (GetAdjacencyMatrixStateImpl(source, key, index) > 0)
          yield return index;
    }
  }
}
