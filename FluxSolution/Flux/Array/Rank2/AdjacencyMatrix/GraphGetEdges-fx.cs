namespace Flux
{
  public static partial class Arrays
  {
    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="isEdgePredicate"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<(int keySource, int keyTarget, T value)> GraphGetEdges<T>(this T[,] source, System.Func<int, int, T, bool> isEdgePredicate)
      where T : System.IEquatable<T>
    {
      GraphAssertProperty(source, out var length);

      for (var r = 0; r < length; r++)
        for (var c = 0; c < length; c++)
          if (source[r, c] is var m && isEdgePredicate(r, c, m))
            yield return (r, c, m);
    }
  }
}
