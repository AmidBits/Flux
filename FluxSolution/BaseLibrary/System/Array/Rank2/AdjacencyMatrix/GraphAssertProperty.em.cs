namespace Flux
{
  public static partial class ArrayRank2
  {
    public static void GraphAssertProperty<T>(this T[,] source, out int length)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (source.Rank != 2) throw new System.ArgumentException($"Invalid rank ({source.Rank}).", nameof(source));
      if(!TryGetSymmetricalLength(source, out length)) throw new System.ArgumentException($"An asymmetrical array does not represent an adjacency matrix.");
    }
  }
}
