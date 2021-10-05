namespace Flux
{
  public static partial class ArrayRank2
  {
    public static bool IsAdjacencyMatrixAdjacent<T>(this T[,] source, int keyFrom, int keyTo)
      where T : System.IEquatable<T>
    {
      AssertAdjacencyMatrixProperty(source, out var length);

      if (keyFrom < 0 || keyFrom >= length) throw new System.ArgumentOutOfRangeException(nameof(keyFrom));
      if (keyTo < 0 || keyTo >= length) throw new System.ArgumentOutOfRangeException(nameof(keyTo));

      return GetAdjacencyMatrixStateImpl(source, keyFrom, keyTo) == 1;
    }
  }
}
