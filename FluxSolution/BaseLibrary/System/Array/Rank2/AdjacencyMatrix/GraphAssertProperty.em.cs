namespace Flux
{
  public static partial class Fx
  {
    public static void GraphAssertProperty<T>(this T[,] source, out int length)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (!source.TryGetSymmetricalLength(out length) || source.Rank != 2) throw new System.ArgumentException($"An asymmetrical array does not represent an adjacency matrix.");
    }
  }
}
