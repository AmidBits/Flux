namespace Flux
{
  public static partial class Reflection
  {
    public static void GraphAssertProperty<T>(this T[,] source, out int length)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (source.Rank != 2) throw new System.ArgumentException($"Invalid rank ({source.Rank}).", nameof(source));
      if (!TryGetSymmetricalLength(source, out length)) throw new System.ArgumentException($"An asymmetrical array does not represent an adjacency matrix.");
    }
  }
}
