namespace Flux
{
  public static partial class ArrayRank2
  {
    private static int GetAdjacencyMatrixStateImpl<T>(this T[,] source, int keyFrom, int keyTo)
      where T : System.IEquatable<T>
      => source[keyFrom, keyTo] is var m && m.Equals(default!) ? 0 : keyFrom == keyTo ? 2 : 1;
    public static int GetAdjacencyMatrixState<T>(this T[,] source, int keySource, int keyTarget)
      where T : System.IEquatable<T>
    {
      AssertAdjacencyMatrixProperty(source, out var length);

      if (keySource < 0 || keySource >= length) throw new System.ArgumentOutOfRangeException(nameof(keySource));
      if (keyTarget < 0 || keyTarget >= length) throw new System.ArgumentOutOfRangeException(nameof(keyTarget));

      return GetAdjacencyMatrixStateImpl(source, keySource, keyTarget);
    }
  }
}
