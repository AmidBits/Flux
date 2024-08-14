namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Asserts that the <paramref name="source"/> rank is equal to <paramref name="rank"/> and throws an exception if not.</para>
    /// </summary>
    public static System.Array AssertEqualRank(this System.Array source, int rank)
      => IsEqualRank(source, rank) ? source : throw new System.ArgumentException($"Invalid rank ({source.Rank} != {rank}).", nameof(source));

    /// <summary>
    /// <para>Indicates whether <paramref name="source"/> is not null and <paramref name="source"/>.Rank is equal to <paramref name="rank"/>.</para>
    /// </summary>
    public static bool IsEqualRank(this System.Array source, int rank)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (rank < 1) throw new System.ArgumentOutOfRangeException(nameof(rank));

      return source.Rank == rank;
    }
  }
}
