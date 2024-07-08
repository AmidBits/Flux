namespace Flux
{
  public static partial class Fx
  {
    public static void GraphAssertProperty<T>(this T[,] source, out int length)
    {
      if (!source.TryGetSymmetricalLength(out length) || source.IsUnequalRank(2))
      {
        System.ArgumentNullException.ThrowIfNull(source);

        throw new System.ArgumentException($"An asymmetrical array does not represent an adjacency matrix.");
      }
    }
  }
}
