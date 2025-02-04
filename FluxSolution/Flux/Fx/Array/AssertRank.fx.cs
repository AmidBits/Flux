namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Asserts that the <paramref name="source"/> rank is equal to <paramref name="rank"/> and throws an exception if not.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="rank"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentNullException"></exception>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Array AssertRank(this System.Array source, int rank)
      => source.IsRank(rank) ? source : throw new System.ArgumentOutOfRangeException(nameof(source), "Array rank mismatch.");

    /// <summary>
    /// <para>Indicates whether <paramref name="source"/> is not null and <paramref name="source"/>.Rank is equal to <paramref name="rank"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="rank"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static bool IsRank(this System.Array source, int rank)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (rank < 1) throw new System.ArgumentOutOfRangeException(nameof(rank));

      return source.Rank == rank;
    }
  }
}
