namespace Flux
{
  public static partial class Arrays
  {
    /// <summary>
    /// <para>Asserts the adjacency matrix graph property: two dimensions of symmetrical length.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="length"></param>
    /// <exception cref="System.ArgumentNullException"></exception>
    /// <exception cref="System.ArgumentException"></exception>
    public static void GraphAssertProperty<T>(this T[,] source, out int length)
    {
      if (!source.TryGetDimensionallySymmetricalLength(out length) || !source.IsRank(2))
      {
        System.ArgumentNullException.ThrowIfNull(source);

        throw new System.ArgumentException($"An asymmetrical array does not represent an adjacency matrix.");
      }
    }
  }
}
