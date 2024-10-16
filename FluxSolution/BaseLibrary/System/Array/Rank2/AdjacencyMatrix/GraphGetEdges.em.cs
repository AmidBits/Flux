namespace Flux
{
  public static partial class Fx
  {
    public static System.Collections.Generic.IEnumerable<(int keySource, int keyTarget, T value)> GraphGetEdges<T>(this T[,] source)
      where T : System.IEquatable<T>
    {
      GraphAssertProperty(source, out var length);

      for (var si = 0; si < length; si++)
        for (var ti = 0; ti < length; ti++)
          if (source[si, ti] is var m && !m.Equals(default!))
            yield return (si, ti, m);
    }
  }
}
