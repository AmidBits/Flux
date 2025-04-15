namespace Flux
{
  public static partial class Arrays
  {
    /// <summary>If the adjacency matrix value yields the default(T) value, state = 0, otherwise, if the from/to indices are the same. state = 2. and if they are different, state = 1. This "Impl" version bypass argument checks.</summary>
    private static int GraphGetStateImpl<T>(this T[,] source, int keyFrom, int keyTo)
      where T : System.IEquatable<T>
      => source[keyFrom, keyTo] is var m && m.Equals(default!) ? 0 : keyFrom == keyTo ? 2 : 1;

    /// <summary>If the adjacency matrix value yields the default(T) value, state = 0, otherwise, if the from/to indices are the same. state = 2. and if they are different, state = 1. This version performs all argument checks.</summary>
    public static int GraphGetState<T>(this T[,] source, int keySource, int keyTarget)
      where T : System.IEquatable<T>
    {
      GraphAssertProperty(source, out var length);

      if (keySource < 0 || keySource >= length) throw new System.ArgumentOutOfRangeException(nameof(keySource));
      if (keyTarget < 0 || keyTarget >= length) throw new System.ArgumentOutOfRangeException(nameof(keyTarget));

      return GraphGetStateImpl(source, keySource, keyTarget);
    }
  }
}
