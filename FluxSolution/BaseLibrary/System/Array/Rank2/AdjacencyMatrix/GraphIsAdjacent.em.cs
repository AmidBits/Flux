namespace Flux
{
  public static partial class Reflection
  {
    public static bool GraphIsAdjacent<T>(this T[,] source, int keyFrom, int keyTo)
      where T : System.IEquatable<T>
    {
      GraphAssertProperty(source, out var length);

      if (keyFrom < 0 || keyFrom >= length) throw new System.ArgumentOutOfRangeException(nameof(keyFrom));
      if (keyTo < 0 || keyTo >= length) throw new System.ArgumentOutOfRangeException(nameof(keyTo));

      return GraphGetStateImpl(source, keyFrom, keyTo) == 1;
    }
  }
}
