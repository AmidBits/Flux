namespace Flux
{
  public static partial class Fx
  {
    public static int[] GraphGetVertices<T>(this T[,] source)
    {
      GraphAssertProperty(source, out var length);

      return System.Linq.Enumerable.Range(0, length).ToArray();
    }
  }
}
