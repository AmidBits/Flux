namespace Flux
{
  public static partial class Reflection
  {
    public static System.Collections.Generic.IEnumerable<int> GraphGetVertices<T>(this T[,] source)
      where T : System.IEquatable<T>
    {
      GraphAssertProperty(source, out var length);

      for (var index = 0; index < length; index++)
        yield return index;
    }
  }
}
