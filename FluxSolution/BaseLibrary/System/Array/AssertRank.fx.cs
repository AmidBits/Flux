namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Asserts that the <paramref name="source"/> rank is equal to <paramref name="rank"/> and throws an exception if not.</para>
    /// </summary>
    public static System.Array AssertRank(this System.Array source, int rank)
      => source.IsRank(rank) ? source : throw new System.ArgumentException($"Invalid rank ({source.Rank} != {rank}).", nameof(source));

    /// <summary>
    /// <para>Indicates whether <paramref name="source"/> is not null and <paramref name="source"/>.Rank is equal to <paramref name="rank"/>.</para>
    /// </summary>
    public static bool IsRank(this System.Array source, int rank)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (rank < 1) throw new System.ArgumentOutOfRangeException(nameof(rank));

      return source.Rank == rank;
    }
  }
}
