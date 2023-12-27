namespace Flux
{
  public static partial class Reflection
  {
    public static System.Collections.Generic.IEnumerable<int> GraphGetNeighbors<T>(this T[,] source, int key)
      where T : System.IEquatable<T>
    {
      GraphAssertProperty(source, out var length);

      if (key < 0 || key >= length) throw new System.ArgumentOutOfRangeException(nameof(key));

      for (var index = 0; index < length; index++)
        if (GraphGetStateImpl(source, key, index) > 0)
          yield return index;
    }
  }
}
